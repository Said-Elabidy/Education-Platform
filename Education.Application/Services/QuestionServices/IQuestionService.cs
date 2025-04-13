using Education.Domain.Entities;

namespace Education.Application.Services.QuestionServices
{
    public interface IQuestionService
    {
        Task<IEnumerable<Question>> GetQuestions();

        Task<Question> GetQuestionById(int id);

        Task <bool>Update(Question question);

        Task <bool>Delete(int id);

        Task Add(Question question);
        
    }
}
