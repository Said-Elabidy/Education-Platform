using System.ComponentModel.DataAnnotations;

namespace EducationPlatform.ViewModels
{
    public record LoginViewModel
    {
       
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email isn't in correct format")]
        public string Email { get; init; } = null!;
        [Required(ErrorMessage = "Password is required")]
        public  string Password { get; init; } = null!;
        [Required(ErrorMessage = "Remember me is required")]
        public bool RememberMe { get; init; } = false;
        
    }
}
