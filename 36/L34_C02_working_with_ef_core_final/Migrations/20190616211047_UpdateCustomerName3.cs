using Microsoft.EntityFrameworkCore.Migrations;

namespace L34_C02_working_with_ef_core_final.Migrations
{
    public partial class UpdateCustomerName3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "UQ_Customers_Name",
                table: "Customers",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "UQ_Customers_Name",
                table: "Customers");
        }
    }
}
