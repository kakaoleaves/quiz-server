using System.ComponentModel.DataAnnotations;

namespace QuizAPI_DotNet8.Entities
{
    public class UserAnswer
    {
        [Key]
        public int UserAnswerId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int QuestionId { get; set; }
        [Required]
        public int ChoiceId { get; set; }
        [Required]
        public required bool IsCorrect { get; set; }
        [Required]
        public required DateOnly DateAnswered { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public virtual Question Question { get; set; }        
        public virtual Choice Choice { get; set; }
    }
}