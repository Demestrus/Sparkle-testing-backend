using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SparkleTesting.Persistence.Migrations
{
    public partial class PassFills : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswerStrings",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "AnswerStrings",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "UserAnswer",
                table: "Answers");

            migrationBuilder.CreateTable(
                name: "FilledPass",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserAnswer = table.Column<string>(nullable: true),
                    AnswerStrings = table.Column<string>(nullable: true),
                    PassFillingAnswerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilledPass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilledPass_Answers_PassFillingAnswerId",
                        column: x => x.PassFillingAnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PassFilling",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnswerStrings = table.Column<string>(nullable: true),
                    PassFillingQuestionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassFilling", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PassFilling_Questions_PassFillingQuestionId",
                        column: x => x.PassFillingQuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FilledPass_PassFillingAnswerId",
                table: "FilledPass",
                column: "PassFillingAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_PassFilling_PassFillingQuestionId",
                table: "PassFilling",
                column: "PassFillingQuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilledPass");

            migrationBuilder.DropTable(
                name: "PassFilling");

            migrationBuilder.AddColumn<string>(
                name: "AnswerStrings",
                table: "Questions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AnswerStrings",
                table: "Answers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserAnswer",
                table: "Answers",
                nullable: true);
        }
    }
}
