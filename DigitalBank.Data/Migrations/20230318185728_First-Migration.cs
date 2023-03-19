using System;
using DigitalBank.Shared.Constants.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalBank.Data.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "digital_bank");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:e_transaction_type", "WITHDRAW,DEPOSIT");

            migrationBuilder.CreateTable(
                name: "reg_accounts",
                schema: "digital_bank",
                columns: table => new
                {
                    id_account = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    name = table.Column<string>(type: "varchar(50)", nullable: false),
                    document_number = table.Column<string>(type: "varchar(13)", nullable: false),
                    balance = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    account_number = table.Column<int>(type: "int", nullable: false),
                    deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "(now() at time zone 'utc')"),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reg_accounts", x => x.id_account);
                });

            migrationBuilder.CreateTable(
                name: "reg_transactions",
                schema: "digital_bank",
                columns: table => new
                {
                    id_transaction = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    amount = table.Column<decimal>(type: "decimal", nullable: false),
                    Type = table.Column<ETransactionType>(type: "e_transaction_type", nullable: false),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "(now() at time zone 'utc')"),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reg_transactions", x => x.id_transaction);
                    table.ForeignKey(
                        name: "FK_reg_transactions_reg_accounts_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "digital_bank",
                        principalTable: "reg_accounts",
                        principalColumn: "id_account",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_reg_accounts_document_number",
                schema: "digital_bank",
                table: "reg_accounts",
                column: "document_number",
                unique: true,
                filter: "deleted = false");

            migrationBuilder.CreateIndex(
                name: "IX_reg_transactions_AccountId",
                schema: "digital_bank",
                table: "reg_transactions",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reg_transactions",
                schema: "digital_bank");

            migrationBuilder.DropTable(
                name: "reg_accounts",
                schema: "digital_bank");
        }
    }
}
