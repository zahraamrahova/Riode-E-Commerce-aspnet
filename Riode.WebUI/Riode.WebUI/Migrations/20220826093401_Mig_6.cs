using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Riode.WebUI.Migrations
{
    public partial class Mig_6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_contactPosts",
                table: "contactPosts");

            migrationBuilder.RenameTable(
                name: "contactPosts",
                newName: "ContactPosts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactPosts",
                table: "ContactPosts",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactPosts",
                table: "ContactPosts");

            migrationBuilder.RenameTable(
                name: "ContactPosts",
                newName: "contactPosts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_contactPosts",
                table: "contactPosts",
                column: "Id");
        }
    }
}
