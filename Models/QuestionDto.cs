using QuizAPI_DotNet8.Entities;

namespace QuizAPI_DotNet8.Models
{
    public class CreateQuestionDto
    {
        public string Content { get; set; }
        public List<CreateChoiceDto> Choices { get; set; }
        public int CreatedBy { get; set; }
    }

    public class GetQuestionDto
    {
        public int QuestionId { get; set; }
        public string Content { get; set; }
        public List<ChoiceDto> Choices { get; set; }
        public DateOnly DateCreated { get; set; }
        public UserSummaryDto Creator { get; set; }
    }

    public class PutQuestionDto
    {
        public string Content { get; set; }
        public List<ChoiceDto> Choices { get; set; }
    }

    public class QuestionAnswerDto
    {
        public int QuestionId { get; set; }
        public string Content { get; set; }
        public List<ChoiceDto> Choices { get; set; }
        public DateOnly DateCreated { get; set; }
        public UserSummaryDto Creator { get; set; }
        public UserAnswerDto UserAnswer { get; set; }
    }
    public class UserAnswerDto
    {
        public int UserAnswerId { get; set; }
        public int UserId { get; set; }
        public int ChoiceId { get; set; }
        public int QuestionId { get; set; }
        public bool IsCorrect { get; set; }
    }

    public class PostAnswerDto
    {
        public int UserId { get; set; }
        public int ChoiceId { get; set; }
        public int QuestionId { get; set; }
    }
}
