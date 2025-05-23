﻿using Education.Application.DTO_s.QuizDto_s;
using Education.Application.DTO_s.SectionDTO_s;
using Education.Domain.Entities;
using Education.Domain.Repository;


namespace Education.Application.Services.SectionServices
{
    public class SectionServices : ISectionServices
    {
        private readonly ISectionRepository<SectionDto, GetSectionsWithIncloudQuiz_Video> _sectionRepo;

        public SectionServices(ISectionRepository<SectionDto, GetSectionsWithIncloudQuiz_Video> sectionRepo)
        {
            _sectionRepo = sectionRepo;
        }
        public async Task Add(CreateSectionDto sectionDto)
        {
            var section = new Section()
            {
                SectionName = sectionDto.SectionName,
                //Videos = sectionDto.Videos,
                CourseId = sectionDto.CourseId,
                //Quiz = sectionDto.Quiz,

                
                IsPassSection=sectionDto.IsPassSection

            };
            await _sectionRepo.AddAsync(section);
            await _sectionRepo.SaveChangesAsync();


        }

        public async Task<bool> Delete(int id)
        {
            var section = await _sectionRepo.GetByIdAsync(id);
            if (section != null)
            {
                await _sectionRepo.Delete(id);
                await _sectionRepo.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<SectionDto?> GetSectionById(int id)
        {
            var section = await _sectionRepo.getBySectionId(id);
            if (section != null)
            {
                var sectionDto = new SectionDto()
                {
                    SectionId = section.SectionId,
                    SectionName = section.SectionName,
                    IsPassSection = section.IsPassSection,
                    quizId = section.quizId,
                    VideosNum = section.VideosNum,
                    //CourseId = section.CourseId,
                    //Courses = section.Courses,
                    //Videos = section.Videos,
                    //Quiz = new GetQuizeDTO { Id = section.Quiz.Id, NumOfQuestion = section.Quiz.NumOfQuestion, PassingScore = section.Quiz.PassingScore, SectionId = section.Quiz.SectionId, Title = section.Quiz.Title }
                };

                return sectionDto;
            }
            return null;
        }

        public async Task<IEnumerable<SectionDto>> GetSections()
        {
            var sections = await _sectionRepo.GetAllAsync();
            var sectionDto = sections.Select(s => new SectionDto()
            {
                SectionId = s.SectionId,
                SectionName = s.SectionName,
                IsPassSection = s.IsPassSection,
                quizId = s.Quiz.Id,
                VideosNum = s.Videos.Count()
                //CourseId = s.CourseId,
                //Courses = s.Courses,

                //Quiz = new GetQuizeDTO { Id = s.Quiz.Id, NumOfQuestion = s.Quiz.NumOfQuestion, PassingScore = s.Quiz.PassingScore, SectionId = s.Quiz.SectionId, Title = s.Quiz.Title },
                //Videos = s.Videos
            }).ToList();
            return sectionDto;
        }

        public async Task<IEnumerable<SectionDto>> GetSectionsByCourseId(int courseId)
        {
            var sections = await _sectionRepo.getAllByCourseId(courseId);
            var sectionDto = sections.Select(s => new SectionDto()
            {
                SectionId = s.SectionId,
                SectionName = s.SectionName,
                //CourseId = s.CourseId,
                //Courses = s.Courses,
                VideosNum = s.VideosNum,
                IsPassSection = s.IsPassSection,
                quizId = s.quizId,
                //Quiz = new GetQuizeDTO { Id = s.Quiz.Id, NumOfQuestion = s.Quiz.NumOfQuestion, PassingScore = s.Quiz.PassingScore, SectionId = s.Quiz.SectionId, Title = s.Quiz.Title, Questions=s.Quiz.Questions },

                //Videos = s.Videos
            }).ToList();
            return sectionDto;
        }
        public async Task<IEnumerable<GetSectionsWithIncloudQuiz_Video>> GetSectionsByCourseIdWithIncloudQuiz_Video(int courseId)
        {
            var sections = await _sectionRepo.getAllByCourseIdWithInclouds(courseId);
            var sectionDto = sections.Select(s => new GetSectionsWithIncloudQuiz_Video()
            {
                SectionId = s.SectionId,
                SectionName = s.SectionName,
                //CourseId = s.CourseId,
                //Courses = s.Courses,
                IsPassSection = s.IsPassSection,
                Quiz = new GetQuizWithIcloudQuestions { Id = s.Quiz.Id, NumOfQuestion = s.Quiz.NumOfQuestion, PassingScore = s.Quiz.PassingScore, SectionId = s.Quiz.SectionId, Title = s.Quiz.Title, Questions = s.Quiz.Questions },

                Videos = s.Videos
            }).ToList();
            return sectionDto;
        }

        public async Task<bool> Update(int sectionId, UpdateSectionDto sectionDto)
        {
            var section = await _sectionRepo.GetByIdAsync(sectionId);
            if (section != null)
            {
                if (sectionDto.SectionName != null)
                {
                    section.SectionName = sectionDto.SectionName;
                }
                section.IsPassSection = sectionDto.IsPassSection;
                await _sectionRepo.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
