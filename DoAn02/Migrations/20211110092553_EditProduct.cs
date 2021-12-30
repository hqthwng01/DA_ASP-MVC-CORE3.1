using Microsoft.EntityFrameworkCore.Migrations;

namespace DoAn02.Migrations
{
    public partial class EditProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductTypesId",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypesId",
                table: "Products",
                column: "ProductTypesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductTypes_ProductTypesId",
                table: "Products",
                column: "ProductTypesId",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductTypes_ProductTypesId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductTypesId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductTypesId",
                table: "Products");
        }
    }
}
