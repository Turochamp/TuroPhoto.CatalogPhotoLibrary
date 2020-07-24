using Microsoft.EntityFrameworkCore.Migrations;

namespace Turochamp.Photo.Migrations
{
    public partial class FileName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Photos");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Photos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Photos");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Photos",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
