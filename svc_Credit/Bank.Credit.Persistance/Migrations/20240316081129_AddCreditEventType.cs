using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bank.Credit.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddCreditEventType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "CreditEvents",
                type: "integer",
                nullable: false,
                defaultValue: 0
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Type", table: "CreditEvents");
        }
    }
}
