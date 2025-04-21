using Education.Domain.Entities;
using Education.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Services.UserQuizServices
{
    public class UserQuizServices : IUserQuizServices

    {
        private readonly IUserQuizReository _userQuizRepository;

        public UserQuizServices(IUserQuizReository userQuizRepository)
        {
            _userQuizRepository = userQuizRepository;
        }
        public async Task<IEnumerable<UserQuiz>> GetQuizzesByUserIdAsync(string userId)
        {
            return await _userQuizRepository.GetQuizzesByUserIdAsync(userId);
        }
        public async Task<IEnumerable<UserQuiz>> GetUsersByQuizIdAsync(int quizId)
        {
            return await _userQuizRepository.GetUsersByQuizIdAsync(quizId);
        }
        public async Task<UserQuiz?> GetUserQuizAsync(string userId, int quizId)
        {
            return await _userQuizRepository.GetUserQuizAsync(userId, quizId);
        }
        public async Task<bool> UpdateQuizScoreAsync(string userId, int quizId, int score)
        {
            
            var userQuiz = await GetUserQuizAsync(userId, quizId);
            if (userQuiz == null)
                return false;

            
            userQuiz.Score = score;

            
            userQuiz.IsPassed = score >= userQuiz.Quiz.PassingScore;

            
            _userQuizRepository.Update(userQuiz);

            
            return await _userQuizRepository.SaveChangesAsync();
        }



    }
}
