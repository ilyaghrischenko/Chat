using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBase.Migrations
{
    /// <inheritdoc />
    public partial class change : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_UserId",
                table: "Messages");

            migrationBuilder.CreateIndex(
                name: "IX_ChatDetails_User_1Id",
                table: "ChatDetails",
                column: "User_1Id");

            migrationBuilder.CreateIndex(
                name: "IX_ChatDetails_User_2Id",
                table: "ChatDetails",
                column: "User_2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatDetails_Users_User_1Id",
                table: "ChatDetails",
                column: "User_1Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatDetails_Users_User_2Id",
                table: "ChatDetails",
                column: "User_2Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_UserId",
                table: "Messages",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatDetails_Users_User_1Id",
                table: "ChatDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatDetails_Users_User_2Id",
                table: "ChatDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_UserId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_ChatDetails_User_1Id",
                table: "ChatDetails");

            migrationBuilder.DropIndex(
                name: "IX_ChatDetails_User_2Id",
                table: "ChatDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_UserId",
                table: "Messages",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
