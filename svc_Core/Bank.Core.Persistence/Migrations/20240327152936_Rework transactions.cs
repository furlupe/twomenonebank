using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bank.Core.Persistence.Migrations;

/// <inheritdoc />
public partial class Reworktransactions : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<DateTime>(
            name: "Transfer_ResolvedAt",
            table: "AccountEvent",
            type: "timestamp with time zone",
            nullable: true
        );

        migrationBuilder.AddColumn<int>(
            name: "Transfer_State",
            table: "AccountEvent",
            type: "integer",
            nullable: true
        );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(name: "Transfer_ResolvedAt", table: "AccountEvent");

        migrationBuilder.DropColumn(name: "Transfer_State", table: "AccountEvent");
    }
}
