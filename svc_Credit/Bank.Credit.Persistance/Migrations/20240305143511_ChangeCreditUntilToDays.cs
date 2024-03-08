using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bank.Credit.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCreditUntilToDays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Until", table: "Credits");

            migrationBuilder.AddColumn<int>(
                name: "Days",
                table: "Credits",
                type: "integer",
                nullable: false,
                defaultValue: 0
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Days", table: "Credits");

            migrationBuilder.AddColumn<DateTime>(
                name: "Until",
                table: "Credits",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
            );
        }
    }
}
