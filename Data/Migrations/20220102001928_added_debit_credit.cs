using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorAccounting.Data.Migrations
{
    public partial class added_debit_credit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "TransactionLines",
                newName: "Debit");

            migrationBuilder.AddColumn<decimal>(
                name: "Credit",
                table: "TransactionLines",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Credit",
                table: "TransactionLines");

            migrationBuilder.RenameColumn(
                name: "Debit",
                table: "TransactionLines",
                newName: "Amount");
        }
    }
}
