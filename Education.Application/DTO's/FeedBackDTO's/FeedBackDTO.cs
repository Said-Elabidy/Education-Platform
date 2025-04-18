using Education.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.DTO_s.FeedBackDTO_s
{
    public class FeedBackDTO
    {
        public int FeedBackId { get; set; }
        public string UserId { get; set; }
        public int VideoId { get; set; }
        //public Video video { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}
