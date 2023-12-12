using Microsoft.EntityFrameworkCore;
using QuizAPI_DotNet8.Entities;

namespace QuizAPI_DotNet8.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Choice> Choices { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }
    }
}
