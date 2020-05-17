using Microsoft.EntityFrameworkCore.Migrations;

namespace L34_C01_working_with_ef_core.Migrations
{
    public partial class OrderItemUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Order_OrderId",
                schema: "dbo",
                table: "OrderItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItem",
                schema: "dbo",
                table: "OrderItem");

            migrationBuilder.DropIndex(
                name: "IX_OrderItem_OrderId",
                schema: "dbo",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "dbo",
                table: "OrderItem");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                schema: "dbo",
                table: "OrderItem",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItem",
                schema: "dbo",
                table: "OrderItem",
                columns: new[] { "OrderId", "ProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Order_OrderId",
                schema: "dbo",
                table: "OrderItem",
                column: "OrderId",
                principalSchema: "dbo",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Order_OrderId",
                schema: "dbo",
                table: "OrderItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItem",
                schema: "dbo",
                table: "OrderItem");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                schema: "dbo",
                table: "OrderItem",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                schema: "dbo",
                table: "OrderItem",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItem",
                schema: "dbo",
                table: "OrderItem",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                schema: "dbo",
                table: "OrderItem",
                column: "OrderId");

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
    }
}
