using Education.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Services.UserQuizServices
{
    public interface IUserQuizServices
    {
        Task<IEnumerable<UserQuiz>> GetQuizzesByUserIdAsync(string userId);
        Task<IEnumerable<UserQuiz>> GetUsersByQuizIdAsync(int quizId);
        Task<UserQuiz?> GetUserQuizAsync(string userId, int quizId);
        Task<bool> UpdateQuizScoreAsync(string userId, int quizId, int score);




    }
}
