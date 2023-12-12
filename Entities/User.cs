namespace QuizAPI_DotNet8.Entities
{
    public class User
    {
        public int userId { get; set; } // primary key
        public required string username { get; set; }
        public required string password { get; set; }
        public required bool isAdmin { get; set; }
    }
}