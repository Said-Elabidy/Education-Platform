using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Domain.Entities
{
	public class VideoProgress
	{
		public int VideoId { get; set; }		
		public Video video {  get; set; }	
		public string UserId { get; set; }	
		public ApplicationUser applicationUser {  get; set; }
		public bool IsWatched { get; set; }
	}
}
