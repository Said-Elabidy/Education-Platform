using Education.Application.DTO_s.StudentCourse;
using Education.Domain.Entities;
using Education.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Services.StudentCourseServices
{
    public class StudentCourseServices : IStudetCourseServices
    {
        private readonly IStudentCourseRepository<StudentCourseDTO> _studentCourseRepository;

        public StudentCourseServices(IStudentCourseRepository<StudentCourseDTO> studentCourseRepository)
        {
            _studentCourseRepository = studentCourseRepository;
        }
        public async Task Add(StudentCourseDTO studentCourse)
        {
            var entity = new StudentCourses 
            { CompletedAt = studentCourse.CompletedAt,
                CoursesId = studentCourse.CoursesId,
                EnrollmentDate = DateTime.Now, 
                IsCompleted = false,
                ProgressValue = 0,
                UserId = studentCourse.UserId,
                Rating = studentCourse.Rating };
            await _studentCourseRepository.AddAsync(entity);
            await _studentCourseRepository.SaveChangesAsync();
        }

        public async Task<StudentCourseDTO?> GellStudentCourse(string studentId, int courseId)
        {
            var sc= await _studentCourseRepository.GellStudentCourse(studentId, courseId);
            if (sc != null) {
                var reslt = new StudentCourseDTO
                {
                    CoursesId = sc.CoursesId,
                    CompletedAt = sc.CompletedAt,
                    EnrollmentDate = sc.EnrollmentDate,
                    IsCompleted = sc.IsCompleted,
                    ProgressValue = sc.ProgressValue,
                    Rating = sc.Rating,
                    UserId = sc.UserId
                };
                return reslt;
            }
            return null; 
        }

        public async Task<IEnumerable<StudentCourseDTO>> GellStudentCoursesByCourseId(int courseId)
        {
            return await _studentCourseRepository.GellStudentCoursesByCourseId(courseId);
        }

        public async Task<IEnumerable<StudentCourseDTO>> GellStudentCoursesByStudentId(string studentId)
        {
            return await _studentCourseRepository.GellStudentCoursesByStudentId(studentId);
        }

        public async Task<bool> Update(string userId, int courseId, UpdateStudentCourseDTO updateStudentCourse)
        {
            var entity = await _studentCourseRepository.GellStudentCourse(userId, courseId);
            if (entity != null)
            {
                entity.ProgressValue = updateStudentCourse.ProgressValue;
                entity.CompletedAt = updateStudentCourse.CompletedAt;
                entity.IsCompleted = updateStudentCourse.IsCompleted;
                entity.Rating = updateStudentCourse.Rating;
                _studentCourseRepository.Update(entity);
                await _studentCourseRepository.SaveChangesAsync();
                return true;

            }
            return false;
        }
    }
}
