using Education.Application.DTO_s.QuizDto_s;
using Education.Application.DTO_s.VideoDto_s;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.DTO_s.SectionDTO_s
{
    public class GetSectionsWithIncloudQuiz_Video
    {
        public int SectionId { get; set; }
        [MaxLength(40)]
        [Required]
        public string SectionName { get; set; }
        public bool IsPassSection { get; set; }
        public int VideosNum { get; set; } = 0;
        public List<GetVideosBySectionIdDto> Videos { get; set; } = new List<GetVideosBySectionIdDto>();

        public GetQuizWithIcloudQuestions? Quiz { get; set; }
    }
}
