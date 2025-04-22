using Education.Application.DTO_s.QuestionDto_s;
using Education.Application.DTO_s.QuizDto_s;
using Education.Application.DTO_s.SectionDTO_s;
using Education.Application.DTO_s.VideoDto_s;
using Education.Domain.Entities;
using Education.Domain.Repository;
using Education.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Infrastructure.Repository
{
    public class SectionRepository : GenericRepository<Section>, ISectionRepository<SectionDto, GetSectionsWithIncloudQuiz_Video>
    {
        // no need to have a private context member cause it's already inherted

        public SectionRepository(EducationPlatformDBContext context) : base(context)
        {

        }

        //public async Task<IEnumerable<Section>> getAllByCourseId(int courseId)
        //{

        //}

        public async Task<SectionDto?> getBySectionId(int sectionId)
        {
            var s = await _dbSet.Include(s => s.Quiz).Include(s => s.Videos).FirstOrDefaultAsync(s => s.SectionId == sectionId);
            if (s != null)
                return new SectionDto()
                {
                    SectionId = s.SectionId,
                    IsPassSection = s.IsPassSection,
                    //Quiz = new GetQuizeDTO { Id = s.Quiz.Id, NumOfQuestion = s.Quiz.NumOfQuestion, PassingScore = s.Quiz.PassingScore, SectionId = s.Quiz.SectionId, Title = s.Quiz.Title },
                    SectionName = s.SectionName,
                    VideosNum = s.Videos.Count,
                    //Videos = s.Videos.Select(v => new GetVideosBySectionIdDto { Description = v.Description, IsFree = v.IsFree, SectionId = v.SectionId, Title = v.Title, VideoFileUrl = v.VideoFileUrl, VideoDuration = v.VideoDuration, VideoId = v.VideoId, VideoImageUrl = v.VideoImageUrl }).ToList()
                };
            return null;
        }

        public async Task<IEnumerable<SectionDto>> getAllByCourseId(int courseId)
        {
            return await _dbSet.Include(s=>s.Videos).Where(s => s.CourseId == courseId).
                Select(s => new SectionDto() {
                    SectionId = s.SectionId,
                    IsPassSection = s.IsPassSection, 
                    SectionName = s.SectionName,
             
                    VideosNum = s.Videos.Count }).ToListAsync();
        }

    public async Task<IEnumerable<GetSectionsWithIncloudQuiz_Video>> getAllByCourseIdWithInclouds(int courseId)
        {
            return await _dbSet.Include(s=>s.Quiz).Include(s=>s.Videos).Where(s => s.CourseId == courseId).
                Select(s => new GetSectionsWithIncloudQuiz_Video() {
                    SectionId = s.SectionId,
                    IsPassSection = s.IsPassSection, 
                Quiz = new GetQuizWithIcloudQuestions
                { Id=s.Quiz.Id,
                    NumOfQuestion=s.Quiz.NumOfQuestion,
                    PassingScore=s.Quiz.PassingScore, 
                    SectionId=s.Quiz.SectionId,
                    Title=s.Quiz.Title,
                    Questions=s.Quiz.Questions.Select(q=>new QuestionsDTO 
                    { QuizId=q.QuizId,
                        Order=q.Order,
                        Id=q.Id, 
                        CorrectAnswer=q.CorrectAnswer, 
                        Header= q.Header}).ToList() 
                    }, SectionName = s.SectionName,
                    Videos = s.Videos.Select(v => new GetVideosBySectionIdDto
                    { Description = v.Description, 
                        IsFree = v.IsFree,
                        SectionId = v.SectionId,
                        Title = v.Title, 
                        VideoFileUrl = v.VideoFileUrl,
                        VideoDuration = v.VideoDuration, 
                        VideoId = v.VideoId, 
                        VideoImageUrl = v.VideoImageUrl }).ToList(),
                    VideosNum = s.Videos.Count }).ToListAsync();
        }

    }
}