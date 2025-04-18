using Education.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Domain.Repository
{
    public interface IStudentCourseRepository<TStusentCourse>:IGenericRepository<StudentCourses>
    {
        Task<IEnumerable<TStusentCourse>> GellStudentCoursesByStudentId(string studentId);
        Task<IEnumerable<TStusentCourse>> GellStudentCoursesByCourseId(int courseId);
        Task<StudentCourses?> GellStudentCourse(string studentId, int courseId);
    }
}
