using Education.Application.DTO_s.QuizDto_s;
using Education.Application.DTO_s.VideoDto_s;
using Education.Domain.Entities;
using System.ComponentModel.DataAnnotations;


namespace Education.Application.DTO_s.SectionDTO_s
{
    public class SectionDto
    {
        public int SectionId { get; set; }
        [MaxLength(40)]
        [Required]
        public string SectionName { get; set; }
        public bool IsPassSection { get; set; }
        public int VideosNum { get; set; } = 0;

        public int? quizId { get; set; } = null;
        //public List<GetVideosBySectionIdDto> Videos { get; set; } = new List<GetVideosBySectionIdDto>();


        public GetQuizeDTO? Quiz { get; set; }
    }
}
