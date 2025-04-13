using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Domain.Entities
{
	public class Section
	{
		public int SectionId { get; set; }	
		public string SectionName { get; set; }		
		public bool IsPassSection {  get; set; }	//chech if he pass this section and allow to go next section 
		public int CourseId { get; set; }
		public Courses Courses { get; set; }
		public ICollection<Video> Videos { get; set; } = new List<Video>();

        public Quiz Quiz { get; set; } = null!;
    }
}

//will be work when IsSequentialWatch on Course table (False)

//IsPassSection  will calculate him    buy watched Video on section Per student ===count of video on section 



