namespace QuizAPI_DotNet8.Entities
{
    public class Choice
    {
        public int ChoiceId { get; set; } // primary key
        public required int QuestionId { get; set; } // foreign key
        public required string Content { get; set; }
        public required bool IsCorrect { get; set; }
    }
}
