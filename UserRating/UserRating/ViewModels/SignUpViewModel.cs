using System.ComponentModel.DataAnnotations;

namespace UserRating.ViewModels
{
    public class SignUpViewModel
    {
        [MinLength(3)]
        [StringLength(20)]
        [Required(ErrorMessage = "Please create login!")]
        public string? Login { get; set; }

        [MinLength(3)]
        [StringLength(20)]
        [Required(ErrorMessage = "Please create password!")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [MinLength(3)]
        [StringLength(20)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Invalid password!")]
        public string? ConfirmPassword { get; set; }
    }
}
