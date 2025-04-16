using Education.Application.DTO_s.CourseDto_s;
using Education.Domain.Entities;
using Education.Domain.Repository;
using Education.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Infrastructure.Repository
{
	public class CourseRepository : GenericRepository<Courses> , ICourseRepository<GetCourseDataDTO>
	{
		private readonly EducationPlatformDBContext context;
		public CourseRepository(EducationPlatformDBContext context) : base(context)
		{ 
			this.context = context;	
		}

        public async Task<IEnumerable<GetCourseDataDTO>> GetAllCourses()
        {
            return await _dbSet.Select(c => new GetCourseDataDTO() {
                CategoriesId = c.CategoriesId, CourseImage = c.CourseImage,
                CoursesId = c.CoursesId, CourseStatus = c.CourseStatus, 
                Description = c.Description, DiscountPercentage = c.DiscountPercentage,
                IsFree = c.IsFree, IsSequentialWatch = c.IsSequentialWatch, Price = c.Price,
                Rating = c.Rating, Title = c.Title }).ToListAsync();
        }

        public async Task<GetCourseDataDTO?> GetCourseById(int Id)
        {
            var c = await _dbSet.FirstOrDefaultAsync(c => c.CoursesId == Id);
            if (c != null)
                return new GetCourseDataDTO()
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
                };
            return null;
        }
    }
}
