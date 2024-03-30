using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bank.Core.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTransfers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountEvent_Accounts_AccountId",
                table: "AccountEvent");

            migrationBuilder.DropIndex(
                name: "IX_AccountEvent_AccountId",
                table: "AccountEvent");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "AccountEvent");

            migrationBuilder.DropColumn(
                name: "Transfer_Id",
                table: "AccountEvent");

            migrationBuilder.AddColumn<int>(
                name: "Transfer_Type",
                table: "AccountEvent",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AccountAccountEvent",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    EventsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountAccountEvent", x => new { x.AccountId, x.EventsId });
                    table.ForeignKey(
                        name: "FK_AccountAccountEvent_AccountEvent_EventsId",
                        column: x => x.EventsId,
                        principalTable: "AccountEvent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountAccountEvent_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountAccountEvent_EventsId",
                table: "AccountAccountEvent",
                column: "EventsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountAccountEvent");

            migrationBuilder.DropColumn(
                name: "Transfer_Type",
                table: "AccountEvent");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "AccountEvent",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Transfer_Id",
                table: "AccountEvent",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountEvent_AccountId",
                table: "AccountEvent",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountEvent_Accounts_AccountId",
                table: "AccountEvent",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
