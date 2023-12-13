using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizAPI_DotNet8.Migrations
{
    /// <inheritdoc />
    public partial class InitForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatorUserId",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswers_ChoiceId",
                table: "UserAnswers",
                column: "ChoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswers_QuestionId",
                table: "UserAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswers_UserId",
                table: "UserAnswers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_CreatorUserId",
                table: "Questions",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Choices_QuestionId",
                table: "Choices",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Choices_Questions_QuestionId",
                table: "Choices",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Users_CreatorUserId",
                table: "Questions",
                column: "CreatorUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswers_Choices_ChoiceId",
                table: "UserAnswers",
                column: "ChoiceId",
                principalTable: "Choices",
                principalColumn: "ChoiceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswers_Questions_QuestionId",
                table: "UserAnswers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswers_Users_UserId",
                table: "UserAnswers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Choices_Questions_QuestionId",
                table: "Choices");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Users_CreatorUserId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswers_Choices_ChoiceId",
                table: "UserAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswers_Questions_QuestionId",
                table: "UserAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswers_Users_UserId",
                table: "UserAnswers");

            migrationBuilder.DropIndex(
                name: "IX_UserAnswers_ChoiceId",
                table: "UserAnswers");

            migrationBuilder.DropIndex(
                name: "IX_UserAnswers_QuestionId",
                table: "UserAnswers");

            migrationBuilder.DropIndex(
                name: "IX_UserAnswers_UserId",
                table: "UserAnswers");

            migrationBuilder.DropIndex(
                name: "IX_Questions_CreatorUserId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Choices_QuestionId",
                table: "Choices");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Questions");
        }
    }
}
