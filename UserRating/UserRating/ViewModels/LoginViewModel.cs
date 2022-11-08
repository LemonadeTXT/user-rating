using System.ComponentModel.DataAnnotations;

namespace UserRating.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter login!")]
        [StringLength(20, MinimumLength = 3, 
            ErrorMessage = "The LOGIN length must be between 3 and 20 characters!")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Please enter password!")]
        [StringLength(20, MinimumLength = 3, 
            ErrorMessage = "The PASSWORD length must be between 3 and 20 characters!")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}