using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseTracker.Infrastructure.Migrations
{
   public partial class newTableUserAccounts : Migration
   {
      protected override void Up(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.CreateTable(
             name: "UserAccounts",
             columns: table => new
             {
                UserAccountID = table.Column<int>(type: "int", nullable: false)
                     .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false),
                DOB = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                Gender = table.Column<byte>(type: "tinyint", nullable: false),
                Email = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CreatedBy = table.Column<int>(type: "int", nullable: true),
                DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                ModifiedBy = table.Column<int>(type: "int", nullable: true),
                DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                IsRowDeleted = table.Column<bool>(type: "bit", nullable: true),
                IsSynced = table.Column<bool>(type: "bit", nullable: true)
             },
             constraints: table =>
             {
                table.PrimaryKey("PK_UserAccounts", x => x.UserAccountID);
             });
      }

      protected override void Down(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.DropTable(
             name: "UserAccounts");
      }
   }
}
