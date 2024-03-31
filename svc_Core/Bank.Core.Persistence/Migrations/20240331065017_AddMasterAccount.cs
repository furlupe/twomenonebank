using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bank.Core.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddMasterAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BalanceChange_CreditPayment_CreditId",
                table: "AccountEvent");

            migrationBuilder.DropColumn(
                name: "Transfer_Source_CreditPayment_CreditId",
                table: "AccountEvent");

            migrationBuilder.RenameColumn(
                name: "Transfer_Target_CreditPayment_CreditId",
                table: "AccountEvent",
                newName: "Transfer_CreditTransfer_CreditId");

            migrationBuilder.AddColumn<bool>(
                name: "IsMaster",
                table: "Accounts",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Transfer_CreditTransfer_Type",
                table: "AccountEvent",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMaster",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Transfer_CreditTransfer_Type",
                table: "AccountEvent");

            migrationBuilder.RenameColumn(
                name: "Transfer_CreditTransfer_CreditId",
                table: "AccountEvent",
                newName: "Transfer_Target_CreditPayment_CreditId");

            migrationBuilder.AddColumn<Guid>(
                name: "BalanceChange_CreditPayment_CreditId",
                table: "AccountEvent",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Transfer_Source_CreditPayment_CreditId",
                table: "AccountEvent",
                type: "uuid",
                nullable: true);
        }
    }
}
