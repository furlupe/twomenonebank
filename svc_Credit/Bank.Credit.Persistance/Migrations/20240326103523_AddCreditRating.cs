using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bank.Credit.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddCreditRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreditRating",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "CreditRating", table: "Users");
        }
    }
}
