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
    public class QuizRepository : GenericRepository<Quiz>, IQuizRepository
    {
        // no need to have a private context member cause it's already inherted

        

        public QuizRepository(EducationPlatformDBContext context) : base(context)
        {
            
        }

        public async Task<Quiz?> GetQuizIncludeQuestionsAsync(int Id)
        {
            return await _context.quizzes
            .Where(q => q.Id == Id)
            .Select(q => new Quiz
            {
                Id = q.Id,
                Title = q.Title,
                Questions = q.Questions
                .Select(q => new Question
                {
                    Id = q.Id,
                    Header = q.Header,
                    CorrectAnswer = q.CorrectAnswer,
                    Order = q.Order,
                    QuizId = q.QuizId,
                    // Exclude Quiz to break the cycle 
                }).ToList()
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();
        }
       
        public async Task<Quiz?> GetQuizeBySectionId(int sectionId)
        {
            return await _dbSet.Include(q=>q.Questions).FirstOrDefaultAsync(q => q.SectionId == sectionId);
        }
    }
}
