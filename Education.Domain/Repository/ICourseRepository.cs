using Education.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Domain.Repository
{
    public interface ICourseRepository : IGenericRepository<Courses>
    {
        Task<IEnumerable<Courses>> GetAllCourses();
        Task<Courses?> GetCourseById(int Id);
    }
}
