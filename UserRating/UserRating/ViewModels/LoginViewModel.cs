using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UserRating.ViewModels
{
    public class LogInViewModel
    {
        [MinLength(3)]
        [StringLength(20)]
        [Required(ErrorMessage = "Please enter login...")]
        public string? Login { get; set; }

        [MinLength(3)]
        [StringLength(20)]
        [Required(ErrorMessage = "Please enter password...")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}