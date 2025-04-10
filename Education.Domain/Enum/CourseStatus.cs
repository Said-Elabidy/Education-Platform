using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Domain.Enum
{
	public enum CourseStatus
	{
		NotComplete=1,   //Teacher didnt complete download full course  ,student can watch video is on websit,in same time instructor download course
		Completed=2,    // all video download  
		WillComingSoon=3,   // اعلان عن كورس في المستقبل  
		
	}
}
