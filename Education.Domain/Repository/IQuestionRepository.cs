using Education.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Domain.Repository
{
    public interface IQuestionRepository
    {
        Task <IEnumerable<Question>> GetAllQuestionsAsync ();

        Task<Question> GetQuestionByIdAsync (int id);

        Task<int> AddQuestionAsync(Question question);

        Task <bool> UpdateQuestion (Question question);

        Task<bool> DeleteQuestionAsync(int Id);

        Task<bool> SaveChangesAsync();
    }
}
