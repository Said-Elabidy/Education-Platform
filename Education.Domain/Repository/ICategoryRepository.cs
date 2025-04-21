using Education.Domain.Entities;

namespace Education.Domain.Repository
{
    public interface ICategoryRepository : IGenericRepository<Categories>
    {
        Task<Categories?> GetNotDeletedCategoryById(int id);
    }
}
