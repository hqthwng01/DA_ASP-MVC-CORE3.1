using Microsoft.EntityFrameworkCore.Migrations;

namespace DoAn02.Migrations
{
    public partial class editInvoice_AddFullname_AddEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Invoices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fullname",
                table: "Invoices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Fullname",
                table: "Invoices");
        }
    }
}
