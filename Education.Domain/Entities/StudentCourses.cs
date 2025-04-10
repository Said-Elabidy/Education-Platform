using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Education.Domain.Entities
{
	public class StudentCourses
	{
		public string UserId { get; set; } = null!;
		public int CoursesId { get; set; }	
		public Courses Course { get; set; }
		public DateTime EnrollmentDate { get; set; }
		public bool IsCompleted { get; set; }  //to know if student complete course
		public DateTime? CompletedAt { get; set; }
		public ApplicationUser ApplicationUser { get; set; }
		
	}
}
