using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bank.Core.Persistence.Migrations;

/// <inheritdoc />
public partial class AddCurrencyExchangeRateStore : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "CurrencyExhangeRateRecord",
            columns: table => new
            {
                Source = table.Column<int>(type: "integer", nullable: false),
                Target = table.Column<int>(type: "integer", nullable: false),
                Value = table.Column<decimal>(type: "numeric", nullable: false),
                ExpiresAt = table.Column<DateTime>(
                    type: "timestamp with time zone",
                    nullable: false
                )
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CurrencyExhangeRateRecord", x => new { x.Source, x.Target });
            }
        );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "CurrencyExhangeRateRecord");
    }
}
