using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Domain.Entities
{
	public class ApplicationUser : IdentityUser 
	{
		public string FirstName { get; set; } = null!;
		public string LastName { get; set; } =null!;
		public string? ProfileImage { get; set; }
		public DateTime DateOfBirt { get; set; }
		public ICollection<FeedBack>? feedBacks { get; set; }
		public ICollection<Payment>? payments { get; set; }

		public ICollection<StudentCourses>? StudentCourses { get; set; }

    }
}
