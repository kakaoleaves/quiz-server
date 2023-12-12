using System.ComponentModel.DataAnnotations;

namespace QuizAPI_DotNet8.Models
{
    public class UserViewModel
    {
        [Required]
        [RegularExpression("^[a-zA-Z\\d]{5,12}$", ErrorMessage = "Username must be 5-12 characters long and start with a letter.")]
        public required string Username { get; set; }

        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{8,}$", ErrorMessage = "Password must be 8 characters long, contain at least one uppercase letter, one lowercase letter and one number.")]
        public required string Password { get; set; }
    }
}
