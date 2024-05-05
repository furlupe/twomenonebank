using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bank.Auth.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddIdempotence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActionDescriptors",
                columns: table => new
                {
                    IdempotencyKey = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionDescriptors", x => x.IdempotencyKey);
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "ActionDescriptors");
        }
    }
}
