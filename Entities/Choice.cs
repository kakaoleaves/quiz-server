using System.ComponentModel.DataAnnotations;

namespace QuizAPI_DotNet8.Entities
{
    public class Choice
    {
        [Key]
        public int ChoiceId { get; set; }
        [Required]
        public int QuestionId { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public bool IsCorrect { get; set; }

        // Navigation properties
        public virtual Question Question { get; set; }
        public virtual ICollection<UserAnswer> UserAnswers { get; set; }
    }
}
