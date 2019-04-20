using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SparkleTesting.Persistence.Migrations
{
    public partial class TestAttempts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Correct",
                table: "Options",
                newName: "IsCorrect");

            migrationBuilder.CreateTable(
                name: "Attempts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    Assigned = table.Column<string>(nullable: true),
                    TestId = table.Column<int>(nullable: false),
                    MaxTime = table.Column<TimeSpan>(nullable: true),
                    ElapsedTime = table.Column<TimeSpan>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: true),
                    PointsAcquired = table.Column<int>(nullable: true),
                    MarkId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attempts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attempts_TestMarks_MarkId",
                        column: x => x.MarkId,
                        principalTable: "TestMarks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attempts_Test_TestId",
                        column: x => x.TestId,
                        principalTable: "Test",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attempts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AttemptId = table.Column<int>(nullable: false),
                    QuestionId = table.Column<int>(nullable: false),
                    QuestionText = table.Column<string>(nullable: true),
                    QuestionPoints = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    UserAnswer = table.Column<string>(nullable: true),
                    AnswerStrings = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Attempts_AttemptId",
                        column: x => x.AttemptId,
                        principalTable: "Attempts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnswerOptions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnswerId = table.Column<int>(nullable: false),
                    OptionId = table.Column<int>(nullable: false),
                    OptionText = table.Column<string>(nullable: true),
                    IsCorrect = table.Column<bool>(nullable: false),
                    IsChoosen = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswerOptions_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerOptions_AnswerId_OptionId",
                table: "AnswerOptions",
                columns: new[] { "AnswerId", "OptionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answers_AttemptId_QuestionId",
                table: "Answers",
                columns: new[] { "AttemptId", "QuestionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attempts_MarkId",
                table: "Attempts",
                column: "MarkId");

            migrationBuilder.CreateIndex(
                name: "IX_Attempts_TestId",
                table: "Attempts",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_Attempts_UserId",
                table: "Attempts",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswerOptions");

            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Attempts");

            migrationBuilder.RenameColumn(
                name: "IsCorrect",
                table: "Options",
                newName: "Correct");
        }
    }
}
