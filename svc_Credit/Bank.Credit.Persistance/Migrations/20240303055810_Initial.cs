using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bank.Credit.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tariffs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Rate = table.Column<int>(type: "integer", nullable: false),
                    Period = table.Column<TimeSpan>(type: "interval", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tariffs", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "Credits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsClosed = table.Column<bool>(type: "boolean", nullable: false),
                    TariffId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    RateLastApplied = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Credits_Tariffs_TariffId",
                        column: x => x.TariffId,
                        principalTable: "Tariffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_Credits_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "CreditEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreditId = table.Column<Guid>(type: "uuid", nullable: true),
                    Discriminator = table.Column<string>(
                        type: "character varying(34)",
                        maxLength: 34,
                        nullable: false
                    ),
                    Amount = table.Column<int>(type: "integer", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    AggregateId = table.Column<Guid>(type: "uuid", nullable: false),
                    HappenedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    )
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreditEvents_Credits_CreditId",
                        column: x => x.CreditId,
                        principalTable: "Credits",
                        principalColumn: "Id"
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_CreditEvents_CreditId",
                table: "CreditEvents",
                column: "CreditId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Credits_TariffId",
                table: "Credits",
                column: "TariffId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Credits_UserId",
                table: "Credits",
                column: "UserId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Tariffs_Name",
                table: "Tariffs",
                column: "Name",
                unique: true
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "CreditEvents");

            migrationBuilder.DropTable(name: "Credits");

            migrationBuilder.DropTable(name: "Tariffs");

            migrationBuilder.DropTable(name: "Users");
        }
    }
}
