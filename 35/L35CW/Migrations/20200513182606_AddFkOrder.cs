using Microsoft.EntityFrameworkCore.Migrations;

namespace L35CW.Migrations
{
    public partial class AddFkOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Order_OrderId",
                schema: "dbo",
                table: "OrderItem");

            migrationBuilder.DropIndex(
                name: "IX_OrderItem_OrderId",
                schema: "dbo",
                table: "OrderItem");
        }
    }
}
