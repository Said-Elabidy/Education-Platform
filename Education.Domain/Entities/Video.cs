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
        public string? Description { get; set; }

        public TimeSpan VideoDuration { get; set; }  

        public bool IsFree { get; set; }

        // Public URL of the video thumbnail (image) stored in Supabase
        public string? VideoImageUrl { get; set; }

        // Public URL of the video file stored in Supabase
        public string VideoFileUrl { get; set; } 

        public int SectionId { get; set; }
        public Section? Section { get; set; }

        // Navigation property
        public ICollection<FeedBack> FeedBacks { get; set; } = new List<FeedBack>();

        
    }

}
