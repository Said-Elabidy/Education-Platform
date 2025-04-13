using System.ComponentModel.DataAnnotations;

namespace Education.Application.DTO_s;

public class UpdateQuestionDto
{
    [Required(ErrorMessage = "Question header is required")]
    [StringLength(500, MinimumLength = 10,
                ErrorMessage = "Question header must be between 10 and 500 characters")]
    public string Header { get; set; } = default!;

    [Required(ErrorMessage = "Order is required")]
    [Range(1, 100, ErrorMessage = "Order must be between 1 and 100")]
    public int Order { get; set; }

    [Required(ErrorMessage = "Correct answer is required")]
    public bool CorrectAnswer { get; set; }

   

}
