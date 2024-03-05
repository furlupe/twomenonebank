using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bank.Credit.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddNewCreditEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Period", table: "Tariffs");

            migrationBuilder.RenameColumn(
                name: "HappenedAt",
                table: "CreditEvents",
                newName: "CreatedAt"
            );

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
            );

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Tariffs",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
            );

            migrationBuilder.AddColumn<int>(
                name: "BaseAmount",
                table: "Credits",
                type: "integer",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Credits",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
            );

            migrationBuilder.AddColumn<DateTime>(
                name: "LastPaymentDate",
                table: "Credits",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
            );

            migrationBuilder.AddColumn<int>(
                name: "MissedPaymentPeriods",
                table: "Credits",
                type: "integer",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.AddColumn<DateTime>(
                name: "NextPaymentDate",
                table: "Credits",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
            );

            migrationBuilder.AddColumn<int>(
                name: "Penalty",
                table: "Credits",
                type: "integer",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.AddColumn<int>(
                name: "PeriodicPayment",
                table: "Credits",
                type: "integer",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.AddColumn<DateTime>(
                name: "Until",
                table: "Credits",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "CreatedAt", table: "Users");

            migrationBuilder.DropColumn(name: "CreatedAt", table: "Tariffs");

            migrationBuilder.DropColumn(name: "BaseAmount", table: "Credits");

            migrationBuilder.DropColumn(name: "CreatedAt", table: "Credits");

            migrationBuilder.DropColumn(name: "LastPaymentDate", table: "Credits");

            migrationBuilder.DropColumn(name: "MissedPaymentPeriods", table: "Credits");

            migrationBuilder.DropColumn(name: "NextPaymentDate", table: "Credits");

            migrationBuilder.DropColumn(name: "Penalty", table: "Credits");

            migrationBuilder.DropColumn(name: "PeriodicPayment", table: "Credits");

            migrationBuilder.DropColumn(name: "Until", table: "Credits");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "CreditEvents",
                newName: "HappenedAt"
            );

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Period",
                table: "Tariffs",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0)
            );
        }
    }
}
