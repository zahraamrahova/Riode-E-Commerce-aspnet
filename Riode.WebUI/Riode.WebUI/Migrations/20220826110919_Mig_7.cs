using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Riode.WebUI.Migrations
{
    public partial class Mig_7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Asnwer",
                table: "ContactPosts");

            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "ContactPosts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Answer",
                table: "ContactPosts");

            migrationBuilder.AddColumn<string>(
                name: "Asnwer",
                table: "ContactPosts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
