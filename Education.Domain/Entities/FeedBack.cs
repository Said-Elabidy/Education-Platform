using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Domain.Entities
{
	public class FeedBack :BaseModal
	{
		public int FeedBackId { get; set; } 
		public string UserId { get; set; }	
		public ApplicationUser ApplicationUser { get; set; }	
		public int VideoId { get; set; }	
		public Video video { get; set; }
		public int Rating { get; set; }	
		public string? Comment { get; set; }	

	}
}
