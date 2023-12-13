using System.ComponentModel.DataAnnotations;

namespace QuizAPI_DotNet8.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public required string Username { get; set; }
        [Required]
        public required string Password { get; set; }
        [Required]
        public required bool IsAdmin { get; set; }

        // Navigation properties
        public virtual ICollection<UserAnswer> UserAnswers { get; set; }
    }
}