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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Question-Choice Relationship
            modelBuilder.Entity<Question>()
                .HasMany(q => q.Choices)
                .WithOne(c => c.Question)
                .HasForeignKey(c => c.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Question-UserAnswer Relationship
            modelBuilder.Entity<Question>()
                .HasMany(q => q.UserAnswers)
                .WithOne(ua => ua.Question)
                .HasForeignKey(ua => ua.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Choice-UserAnswer Relationship
            modelBuilder.Entity<Choice>()
                .HasMany(c => c.UserAnswers)
                .WithOne(ua => ua.Choice)
                .HasForeignKey(ua => ua.ChoiceId)
                .OnDelete(DeleteBehavior.Cascade);

            // User-UserAnswer Relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.UserAnswers)
                .WithOne(ua => ua.User)
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
