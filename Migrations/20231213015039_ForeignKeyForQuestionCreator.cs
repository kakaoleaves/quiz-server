using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizAPI_DotNet8.Migrations
{
    /// <inheritdoc />
    public partial class ForeignKeyForQuestionCreator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Users_CreatorUserId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_CreatorUserId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Questions");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_CreatedBy",
                table: "Questions",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Users_CreatedBy",
                table: "Questions",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Users_CreatedBy",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_CreatedBy",
                table: "Questions");

            migrationBuilder.AddColumn<int>(
                name: "CreatorUserId",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_CreatorUserId",
                table: "Questions",
                column: "CreatorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Users_CreatorUserId",
                table: "Questions",
                column: "CreatorUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
