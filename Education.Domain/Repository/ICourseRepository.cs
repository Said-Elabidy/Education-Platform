using Education.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Domain.Repository
{
	public interface ICourseRepository<TCourses>: IGenericRepository<Courses>
	{
		Task<IEnumerable<TCourses>> GetAllCourses();
		Task<TCourses?> GetCourseById(int Id);
	}
}
