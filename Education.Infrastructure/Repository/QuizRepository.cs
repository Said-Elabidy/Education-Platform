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
        private readonly EducationPlatformDBContext _context;

        public QuizRepository(EducationPlatformDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Quiz?> GetQuizIncludeQuestionsAsync(int Id)
        {
            return await _context.quizzes.Include(q =>q.Questions).AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);
        }
    }
}
