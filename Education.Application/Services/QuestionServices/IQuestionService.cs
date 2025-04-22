using Education.Application.DTO_s;
using Education.Application.DTO_s.QuestionDto_s;
using Education.Domain.Entities;

namespace Education.Application.Services.QuestionServices
{
    public interface IQuestionService
    {
        Task<IEnumerable<Question>> GetQuestions();

        Task<Question?> GetQuestionById(int id);

        Task<QuestionsDTO?> GetQuestionDtoById(int id);

        Task<bool>Update(int Id, UpdateQuestionDto updateQuestionDto);

        Task <bool>Delete(int id);

        Task Add(CreateQuestionDto questionDto);
        
    }
}
