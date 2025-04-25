using Education.Application.DTO_s.CourseDto_s;
using Education.Domain.Entities;
using Education.Domain.Repository;
using Education.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Education.Infrastructure.Repository
{
    public class CourseRepository : GenericRepository<Courses>, ICourseRepository
    {
        // no need to have a private context member cause it's already inherted
        public CourseRepository(EducationPlatformDBContext context) : base(context)
        {

        }

        public override async Task<int> RecordCount()
        {
            return await _dbSet.Where(i => i.IsDeleted == false).CountAsync();
        }

        public override async Task<Courses?> GetEntityAsync(Expression<Func<Courses, bool>> filter, string[] Includes = null, bool tracked = false)
        {
            IQueryable<Courses> query = _dbSet.Where(i => i.IsDeleted == false).AsQueryable();
            if (tracked)
                query = query.AsNoTracking();

            if (Includes != null)
            {
                foreach (var Include in Includes)
                    query = query.Include(Include);
            }

            return await query.SingleOrDefaultAsync(filter);
        }

        public override async Task<IEnumerable<Courses>> GetAllEntitiesAsync(Expression<Func<Courses, bool>> Filter = null, string[] Includes = null, bool track = false, int pageNumber = 0, int pageSize = 0)
        {
            IQueryable<Courses> query = _dbSet.Where(i => i.IsDeleted == false).AsQueryable();

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

        public async Task<IEnumerable<Courses>> GetAllCourses()
        {
            return await _dbSet.Select(c => new Courses()
            {
                CategoriesId = c.CategoriesId,
                CourseImage = c.CourseImage,
                CoursesId = c.CoursesId,
                CourseStatus = c.CourseStatus,
                Description = c.Description,
                DiscountPercentage = c.DiscountPercentage,
                IsFree = c.IsFree,
                IsSequentialWatch = c.IsSequentialWatch,
                Price = c.Price,
                Rating = c.Rating,
                Title = c.Title
            }).ToListAsync();
        }

        public async Task<Courses?> GetCourseById(int Id)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.CoursesId == Id);
            //if (c != null)
            //return new Courses() 22
            //{
            //    CategoriesId = c.CategoriesId,
            //    CourseImage = c.CourseImage,
            //    CoursesId = c.CoursesId,
            //    CourseStatus = c.CourseStatus,
            //    Description = c.Description,
            //    DiscountPercentage = c.DiscountPercentage,
            //    IsFree = c.IsFree,
            //    IsSequentialWatch = c.IsSequentialWatch,
            //    Price = c.Price,
            //    Rating = c.Rating,
            //    Title = c.Title
            //};
            // return null;
        }
    }
}
