using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Turochamp.Photo.Migrations
{
    public partial class DateTimeFromMetaData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeFromMetaData",
                table: "Photos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTimeFromMetaData",
                table: "Photos");
        }
    }
}
