using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bank.Core.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCurrencyConversionRatesStore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CurrencyExhangeRateRecord",
                table: "CurrencyExhangeRateRecord");

            migrationBuilder.RenameTable(
                name: "CurrencyExhangeRateRecord",
                newName: "CurrencyExhangeRates");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CurrencyExhangeRates",
                table: "CurrencyExhangeRates",
                columns: new[] { "Source", "Target" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CurrencyExhangeRates",
                table: "CurrencyExhangeRates");

            migrationBuilder.RenameTable(
                name: "CurrencyExhangeRates",
                newName: "CurrencyExhangeRateRecord");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CurrencyExhangeRateRecord",
                table: "CurrencyExhangeRateRecord",
                columns: new[] { "Source", "Target" });
        }
    }
}
