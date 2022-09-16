using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Riode.WebUI.Migrations
{
    public partial class MembershipAddRoleAsSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "Membership",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 1, "87e6cf82-a5de-413a-8ae7-c8a0bf93e98b", "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.InsertData(
                schema: "Membership",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 2, "fb98b010-e90c-4e74-8aa8-b30e444066d8", "Operator", "OPERATOR" });

            migrationBuilder.InsertData(
                schema: "Membership",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 3, "198ea085-27b0-463c-aa15-40beb8f9848d", "Reporter", "REPORTER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Membership",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "Membership",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "Membership",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
