using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorAccounting.Data.Migrations
{
    public partial class removed_user_foreign_key_from_transaction_line : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionLines_AspNetUsers_UserId",
                table: "TransactionLines");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionLines_Transactions_TransactionId",
                table: "TransactionLines");

            migrationBuilder.DropIndex(
                name: "IX_TransactionLines_UserId",
                table: "TransactionLines");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TransactionLines");

            migrationBuilder.AlterColumn<int>(
                name: "TransactionId",
                table: "TransactionLines",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionLines_Transactions_TransactionId",
                table: "TransactionLines",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionLines_Transactions_TransactionId",
                table: "TransactionLines");

            migrationBuilder.AlterColumn<int>(
                name: "TransactionId",
                table: "TransactionLines",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TransactionLines",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionLines_UserId",
                table: "TransactionLines",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionLines_AspNetUsers_UserId",
                table: "TransactionLines",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionLines_Transactions_TransactionId",
                table: "TransactionLines",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id");
        }
    }
}
