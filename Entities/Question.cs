namespace QuizAPI_DotNet8.Entities
{
    public class Question
    {
        public int questionId { get; set; } // primary key
        public required string Content { get; set; }
        public required int CreatedBy { get; set; } // foreign key
        public required DateOnly DateCreated { get; set; }
    }
}
