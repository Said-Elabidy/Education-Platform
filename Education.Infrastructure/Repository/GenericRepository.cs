﻿using Education.Domain.Entities;
using Education.Domain.Repository;
using Education.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;

namespace Education.Infrastructure.Repository
{
    // i marked generic repo as abstract cause it's not gonna be injected , it's used as a template
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly EducationPlatformDBContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(EducationPlatformDBContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }



		public virtual async Task<T?> GetByIdAsync(int id)
		{
			return await _dbSet.FindAsync(id);
		}

		public virtual async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _dbSet.ToListAsync();
		}

		public virtual async Task AddAsync(T entity)
		{
			await _dbSet.AddAsync(entity);
		}

		public virtual void Update(T entity)
		{
			_dbSet.Update(entity);

        }

		public virtual void Delete(T entity)
		{


            _dbSet.Remove(entity);

        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }


          
          public virtual async Task<bool> Delete(int Id)

        {
            var entity = await _dbSet.FindAsync(Id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                return true;
            }
            return false;
            //_context.Remove(Id);
        }

        public virtual async Task<IEnumerable<T>> GetAllEntitiesAsync(Expression<Func<T, bool>> Filter = null, string[] Includes = null, bool track = false, int pageNumber = 0, int pageSize = 0)
        {
            IQueryable<T> query = _dbSet.AsQueryable();

            if (track)
            {
                query = query.AsNoTracking();
            }
            if (Includes != null)
            {
                foreach (var Include in Includes)
                    query = query.Include(Include);
            }

            if (Filter != null)
            {
                query = query.Where(Filter);
            }
            if (pageNumber != 0 && pageSize != 0)
            {

                query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            }
            return await query.ToListAsync();
        }

        public virtual async Task<T?> GetEntityAsync(Expression<Func<T, bool>> filter, string[] Includes = null, bool tracked = false)
        {
            IQueryable<T> query = _dbSet.AsQueryable();
            if (tracked)
                query = query.AsNoTracking();

            if (Includes != null)
            {
                foreach (var Include in Includes)
                    query = query.Include(Include);
            }

            return await query.SingleOrDefaultAsync(filter);
        }
        public virtual async Task<int> RecordCount()
        {
            return await _dbSet.CountAsync();
        }


    }
}


