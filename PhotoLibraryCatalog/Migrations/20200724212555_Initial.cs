using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TuroPhoto.PhotoLibraryCatalog.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LibraryCatalog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComputerName = table.Column<string>(nullable: true),
                    DirectoryPath = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryCatalog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LibraryCatalogDirectory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(nullable: true),
                    LibraryCatalogId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryCatalogDirectory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LibraryCatalogDirectory_LibraryCatalog_LibraryCatalogId",
                        column: x => x.LibraryCatalogId,
                        principalTable: "LibraryCatalog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Photo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(nullable: true),
                    DateTimeFromMetaData = table.Column<DateTime>(nullable: true),
                    DateTimeFromFile = table.Column<DateTime>(nullable: false),
                    LibraryCatalogDirectoryId = table.Column<int>(nullable: true),
                    LibraryCatalogId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photo_LibraryCatalogDirectory_LibraryCatalogDirectoryId",
                        column: x => x.LibraryCatalogDirectoryId,
                        principalTable: "LibraryCatalogDirectory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Photo_LibraryCatalog_LibraryCatalogId",
                        column: x => x.LibraryCatalogId,
                        principalTable: "LibraryCatalog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LibraryCatalogDirectory_LibraryCatalogId",
                table: "LibraryCatalogDirectory",
                column: "LibraryCatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_Photo_LibraryCatalogDirectoryId",
                table: "Photo",
                column: "LibraryCatalogDirectoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Photo_LibraryCatalogId",
                table: "Photo",
                column: "LibraryCatalogId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photo");

            migrationBuilder.DropTable(
                name: "LibraryCatalogDirectory");

            migrationBuilder.DropTable(
                name: "LibraryCatalog");
        }
    }
}
