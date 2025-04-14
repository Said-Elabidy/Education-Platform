using Education.Domain.Repository;
using Education.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Education.Infrastructure.Repository
{

        public class GenericRepository<T> : IGenericRepository<T> where T : class
        {
            protected readonly EducationPlatformDBContext _context;
            protected readonly DbSet<T> _dbSet;

            public GenericRepository(EducationPlatformDBContext context)
            {
                _context = context;
                _dbSet = context.Set<T>();
            }

            public async Task<T?> GetByIdAsync(int id)
            {
                return await _dbSet.FindAsync(id);
            }

            public async Task<IEnumerable<T>> GetAllAsync()
            {
                return await _dbSet.ToListAsync();
            }

            public async Task AddAsync(T entity)
            {
                await _dbSet.AddAsync(entity);
            }

            public void Update(T entity)
            {
            _dbSet.Update(entity);

            }

           public void Delete(T entity)
            {

            _dbSet.Remove(entity);

            }

             public async Task<bool> SaveChangesAsync()
            {
                return await _context.SaveChangesAsync() > 0;
            }

            public async Task Delete(int Id)
            {
                var entity = await _dbSet.FindAsync(Id);
                if (entity != null)
                {
                    _dbSet.Remove(entity);
                }
            //_context.Remove(Id);
        }
    }
}
