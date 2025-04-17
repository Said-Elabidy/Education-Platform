
using System.ComponentModel.DataAnnotations;


namespace Education.Application.DTO_s.CategoryDto_s
{
    public class CreateCategoryDto
    {

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]

        public string Name { get; set; } 
    }
}
