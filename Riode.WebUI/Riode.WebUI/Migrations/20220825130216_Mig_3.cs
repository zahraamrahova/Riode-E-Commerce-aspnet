using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Riode.WebUI.Migrations
{
    public partial class Mig_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CatgoryId",
                table: "SpecificationCategoryCollection");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CatgoryId",
                table: "SpecificationCategoryCollection",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
