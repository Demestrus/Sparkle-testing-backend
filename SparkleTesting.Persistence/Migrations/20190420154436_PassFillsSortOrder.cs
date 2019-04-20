using Microsoft.EntityFrameworkCore.Migrations;

namespace SparkleTesting.Persistence.Migrations
{
    public partial class PassFillsSortOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "PassFilling",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "FilledPass",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "PassFilling");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "FilledPass");
        }
    }
}
