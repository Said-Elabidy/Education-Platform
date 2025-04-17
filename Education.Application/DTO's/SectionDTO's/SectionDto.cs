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
        public int VideosNum { get; set; } 

        public Quiz? Quiz { get; set; }
    }
}
