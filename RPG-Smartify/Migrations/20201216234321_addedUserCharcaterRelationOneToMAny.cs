using Microsoft.EntityFrameworkCore.Migrations;

namespace RPG_Smartify.Migrations
{
    public partial class addedUserCharcaterRelationOneToMAny : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "userid",
                table: "characters",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_characters_userid",
                table: "characters",
                column: "userid");

            migrationBuilder.AddForeignKey(
                name: "FK_characters_Users_userid",
                table: "characters",
                column: "userid",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_characters_Users_userid",
                table: "characters");

            migrationBuilder.DropIndex(
                name: "IX_characters_userid",
                table: "characters");

            migrationBuilder.DropColumn(
                name: "userid",
                table: "characters");
        }
    }
}
