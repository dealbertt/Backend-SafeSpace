using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_SafeSpace.Migrations
{
    /// <inheritdoc />
    public partial class AddChatRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chatrooms",
                columns: table => new
                {
                    ChatroomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chatrooms", x => x.ChatroomId);
                });

            migrationBuilder.CreateTable(
                name: "UserChatrooms",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ChatroomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChatrooms", x => new { x.UserId, x.ChatroomId });
                    table.ForeignKey(
                        name: "FK_UserChatrooms_Chatrooms_ChatroomId",
                        column: x => x.ChatroomId,
                        principalTable: "Chatrooms",
                        principalColumn: "ChatroomId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserChatrooms_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserChatrooms_ChatroomId",
                table: "UserChatrooms",
                column: "ChatroomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserChatrooms");

            migrationBuilder.DropTable(
                name: "Chatrooms");
        }
    }
}
