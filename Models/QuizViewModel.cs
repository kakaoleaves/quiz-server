using QuizAPI_DotNet8.Entities;

namespace QuizAPI_DotNet8.Models
{
    public class QuizViewModel
    {
        public int QuestionId { get; set; }
        public required string Content { get; set; }
        public required List<Choice> Choices { get; set; }
    }
}
