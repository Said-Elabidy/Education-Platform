using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Domain.Entities
{
	//relation one-to-one ,payment-User , payment-Course
	public class Payment
	{
		
		public string UserId { get; set; }	
		public ApplicationUser ApplicationUser { get; set; }	
		public int CourseId { get; set; }	
		public Courses Courses { get; set; }
		public decimal TotalPrice { get; set; }
    }
}
