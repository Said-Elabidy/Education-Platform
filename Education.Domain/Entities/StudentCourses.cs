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
		public string UserId { get; set; }
		public int CoursesId { get; set; }
        public int ProgressValue { get; set; }

		public int Rating { get; set; }  //to know if student complete course
        public Courses Course { get; set; }
		public DateTime EnrollmentDate { get; set; }
		public bool IsCompleted { get; set; }  //to know if student complete course
		public DateTime? CompletedAt { get; set; }
		public ApplicationUser ApplicationUser { get; set; }

		
		
	}
}
