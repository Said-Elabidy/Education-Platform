using Education.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Domain.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        public Task<int> AddQuestionAsync(Question question)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteQuestionAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Question>> GetAllQuestionsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Question> GetQuestionByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateQuestion(Question question)
        {
            throw new NotImplementedException();
        }
    }
}
