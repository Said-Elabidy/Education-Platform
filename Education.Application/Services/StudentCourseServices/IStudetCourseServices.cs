using Education.Application.DTO_s.StudentCourse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Services.StudentCourseServices
{
    public interface IStudetCourseServices
    {
        Task Add(StudentCourseDTO studentCourse);
        Task<bool> Update(string userId, int courseId, UpdateStudentCourseDTO updateStudentCourse);

        Task<IEnumerable<StudentCourseDTO>> GellStudentCoursesByStudentId(string studentId);
        Task<IEnumerable<StudentCourseDTO>> GellStudentCoursesByCourseId(int courseId);
        Task<StudentCourseDTO?> GellStudentCourse(string studentId, int courseId);
    }
}
