using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pawbook.Migrations
{
    public partial class UpdatePaw : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Paws_UserId",
                table: "Paws",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Paws_Users_UserId",
                table: "Paws",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paws_Users_UserId",
                table: "Paws");

            migrationBuilder.DropIndex(
                name: "IX_Paws_UserId",
                table: "Paws");
        }
    }
}
