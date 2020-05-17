using Microsoft.EntityFrameworkCore.Migrations;

namespace L34_C01_working_with_ef_core.Migrations
{
    public partial class BigRefactoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems");

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

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_OrderId",
                schema: "dbo",
                table: "OrderItem",
                newName: "IX_OrderItem_OrderId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "dbo",
                table: "Customer",
                type: "VARCHAR(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

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

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Order_OrderId",
                schema: "dbo",
                table: "OrderItem",
                column: "OrderId",
                principalSchema: "dbo",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Order_OrderId",
                schema: "dbo",
                table: "OrderItem");

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

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItems",
                newName: "IX_OrderItems_OrderId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Customers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(50)");

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

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
