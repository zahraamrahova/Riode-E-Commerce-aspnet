using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Riode.WebUI.Migrations
{
    public partial class BaseEntityCreateandDeleteUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Membership",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "a455d617-0c35-454c-b61d-2939b8457041");

            migrationBuilder.UpdateData(
                schema: "Membership",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "91383f7b-3ea4-4e8d-b98f-389e1d8f0b07");

            migrationBuilder.UpdateData(
                schema: "Membership",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "2cff7e0a-7d6b-4c05-8433-887af7b231ce");

            migrationBuilder.CreateIndex(
                name: "IX_Subscribes_CreatedByUserId",
                table: "Subscribes",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscribes_DeletedByUserId",
                table: "Subscribes",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecificationValues_CreatedByUserId",
                table: "SpecificationValues",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecificationValues_DeletedByUserId",
                table: "SpecificationValues",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Specifications_CreatedByUserId",
                table: "Specifications",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Specifications_DeletedByUserId",
                table: "Specifications",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecificationCategoryCollection_CreatedByUserId",
                table: "SpecificationCategoryCollection",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecificationCategoryCollection_DeletedByUserId",
                table: "SpecificationCategoryCollection",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSizes_CreatedByUserId",
                table: "ProductSizes",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSizes_DeletedByUserId",
                table: "ProductSizes",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSizeColorCollection_CreatedByUserId",
                table: "ProductSizeColorCollection",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSizeColorCollection_DeletedByUserId",
                table: "ProductSizeColorCollection",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CreatedByUserId",
                table: "Products",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_DeletedByUserId",
                table: "Products",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_CreatedByUserId",
                table: "ProductImages",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_DeletedByUserId",
                table: "ProductImages",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductColors_CreatedByUserId",
                table: "ProductColors",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductColors_DeletedByUserId",
                table: "ProductColors",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Faqs_CreatedByUserId",
                table: "Faqs",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Faqs_DeletedByUserId",
                table: "Faqs",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPosts_CreatedByUserId",
                table: "ContactPosts",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPosts_DeletedByUserId",
                table: "ContactPosts",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CreatedByUserId",
                table: "Categories",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_DeletedByUserId",
                table: "Categories",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_CreatedByUserId",
                table: "Brands",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_DeletedByUserId",
                table: "Brands",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_CreatedByUserId",
                table: "BlogPosts",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_DeletedByUserId",
                table: "BlogPosts",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_CreatedByUserId",
                table: "AuditLogs",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_DeletedByUserId",
                table: "AuditLogs",
                column: "DeletedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditLogs_Users_CreatedByUserId",
                table: "AuditLogs",
                column: "CreatedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditLogs_Users_DeletedByUserId",
                table: "AuditLogs",
                column: "DeletedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_Users_CreatedByUserId",
                table: "BlogPosts",
                column: "CreatedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_Users_DeletedByUserId",
                table: "BlogPosts",
                column: "DeletedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_Users_CreatedByUserId",
                table: "Brands",
                column: "CreatedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_Users_DeletedByUserId",
                table: "Brands",
                column: "DeletedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Users_CreatedByUserId",
                table: "Categories",
                column: "CreatedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Users_DeletedByUserId",
                table: "Categories",
                column: "DeletedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactPosts_Users_CreatedByUserId",
                table: "ContactPosts",
                column: "CreatedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactPosts_Users_DeletedByUserId",
                table: "ContactPosts",
                column: "DeletedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Faqs_Users_CreatedByUserId",
                table: "Faqs",
                column: "CreatedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Faqs_Users_DeletedByUserId",
                table: "Faqs",
                column: "DeletedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColors_Users_CreatedByUserId",
                table: "ProductColors",
                column: "CreatedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColors_Users_DeletedByUserId",
                table: "ProductColors",
                column: "DeletedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Users_CreatedByUserId",
                table: "ProductImages",
                column: "CreatedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Users_DeletedByUserId",
                table: "ProductImages",
                column: "DeletedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_CreatedByUserId",
                table: "Products",
                column: "CreatedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_DeletedByUserId",
                table: "Products",
                column: "DeletedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSizeColorCollection_Users_CreatedByUserId",
                table: "ProductSizeColorCollection",
                column: "CreatedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSizeColorCollection_Users_DeletedByUserId",
                table: "ProductSizeColorCollection",
                column: "DeletedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSizes_Users_CreatedByUserId",
                table: "ProductSizes",
                column: "CreatedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSizes_Users_DeletedByUserId",
                table: "ProductSizes",
                column: "DeletedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecificationCategoryCollection_Users_CreatedByUserId",
                table: "SpecificationCategoryCollection",
                column: "CreatedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecificationCategoryCollection_Users_DeletedByUserId",
                table: "SpecificationCategoryCollection",
                column: "DeletedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Specifications_Users_CreatedByUserId",
                table: "Specifications",
                column: "CreatedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Specifications_Users_DeletedByUserId",
                table: "Specifications",
                column: "DeletedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecificationValues_Users_CreatedByUserId",
                table: "SpecificationValues",
                column: "CreatedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecificationValues_Users_DeletedByUserId",
                table: "SpecificationValues",
                column: "DeletedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscribes_Users_CreatedByUserId",
                table: "Subscribes",
                column: "CreatedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscribes_Users_DeletedByUserId",
                table: "Subscribes",
                column: "DeletedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditLogs_Users_CreatedByUserId",
                table: "AuditLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_AuditLogs_Users_DeletedByUserId",
                table: "AuditLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_Users_CreatedByUserId",
                table: "BlogPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_Users_DeletedByUserId",
                table: "BlogPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_Brands_Users_CreatedByUserId",
                table: "Brands");

            migrationBuilder.DropForeignKey(
                name: "FK_Brands_Users_DeletedByUserId",
                table: "Brands");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Users_CreatedByUserId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Users_DeletedByUserId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactPosts_Users_CreatedByUserId",
                table: "ContactPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactPosts_Users_DeletedByUserId",
                table: "ContactPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_Faqs_Users_CreatedByUserId",
                table: "Faqs");

            migrationBuilder.DropForeignKey(
                name: "FK_Faqs_Users_DeletedByUserId",
                table: "Faqs");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductColors_Users_CreatedByUserId",
                table: "ProductColors");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductColors_Users_DeletedByUserId",
                table: "ProductColors");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Users_CreatedByUserId",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Users_DeletedByUserId",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_CreatedByUserId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_DeletedByUserId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSizeColorCollection_Users_CreatedByUserId",
                table: "ProductSizeColorCollection");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSizeColorCollection_Users_DeletedByUserId",
                table: "ProductSizeColorCollection");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSizes_Users_CreatedByUserId",
                table: "ProductSizes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSizes_Users_DeletedByUserId",
                table: "ProductSizes");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecificationCategoryCollection_Users_CreatedByUserId",
                table: "SpecificationCategoryCollection");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecificationCategoryCollection_Users_DeletedByUserId",
                table: "SpecificationCategoryCollection");

            migrationBuilder.DropForeignKey(
                name: "FK_Specifications_Users_CreatedByUserId",
                table: "Specifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Specifications_Users_DeletedByUserId",
                table: "Specifications");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecificationValues_Users_CreatedByUserId",
                table: "SpecificationValues");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecificationValues_Users_DeletedByUserId",
                table: "SpecificationValues");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscribes_Users_CreatedByUserId",
                table: "Subscribes");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscribes_Users_DeletedByUserId",
                table: "Subscribes");

            migrationBuilder.DropIndex(
                name: "IX_Subscribes_CreatedByUserId",
                table: "Subscribes");

            migrationBuilder.DropIndex(
                name: "IX_Subscribes_DeletedByUserId",
                table: "Subscribes");

            migrationBuilder.DropIndex(
                name: "IX_SpecificationValues_CreatedByUserId",
                table: "SpecificationValues");

            migrationBuilder.DropIndex(
                name: "IX_SpecificationValues_DeletedByUserId",
                table: "SpecificationValues");

            migrationBuilder.DropIndex(
                name: "IX_Specifications_CreatedByUserId",
                table: "Specifications");

            migrationBuilder.DropIndex(
                name: "IX_Specifications_DeletedByUserId",
                table: "Specifications");

            migrationBuilder.DropIndex(
                name: "IX_SpecificationCategoryCollection_CreatedByUserId",
                table: "SpecificationCategoryCollection");

            migrationBuilder.DropIndex(
                name: "IX_SpecificationCategoryCollection_DeletedByUserId",
                table: "SpecificationCategoryCollection");

            migrationBuilder.DropIndex(
                name: "IX_ProductSizes_CreatedByUserId",
                table: "ProductSizes");

            migrationBuilder.DropIndex(
                name: "IX_ProductSizes_DeletedByUserId",
                table: "ProductSizes");

            migrationBuilder.DropIndex(
                name: "IX_ProductSizeColorCollection_CreatedByUserId",
                table: "ProductSizeColorCollection");

            migrationBuilder.DropIndex(
                name: "IX_ProductSizeColorCollection_DeletedByUserId",
                table: "ProductSizeColorCollection");

            migrationBuilder.DropIndex(
                name: "IX_Products_CreatedByUserId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_DeletedByUserId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_ProductImages_CreatedByUserId",
                table: "ProductImages");

            migrationBuilder.DropIndex(
                name: "IX_ProductImages_DeletedByUserId",
                table: "ProductImages");

            migrationBuilder.DropIndex(
                name: "IX_ProductColors_CreatedByUserId",
                table: "ProductColors");

            migrationBuilder.DropIndex(
                name: "IX_ProductColors_DeletedByUserId",
                table: "ProductColors");

            migrationBuilder.DropIndex(
                name: "IX_Faqs_CreatedByUserId",
                table: "Faqs");

            migrationBuilder.DropIndex(
                name: "IX_Faqs_DeletedByUserId",
                table: "Faqs");

            migrationBuilder.DropIndex(
                name: "IX_ContactPosts_CreatedByUserId",
                table: "ContactPosts");

            migrationBuilder.DropIndex(
                name: "IX_ContactPosts_DeletedByUserId",
                table: "ContactPosts");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CreatedByUserId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_DeletedByUserId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Brands_CreatedByUserId",
                table: "Brands");

            migrationBuilder.DropIndex(
                name: "IX_Brands_DeletedByUserId",
                table: "Brands");

            migrationBuilder.DropIndex(
                name: "IX_BlogPosts_CreatedByUserId",
                table: "BlogPosts");

            migrationBuilder.DropIndex(
                name: "IX_BlogPosts_DeletedByUserId",
                table: "BlogPosts");

            migrationBuilder.DropIndex(
                name: "IX_AuditLogs_CreatedByUserId",
                table: "AuditLogs");

            migrationBuilder.DropIndex(
                name: "IX_AuditLogs_DeletedByUserId",
                table: "AuditLogs");

            migrationBuilder.UpdateData(
                schema: "Membership",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "ea01cf92-8661-40fe-a8fe-ec22bdc84191");

            migrationBuilder.UpdateData(
                schema: "Membership",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "46f54f65-ffc6-4e78-9d7b-df914cc86c92");

            migrationBuilder.UpdateData(
                schema: "Membership",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "4cc063cf-4683-496d-854a-4b7f5028e944");
        }
    }
}
