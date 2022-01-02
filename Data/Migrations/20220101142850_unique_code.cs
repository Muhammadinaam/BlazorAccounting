using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorAccounting.Data.Migrations
{
    public partial class unique_code : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Accounts_UserId",
                table: "Accounts");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserId_Code",
                table: "Accounts",
                columns: new[] { "UserId", "Code" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Accounts_UserId_Code",
                table: "Accounts");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserId",
                table: "Accounts",
                column: "UserId");
        }
    }
}
