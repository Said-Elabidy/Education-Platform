using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.DTO_s.VideoDto_s
{
    public class GetVideosBySectionIdDto
    {
        public int VideoId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public TimeSpan VideoDuration { get; set; }
        public bool IsFree { get; set; }
        public string? VideoImageUrl { get; set; } 
        public string VideoFileUrl { get; set; }  
        public int SectionId { get; set; }

    }
}
