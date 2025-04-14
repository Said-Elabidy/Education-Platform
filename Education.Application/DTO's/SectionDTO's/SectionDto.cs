using Education.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.DTO_s.SectionDTO_s
{
    public class SectionDto
    {
        public int SectionId { get; set; }
        [MaxLength(40)]
        [Required]
        public string SectionName { get; set; }
        public bool IsPassSection { get; set; } = false; //chech if he pass this section and allow to go next section 
        public int CourseId { get; set; }
        public Courses Courses { get; set; }
        public ICollection<Video> Videos { get; set; } = new List<Video>();

        public Quiz Quiz { get; set; } = null!;
    }
}
