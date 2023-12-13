using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizAPI_DotNet8.Entities
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public int CreatedBy { get; set; }
        [Required]
        public DateOnly DateCreated { get; set; }

        // Navigation properties
        public virtual ICollection<Choice> Choices { get; set; }
        public virtual ICollection<UserAnswer> UserAnswers { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual User Creator { get; set; }
    }
}