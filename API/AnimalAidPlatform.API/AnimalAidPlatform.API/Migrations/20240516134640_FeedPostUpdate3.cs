using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalAidPlatform.API.Migrations
{
    /// <inheritdoc />
    public partial class FeedPostUpdate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedPosts_AspNetUsers_CreatorId",
                table: "FeedPosts");

            migrationBuilder.AlterColumn<string>(
                name: "CreatorId",
                table: "FeedPosts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FeedPosts_AspNetUsers_CreatorId",
                table: "FeedPosts",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedPosts_AspNetUsers_CreatorId",
                table: "FeedPosts");

            migrationBuilder.AlterColumn<string>(
                name: "CreatorId",
                table: "FeedPosts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedPosts_AspNetUsers_CreatorId",
                table: "FeedPosts",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
