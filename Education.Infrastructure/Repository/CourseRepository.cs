using Education.Application.DTO_s.CourseDto_s;
using Education.Domain.Entities;
using Education.Domain.Repository;
using Education.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Education.Infrastructure.Repository
{
	public class CourseRepository : GenericRepository<Courses> , ICourseRepository
	{
		// no need to have a private context member cause it's already inherted
		public CourseRepository(EducationPlatformDBContext context) : base(context)
		{ 
				
		}

        public async Task<IEnumerable<Courses>> GetAllCourses()
        {
            return await _dbSet.Select(c => new Courses() {
                CategoriesId = c.CategoriesId, CourseImage = c.CourseImage,
                CoursesId = c.CoursesId, CourseStatus = c.CourseStatus, 
                Description = c.Description, DiscountPercentage = c.DiscountPercentage,
                IsFree = c.IsFree, IsSequentialWatch = c.IsSequentialWatch, Price = c.Price,
                Rating = c.Rating, Title = c.Title }).ToListAsync();
        }

        public async Task<Courses?> GetCourseById(int Id)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.CoursesId == Id);
            //if (c != null)
                //return new Courses()
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
            return null;
        }
    }
}
