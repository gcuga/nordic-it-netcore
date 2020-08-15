using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Reminder.Storage.SqlServer.EF.DbInit.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReminderItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ContactId = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    TargetDate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: false),
                    Message = table.Column<string>(maxLength: 200, nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReminderItem", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReminderItem_StatusId",
                table: "ReminderItem",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReminderItem");
        }
    }
}
