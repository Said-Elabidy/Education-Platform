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
            return await _context.quizzes.Include(q =>q.Questions).AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);
        }
        public async Task<Quiz?> GetQuizeBySectionId(int sectionId)
        {
            return await _dbSet.Include(q=>q.Questions).FirstOrDefaultAsync(q => q.SectionId == sectionId);
        }
    }
}
