using System.ComponentModel.DataAnnotations;
using UserRating.Enums;

namespace UserRating.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Age { get; set; }
        public string? City { get; set; }
        public string? AboutMe { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public byte[]? Avatar { get; set; }
        public Role Role { get; set; }
    }
}