using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Riode.WebUI.Migrations
{
    public partial class BlogPostCommentsCollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Membership",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "85b8ab04-8081-4562-a6e7-6c8b7156842b");

            migrationBuilder.UpdateData(
                schema: "Membership",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "6516b10d-b492-46b5-80a1-d9f84ae49737");

            migrationBuilder.UpdateData(
                schema: "Membership",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "ba1a636c-3573-4733-9e82-fbfc8d493780");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Membership",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "f6e3e402-def6-4f4b-8cc6-523805a868ed");

            migrationBuilder.UpdateData(
                schema: "Membership",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "be7e1d5d-959f-492d-b110-3fe8c25d18a3");

            migrationBuilder.UpdateData(
                schema: "Membership",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "0c444904-76f3-43aa-83d0-d9e6a087ffd1");
        }
    }
}
