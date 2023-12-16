using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizAPI_DotNet8.Migrations
{
    /// <inheritdoc />
    public partial class IsCorrectInUserAnswer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCorrect",
                table: "UserAnswers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCorrect",
                table: "UserAnswers");
        }
    }
}
