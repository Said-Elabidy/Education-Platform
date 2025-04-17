using System.ComponentModel.DataAnnotations;

namespace EducationPlatform.ViewModels
{
    public record RegisterViewModel
    {
    
        [Required(ErrorMessage = "First Name is required")]
        [MaxLength(10, ErrorMessage = "First Name can't be more than 10 chars")]
        [MinLength(3, ErrorMessage = "First Name can't be less than 3 chars")]
        public string FirstName { get; init; } = null!;
        [Required(ErrorMessage = "Last Name is required")]
        [MaxLength(10, ErrorMessage = "Last Name can't be more than 10 chars")]
        [MinLength(3, ErrorMessage = "Last Name can't be less than 3 chars")]
        public  string LastName { get; init; } = null!;
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage ="Email isn't in correct format")]
        public string Email { get; init; } = null!;
        [Required(ErrorMessage = "Password is required")]
        [MaxLength(10, ErrorMessage = "Password can't be more than 10 chars")]
        [MinLength(8, ErrorMessage = "Password can't be less than 8 chars")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,10}$"
         , ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, and one number")]
        public string Password { get; init; } = null!;

    }
}
