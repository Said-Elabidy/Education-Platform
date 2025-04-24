using Education.Domain.Entities;
using System.ComponentModel.DataAnnotations;


namespace Education.Application.DTO_s.SectionDTO_s
{
    public class UpdateSectionDto
    {
        [MaxLength(40)]
        [Required]
        public string SectionName { get; set; }
        public bool IsPassSection { get; set; } = false;	//chech if he pass this section and allow to go next section 
        public Quiz Quiz { get; set; } = null!;

    }
}
