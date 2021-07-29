using Microsoft.EntityFrameworkCore.Migrations;

namespace Hobbies.Migrations
{
    public partial class FourthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hobbies_Users_UserId",
                table: "Hobbies");

            migrationBuilder.DropIndex(
                name: "IX_Hobbies_UserId",
                table: "Hobbies");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Hobbies");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Hobbies",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hobbies_UserId",
                table: "Hobbies",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hobbies_Users_UserId",
                table: "Hobbies",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
