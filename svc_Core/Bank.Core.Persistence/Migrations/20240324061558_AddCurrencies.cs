using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bank.Core.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCurrencies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "BalanceChange_Value",
                table: "AccountEvent");

            migrationBuilder.DropColumn(
                name: "Transfer_Source_Value",
                table: "AccountEvent");

            migrationBuilder.DropColumn(
                name: "Transfer_Target_Value",
                table: "AccountEvent");

            migrationBuilder.AddColumn<decimal>(
                name: "Balance_Amount",
                table: "Accounts",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Balance_Currency",
                table: "Accounts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "BalanceChange_ForeignValue_Amount",
                table: "AccountEvent",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BalanceChange_ForeignValue_Currency",
                table: "AccountEvent",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "BalanceChange_NativeValue_Amount",
                table: "AccountEvent",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BalanceChange_NativeValue_Currency",
                table: "AccountEvent",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Transfer_Source_ForeignValue_Amount",
                table: "AccountEvent",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Transfer_Source_ForeignValue_Currency",
                table: "AccountEvent",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Transfer_Source_NativeValue_Amount",
                table: "AccountEvent",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Transfer_Source_NativeValue_Currency",
                table: "AccountEvent",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Transfer_Target_ForeignValue_Amount",
                table: "AccountEvent",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Transfer_Target_ForeignValue_Currency",
                table: "AccountEvent",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Transfer_Target_NativeValue_Amount",
                table: "AccountEvent",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Transfer_Target_NativeValue_Currency",
                table: "AccountEvent",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balance_Amount",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Balance_Currency",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "BalanceChange_ForeignValue_Amount",
                table: "AccountEvent");

            migrationBuilder.DropColumn(
                name: "BalanceChange_ForeignValue_Currency",
                table: "AccountEvent");

            migrationBuilder.DropColumn(
                name: "BalanceChange_NativeValue_Amount",
                table: "AccountEvent");

            migrationBuilder.DropColumn(
                name: "BalanceChange_NativeValue_Currency",
                table: "AccountEvent");

            migrationBuilder.DropColumn(
                name: "Transfer_Source_ForeignValue_Amount",
                table: "AccountEvent");

            migrationBuilder.DropColumn(
                name: "Transfer_Source_ForeignValue_Currency",
                table: "AccountEvent");

            migrationBuilder.DropColumn(
                name: "Transfer_Source_NativeValue_Amount",
                table: "AccountEvent");

            migrationBuilder.DropColumn(
                name: "Transfer_Source_NativeValue_Currency",
                table: "AccountEvent");

            migrationBuilder.DropColumn(
                name: "Transfer_Target_ForeignValue_Amount",
                table: "AccountEvent");

            migrationBuilder.DropColumn(
                name: "Transfer_Target_ForeignValue_Currency",
                table: "AccountEvent");

            migrationBuilder.DropColumn(
                name: "Transfer_Target_NativeValue_Amount",
                table: "AccountEvent");

            migrationBuilder.DropColumn(
                name: "Transfer_Target_NativeValue_Currency",
                table: "AccountEvent");

            migrationBuilder.AddColumn<long>(
                name: "Balance",
                table: "Accounts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "BalanceChange_Value",
                table: "AccountEvent",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Transfer_Source_Value",
                table: "AccountEvent",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Transfer_Target_Value",
                table: "AccountEvent",
                type: "bigint",
                nullable: true);
        }
    }
}
