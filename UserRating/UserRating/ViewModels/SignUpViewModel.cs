using System.ComponentModel.DataAnnotations;

namespace UserRating.ViewModels
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "Please enter email!")]
        [StringLength(30, MinimumLength = 5, 
            ErrorMessage = "The EMAIL length must be between 5 and 30 characters!")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Please create login!")]
        [StringLength(20, MinimumLength = 3, 
            ErrorMessage = "The LOGIN length must be between 3 and 20 characters!")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Please create password!")]
        [StringLength(20, MinimumLength = 3, 
            ErrorMessage = "The PASSWORD length must be between 3 and 20 characters!")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "Invalid password!")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
    }
}