using Microsoft.EntityFrameworkCore.Migrations;

namespace SparkleTesting.Persistence.Migrations
{
    public partial class QuestionOptionFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_Questions_OptionsQuestionId",
                table: "Options");

            migrationBuilder.DropIndex(
                name: "IX_Options_OptionsQuestionId",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "OptionsQuestionId",
                table: "Options");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OptionsQuestionId",
                table: "Options",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Options_OptionsQuestionId",
                table: "Options",
                column: "OptionsQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Questions_OptionsQuestionId",
                table: "Options",
                column: "OptionsQuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
