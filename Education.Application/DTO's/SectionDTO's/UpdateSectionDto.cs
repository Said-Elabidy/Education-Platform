using Education.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
