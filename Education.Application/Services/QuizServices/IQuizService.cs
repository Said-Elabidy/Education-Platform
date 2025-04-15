using Education.Application.DTO_s;
using Education.Application.DTO_s.QuizDto_s;
using Education.Domain.Entities;

namespace Education.Application.Services.QuizServices
{
    public interface IQuizService
    {
        Task<IEnumerable<Quiz>> GetQuizzes();

        Task<Quiz?> GetQuizById(int id);

        Task<bool> Update(int Id, UpdateQuizDto updateQuizDto);
        Task<bool> Delete(int id);

        Task<Quiz>Add(AddQuizDto quizDto);

        Task<Quiz?> GetQuizWithQuestions(int Id);
    }
}
