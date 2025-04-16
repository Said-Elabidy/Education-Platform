using Education.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Domain.Repository
{
	public interface ICourseRepository<TCoures> : IGenericRepository<Courses>
	{
		Task<IEnumerable<TCoures>> GetAllCourses();
		Task<TCoures?> GetCourseById(int Id);
	}
}
