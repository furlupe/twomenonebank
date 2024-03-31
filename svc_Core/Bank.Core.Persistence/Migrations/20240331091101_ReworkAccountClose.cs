using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bank.Core.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ReworkAccountClose : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DefaultTransferAccountId",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ClosedAt",
                table: "Accounts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_DefaultTransferAccountId",
                table: "Users",
                column: "DefaultTransferAccountId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Accounts_DefaultTransferAccountId",
                table: "Users",
                column: "DefaultTransferAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Accounts_DefaultTransferAccountId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_DefaultTransferAccountId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DefaultTransferAccountId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ClosedAt",
                table: "Accounts");
        }
    }
}
