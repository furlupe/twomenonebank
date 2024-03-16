using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bank.Core.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Balance = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountEvent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    EventType = table.Column<int>(type: "integer", nullable: false),
                    BalanceChange_Value = table.Column<long>(type: "bigint", nullable: true),
                    BalanceChange_AccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    BalanceChange_EventType = table.Column<int>(type: "integer", nullable: true),
                    BalanceChange_CreditPayment_CreditId = table.Column<Guid>(type: "uuid", nullable: true),
                    Transfer_Id = table.Column<Guid>(type: "uuid", nullable: true),
                    Transfer_Source_Value = table.Column<long>(type: "bigint", nullable: true),
                    Transfer_Source_AccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    Transfer_Source_EventType = table.Column<int>(type: "integer", nullable: true),
                    Transfer_Source_CreditPayment_CreditId = table.Column<Guid>(type: "uuid", nullable: true),
                    Transfer_Target_Value = table.Column<long>(type: "bigint", nullable: true),
                    Transfer_Target_AccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    Transfer_Target_EventType = table.Column<int>(type: "integer", nullable: true),
                    Transfer_Target_CreditPayment_CreditId = table.Column<Guid>(type: "uuid", nullable: true),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    ResolvedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountEvent_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccountEvent_Accounts_BalanceChange_AccountId",
                        column: x => x.BalanceChange_AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountEvent_Accounts_Transfer_Source_AccountId",
                        column: x => x.Transfer_Source_AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountEvent_Accounts_Transfer_Target_AccountId",
                        column: x => x.Transfer_Target_AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountEvent_AccountId",
                table: "AccountEvent",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountEvent_BalanceChange_AccountId",
                table: "AccountEvent",
                column: "BalanceChange_AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountEvent_Transfer_Source_AccountId",
                table: "AccountEvent",
                column: "Transfer_Source_AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountEvent_Transfer_Target_AccountId",
                table: "AccountEvent",
                column: "Transfer_Target_AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Name_UserId",
                table: "Accounts",
                columns: new[] { "Name", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserId",
                table: "Accounts",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountEvent");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
