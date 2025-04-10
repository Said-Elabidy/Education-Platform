using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Domain.Entities
{
	public class Video
	{
		public int VideoId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public double VideoDuration { get; set; }
		public bool IsFree { get; set; }

		public string VideoImage { get; set; }
		public string VideoData { get; set; }
		public int SectionId { get; set; }
		public Section Section { get; set; }
        
		public ICollection<FeedBack> feedBacks { get; set; }
	}
}
