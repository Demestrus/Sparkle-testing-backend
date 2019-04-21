using Microsoft.EntityFrameworkCore.Migrations;

namespace SparkleTesting.Persistence.Migrations
{
    public partial class PassFillingFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PassFillingId",
                table: "FilledPass",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PassFillingId",
                table: "FilledPass");
        }
    }
}
