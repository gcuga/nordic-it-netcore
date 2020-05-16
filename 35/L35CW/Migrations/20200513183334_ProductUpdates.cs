using Microsoft.EntityFrameworkCore.Migrations;

namespace L35CW.Migrations
{
    public partial class ProductUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "dbo",
                table: "Product",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "UQ_Product_Name",
                schema: "dbo",
                table: "Product",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Price",
                schema: "dbo",
                table: "Product",
                column: "Price");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "UQ_Product_Name",
                schema: "dbo",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_Price",
                schema: "dbo",
                table: "Product");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "dbo",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
