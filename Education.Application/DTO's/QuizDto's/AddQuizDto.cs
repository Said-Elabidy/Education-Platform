
using System.ComponentModel.DataAnnotations;


namespace Education.Application.DTO_s.QuizDto_s
{
    public class AddQuizDto
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 100 characters")]
        public string Title { get; set; } = default!;

        [Required(ErrorMessage = "Passing score is required")]
        [Range(1, 100, ErrorMessage = "Passing score must be between 1 and 100")]
        public int PassingScore { get; set; }

        [Required(ErrorMessage = "Section ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Section ID must be a positive number")]
        public int SectionId { get; set; }
    }
}
