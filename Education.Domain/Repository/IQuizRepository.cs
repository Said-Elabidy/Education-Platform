using Education.Domain.Entities;

namespace Education.Domain.Repository;

public  interface IQuizRepository :IGenericRepository<Quiz>
{
    public Task<Quiz?> GetQuizIncludeQuestionsAsync(int Id);
    Task<Quiz?> GetQuizeBySectionId(int sectionId);
}
