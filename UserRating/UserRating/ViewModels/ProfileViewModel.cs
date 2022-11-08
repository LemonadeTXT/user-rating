using System.ComponentModel.DataAnnotations;

namespace UserRating.ViewModels
{
    public class ProfileViewModel
    {
        [Required(ErrorMessage = "Please fill in the field!")]
        [StringLength(30, MinimumLength = 1,
            ErrorMessage = "The NAME length must be between 1 and 30 characters!")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Please fill in the field!")]
        [StringLength(30, MinimumLength = 1,
            ErrorMessage = "The LAST NAME length must be between 1 and 30 characters!")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Please fill in the field!")]
        [Range(1, 100,
            ErrorMessage = "The AGE range must be between 1 and 100!")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Please fill in the field!")]
        [StringLength(20, MinimumLength = 1,
            ErrorMessage = "The City length must be between 1 and 20 characters!")]
        public string? City { get; set; }

        [Required(ErrorMessage = "Please fill in the field!")]
        [StringLength(300, MinimumLength = 5,
            ErrorMessage = "The ABOUT ME length must be between 5 and 300 characters!")]
        public string? AboutMe { get; set; }

        [Required(ErrorMessage = "Please enter email!")]
        [StringLength(30, MinimumLength = 5,
            ErrorMessage = "The EMAIL length must be between 5 and 30 characters!")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Please enter login!")]
        [StringLength(20, MinimumLength = 3,
            ErrorMessage = "The LOGIN length must be between 3 and 20 characters!")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Please enter password!")]
        [StringLength(20, MinimumLength = 3,
            ErrorMessage = "The PASSWORD length must be between 3 and 20 characters!")]
        public string? Password { get; set; }

        public byte[]? Avatar { get; set; }
    }
}