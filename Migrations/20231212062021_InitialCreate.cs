using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizAPI_DotNet8.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "username",
                table: "Users",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "isAdmin",
                table: "Users",
                newName: "IsAdmin");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Users",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "userAnswerId",
                table: "UserAnswers",
                newName: "UserAnswerId");

            migrationBuilder.RenameColumn(
                name: "questionId",
                table: "Questions",
                newName: "QuestionId");

            migrationBuilder.RenameColumn(
                name: "choiceId",
                table: "Choices",
                newName: "ChoiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Users",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "IsAdmin",
                table: "Users",
                newName: "isAdmin");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Users",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "UserAnswerId",
                table: "UserAnswers",
                newName: "userAnswerId");

            migrationBuilder.RenameColumn(
                name: "QuestionId",
                table: "Questions",
                newName: "questionId");

            migrationBuilder.RenameColumn(
                name: "ChoiceId",
                table: "Choices",
                newName: "choiceId");
        }
    }
}
