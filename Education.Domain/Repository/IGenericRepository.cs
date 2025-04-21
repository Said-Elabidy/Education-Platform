using Education.Domain.Entities;
using System.Linq.Expressions;

namespace Education.Domain.Repository
{
    public interface IGenericRepository <T> where T : BaseModal
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
		Task<IEnumerable<T>> GetAllEntitiesAsync(Expression<Func<T, bool>> Filter = null, string[] Includes = null, bool track = false, int pageNumber=0, int pageSize = 0);
		Task<T?> GetEntityAsync(Expression<Func<T, bool>> filter, string[] Includes = null, bool tracked = false);
        Task<int> RecordCount();
        Task AddAsync(T entity); 
        void Update(T entity);
        void Delete(T entity);
        Task<bool> SaveChangesAsync();
        Task<bool> Delete(int Id);
    }
}
