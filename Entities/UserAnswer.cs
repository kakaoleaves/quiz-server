namespace QuizAPI_DotNet8.Entities
{
    public class UserAnswer
    {
        public int userAnswerId { get; set; } // primary key
        public required int UserId { get; set; } // foreign key
        public required int QuestionId { get; set; } // foreign key
        public required int ChoiceId { get; set; } // foreign key
        public required DateOnly DateAnswered { get; set; }
    }
}
