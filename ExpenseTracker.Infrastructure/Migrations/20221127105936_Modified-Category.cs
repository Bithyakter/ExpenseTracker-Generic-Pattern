using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseTracker.Infrastructure.Migrations
{
    public partial class ModifiedCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseCategories_UserAccounts_UserAccountID",
                table: "ExpenseCategories");

            migrationBuilder.DropIndex(
                name: "IX_ExpenseCategories_UserAccountID",
                table: "ExpenseCategories");

            migrationBuilder.DropColumn(
                name: "UserAccountID",
                table: "ExpenseCategories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserAccountID",
                table: "ExpenseCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseCategories_UserAccountID",
                table: "ExpenseCategories",
                column: "UserAccountID");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseCategories_UserAccounts_UserAccountID",
                table: "ExpenseCategories",
                column: "UserAccountID",
                principalTable: "UserAccounts",
                principalColumn: "UserAccountID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
