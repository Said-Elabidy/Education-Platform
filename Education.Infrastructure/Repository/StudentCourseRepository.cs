using Education.Application.DTO_s.StudentCourse;
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
    class StudentCourseRepository : GenericRepository<StudentCourses>, IStudentCourseRepository<StudentCourseDTO>
    {
        public StudentCourseRepository(EducationPlatformDBContext context):base(context)
        {
            
        }

        public async Task<StudentCourses?> GellStudentCourse(string studentId, int courseId)
        {
            var reslt = await _dbSet.FirstOrDefaultAsync(sc => (sc.CoursesId == courseId && sc.UserId == studentId));

            if (reslt != null)
            {
              
                return reslt;
            }
            return null;
        }

        public async Task<IEnumerable<StudentCourseDTO>> GellStudentCoursesByCourseId(int courseId)
        {
            return await _dbSet.Where(sc => sc.CoursesId == courseId).Select(sc => new StudentCourseDTO
            { 
                CoursesId = sc.CoursesId, 
                CompletedAt = sc.CompletedAt,
                EnrollmentDate = sc.EnrollmentDate, 
                IsCompleted = sc.IsCompleted, 
                ProgressValue = sc.ProgressValue, 
                Rating = sc.Rating,
                UserId = sc.UserId 
            }).ToListAsync();
        }

        public async Task<IEnumerable<StudentCourseDTO>> GellStudentCoursesByStudentId(string studentId)
        {
            return await _dbSet.Where(sc => sc.UserId == studentId).Select(sc => new StudentCourseDTO
            {
                CoursesId = sc.CoursesId,
                CompletedAt = sc.CompletedAt,
                EnrollmentDate = sc.EnrollmentDate,
                IsCompleted = sc.IsCompleted,
                ProgressValue = sc.ProgressValue,
                Rating = sc.Rating,
                UserId = sc.UserId
            }).ToListAsync();
        }

    }
}
