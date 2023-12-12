namespace QuizAPI_DotNet8.Entities
{
    public class User
    {
        public int UserId { get; set; } // primary key
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required bool IsAdmin { get; set; }
    }
}