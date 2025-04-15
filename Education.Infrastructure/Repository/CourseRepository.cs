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
	public class CourseRepository : GenericRepository<Courses> , ICourseRepository
	{
		private readonly EducationPlatformDBContext context;
		public CourseRepository(EducationPlatformDBContext context) : base(context)
		{ 
			this.context = context;	
		}
	  
	}
}
