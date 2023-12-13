using System.ComponentModel.DataAnnotations;

namespace QuizAPI_DotNet8.Models
{
    public class UserLoginDto
    {
        [Required]
        [RegularExpression("^[a-zA-Z\\d]{5,12}$", ErrorMessage = "Username must be 5-12 characters long and start with a letter.")]
        public string Username { get; set; }

        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{8,}$", ErrorMessage = "Password must be 8 characters long, contain at least one uppercase letter, one lowercase letter and one number.")]
        public string Password { get; set; }
    }

    public class UserSummaryDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
    }
}
