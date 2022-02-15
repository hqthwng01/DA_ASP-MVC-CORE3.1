using Microsoft.EntityFrameworkCore.Migrations;

namespace DoAn02.Migrations
{
    public partial class EditInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductItemsId",
                table: "Invoices",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ProductItemsId",
                table: "Invoices",
                column: "ProductItemsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Products_ProductItemsId",
                table: "Invoices",
                column: "ProductItemsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Products_ProductItemsId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_ProductItemsId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ProductItemsId",
                table: "Invoices");
        }
    }
}
