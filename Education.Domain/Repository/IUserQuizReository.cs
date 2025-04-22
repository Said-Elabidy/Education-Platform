using Education.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Domain.Repository
{
    public interface IUserQuizReository : IGenericRepository<UserQuiz>
    {
        Task<IEnumerable<UserQuiz>> GetQuizzesByUserIdAsync(string userId);
        Task<IEnumerable<UserQuiz>> GetUsersByQuizIdAsync(int quizId);
        Task<UserQuiz?> GetUserQuizAsync(string userId, int quizId);
        





    }
}
