using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pawbook.Migrations
{
    public partial class UpdatePaw2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paws_Posts_PostId",
                table: "Paws");

            migrationBuilder.DropForeignKey(
                name: "FK_Paws_Users_UserId",
                table: "Paws");

            migrationBuilder.DropIndex(
                name: "IX_Paws_UserId",
                table: "Paws");

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "Paws",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Paws_Posts_PostId",
                table: "Paws",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paws_Posts_PostId",
                table: "Paws");

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "Paws",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Paws_UserId",
                table: "Paws",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Paws_Posts_PostId",
                table: "Paws",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Paws_Users_UserId",
                table: "Paws",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
