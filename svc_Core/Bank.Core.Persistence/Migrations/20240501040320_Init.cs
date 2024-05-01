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
                name: "CurrencyExhangeRates",
                columns: table => new
                {
                    Source = table.Column<int>(type: "integer", nullable: false),
                    Target = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyExhangeRates", x => new { x.Source, x.Target });
                });

            migrationBuilder.CreateTable(
                name: "AccountEvent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EventType = table.Column<int>(type: "integer", nullable: false),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    ResolvedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IdempotenceKey = table.Column<Guid>(type: "uuid", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountEvent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    Balance_Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Balance_Currency = table.Column<int>(type: "integer", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IsMaster = table.Column<bool>(type: "boolean", nullable: false),
                    ClosedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionEvent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    EventType = table.Column<int>(type: "integer", nullable: false),
                    BalanceChange_NativeValue_Amount = table.Column<decimal>(type: "numeric", nullable: true),
                    BalanceChange_NativeValue_Currency = table.Column<int>(type: "integer", nullable: true),
                    BalanceChange_ForeignValue_Amount = table.Column<decimal>(type: "numeric", nullable: true),
                    BalanceChange_ForeignValue_Currency = table.Column<int>(type: "integer", nullable: true),
                    BalanceChange_AccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    BalanceChange_EventType = table.Column<int>(type: "integer", nullable: true),
                    Transfer_Source_NativeValue_Amount = table.Column<decimal>(type: "numeric", nullable: true),
                    Transfer_Source_NativeValue_Currency = table.Column<int>(type: "integer", nullable: true),
                    Transfer_Source_ForeignValue_Amount = table.Column<decimal>(type: "numeric", nullable: true),
                    Transfer_Source_ForeignValue_Currency = table.Column<int>(type: "integer", nullable: true),
                    Transfer_Source_AccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    Transfer_Source_EventType = table.Column<int>(type: "integer", nullable: true),
                    Transfer_Target_NativeValue_Amount = table.Column<decimal>(type: "numeric", nullable: true),
                    Transfer_Target_NativeValue_Currency = table.Column<int>(type: "integer", nullable: true),
                    Transfer_Target_ForeignValue_Amount = table.Column<decimal>(type: "numeric", nullable: true),
                    Transfer_Target_ForeignValue_Currency = table.Column<int>(type: "integer", nullable: true),
                    Transfer_Target_AccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    Transfer_Target_EventType = table.Column<int>(type: "integer", nullable: true),
                    Transfer_Type = table.Column<int>(type: "integer", nullable: true),
                    Transfer_CreditTransfer_Type = table.Column<int>(type: "integer", nullable: true),
                    Transfer_CreditTransfer_CreditId = table.Column<Guid>(type: "uuid", nullable: true),
                    ResolvedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IdempotenceKey = table.Column<Guid>(type: "uuid", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionEvent_Accounts_BalanceChange_AccountId",
                        column: x => x.BalanceChange_AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionEvent_Accounts_Transfer_Source_AccountId",
                        column: x => x.Transfer_Source_AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionEvent_Accounts_Transfer_Target_AccountId",
                        column: x => x.Transfer_Target_AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    DefaultTransferAccountId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Accounts_DefaultTransferAccountId",
                        column: x => x.DefaultTransferAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AccountTransactionEvent",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    TransactionsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTransactionEvent", x => new { x.AccountId, x.TransactionsId });
                    table.ForeignKey(
                        name: "FK_AccountTransactionEvent_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountTransactionEvent_TransactionEvent_TransactionsId",
                        column: x => x.TransactionsId,
                        principalTable: "TransactionEvent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountEvent_AccountId",
                table: "AccountEvent",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Name_OwnerId",
                table: "Accounts",
                columns: new[] { "Name", "OwnerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_OwnerId",
                table: "Accounts",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTransactionEvent_TransactionsId",
                table: "AccountTransactionEvent",
                column: "TransactionsId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionEvent_BalanceChange_AccountId",
                table: "TransactionEvent",
                column: "BalanceChange_AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionEvent_Transfer_Source_AccountId",
                table: "TransactionEvent",
                column: "Transfer_Source_AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionEvent_Transfer_Target_AccountId",
                table: "TransactionEvent",
                column: "Transfer_Target_AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DefaultTransferAccountId",
                table: "Users",
                column: "DefaultTransferAccountId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountEvent_Accounts_AccountId",
                table: "AccountEvent",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Users_OwnerId",
                table: "Accounts",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Accounts_DefaultTransferAccountId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "AccountEvent");

            migrationBuilder.DropTable(
                name: "AccountTransactionEvent");

            migrationBuilder.DropTable(
                name: "CurrencyExhangeRates");

            migrationBuilder.DropTable(
                name: "TransactionEvent");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
