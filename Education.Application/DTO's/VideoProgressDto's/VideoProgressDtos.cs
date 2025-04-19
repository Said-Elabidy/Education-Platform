using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.DTO_s.VideoProgressDto_s
{
    public class VideoProgressDtos
    {
        public int VideoId { get; set; }
        public string UserId { get; set; }
        public bool IsWatched { get; set; }
    }
}
