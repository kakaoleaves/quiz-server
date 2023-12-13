namespace QuizAPI_DotNet8.Models
{
    public class CreateChoiceDto
    {
        public string Content { get; set; }
        public bool IsCorrect { get; set; }
    }
    public class ChoiceDto
    {
        public int ChoiceId { get; set; }
        public string Content { get; set; }
        public bool IsCorrect { get; set; }
    }
}
