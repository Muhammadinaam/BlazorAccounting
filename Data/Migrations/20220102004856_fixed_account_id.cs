using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorAccounting.Data.Migrations
{
    public partial class fixed_account_id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionLines_Accounts_AccountId1",
                table: "TransactionLines");

            migrationBuilder.DropIndex(
                name: "IX_TransactionLines_AccountId1",
                table: "TransactionLines");

            migrationBuilder.DropColumn(
                name: "AccountId1",
                table: "TransactionLines");

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "TransactionLines",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionLines_AccountId",
                table: "TransactionLines",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionLines_Accounts_AccountId",
                table: "TransactionLines",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionLines_Accounts_AccountId",
                table: "TransactionLines");

            migrationBuilder.DropIndex(
                name: "IX_TransactionLines_AccountId",
                table: "TransactionLines");

            migrationBuilder.AlterColumn<string>(
                name: "AccountId",
                table: "TransactionLines",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AccountId1",
                table: "TransactionLines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionLines_AccountId1",
                table: "TransactionLines",
                column: "AccountId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionLines_Accounts_AccountId1",
                table: "TransactionLines",
                column: "AccountId1",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
