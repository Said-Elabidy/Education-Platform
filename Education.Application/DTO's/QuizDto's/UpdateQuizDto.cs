using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.DTO_s.QuizDto_s
{
    public class UpdateQuizDto
    {
        
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 100 characters")]
        public string ? Title { get; set; } = default!;


        [Range(1, 100, ErrorMessage = "Passing score must be between 1 and 100")]
        public int? PassingScore { get; set; }

        
    }
}
