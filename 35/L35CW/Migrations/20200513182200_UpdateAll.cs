using Microsoft.EntityFrameworkCore.Migrations;

namespace L35CW.Migrations
{
    public partial class UpdateAll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "OrderItems",
                newName: "OrderItem",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Customer",
                newSchema: "dbo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                schema: "dbo",
                table: "Product",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                schema: "dbo",
                table: "Order",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItem",
                schema: "dbo",
                table: "OrderItem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                schema: "dbo",
                table: "Customer",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                schema: "dbo",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItem",
                schema: "dbo",
                table: "OrderItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                schema: "dbo",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                schema: "dbo",
                table: "Customer");

            migrationBuilder.RenameTable(
                name: "Product",
                schema: "dbo",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "OrderItem",
                schema: "dbo",
                newName: "OrderItems");

            migrationBuilder.RenameTable(
                name: "Order",
                schema: "dbo",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "Customer",
                schema: "dbo",
                newName: "Customers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");
        }
    }
}
