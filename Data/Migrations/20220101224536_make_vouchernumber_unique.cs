using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorAccounting.Data.Migrations
{
    public partial class make_vouchernumber_unique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_AccountId1",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_AccountId1",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_UserId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "AccountId1",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Transactions");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "Transactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "VoucherNumber",
                table: "Transactions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AccountId",
                table: "TransactionLines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "AccountId1",
                table: "TransactionLines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "TransactionLines",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "TransactionLines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TransactionId",
                table: "TransactionLines",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserId_VoucherNumber",
                table: "Transactions",
                columns: new[] { "UserId", "VoucherNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionLines_AccountId1",
                table: "TransactionLines",
                column: "AccountId1");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionLines_TransactionId",
                table: "TransactionLines",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionLines_Accounts_AccountId1",
                table: "TransactionLines",
                column: "AccountId1",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionLines_Transactions_TransactionId",
                table: "TransactionLines",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionLines_Accounts_AccountId1",
                table: "TransactionLines");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionLines_Transactions_TransactionId",
                table: "TransactionLines");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_UserId_VoucherNumber",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_TransactionLines_AccountId1",
                table: "TransactionLines");

            migrationBuilder.DropIndex(
                name: "IX_TransactionLines_TransactionId",
                table: "TransactionLines");

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "VoucherNumber",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "TransactionLines");

            migrationBuilder.DropColumn(
                name: "AccountId1",
                table: "TransactionLines");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "TransactionLines");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "TransactionLines");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "TransactionLines");

            migrationBuilder.AddColumn<string>(
                name: "AccountId",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "AccountId1",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "Transactions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountId1",
                table: "Transactions",
                column: "AccountId1");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserId",
                table: "Transactions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_AccountId1",
                table: "Transactions",
                column: "AccountId1",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
