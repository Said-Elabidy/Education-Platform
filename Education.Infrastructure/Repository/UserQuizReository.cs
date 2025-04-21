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
    public class UserQuizReository : GenericRepository<UserQuiz>, IUserQuizReository
    {
        public UserQuizReository(EducationPlatformDBContext context) : base(context)
        {
        }
        public async Task<IEnumerable<UserQuiz>> GetUsersByQuizIdAsync(string userId)
        {
            return await _dbSet
                .Where(uq => uq.UserId == userId)
                .Include(uq => uq.Quiz)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserQuiz>> GetQuizzesByQuizIdAsync(int quizId)
        {
            return await _dbSet
                .Where(uq => uq.QuizId == quizId)
                .Include(uq => uq.ApplicationUser)
                .ToListAsync();
        }
        public async Task<UserQuiz?> GetUserQuizAsync(string userId, int quizId)
        {
            return await _dbSet
                .FirstOrDefaultAsync(uq => uq.UserId == userId && uq.QuizId == quizId);
        }

        Task<IEnumerable<UserQuiz>> IUserQuizReository.GetQuizzesByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<UserQuiz>> IUserQuizReository.GetUsersByQuizIdAsync(int quizId)
        {
            throw new NotImplementedException();
        }

        
    }
}
