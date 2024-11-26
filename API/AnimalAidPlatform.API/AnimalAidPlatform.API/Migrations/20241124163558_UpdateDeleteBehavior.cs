using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalAidPlatform.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedPostLikes_FeedPosts_FeedPostId",
                table: "FeedPostLikes");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedPostLikes_FeedPosts_FeedPostId",
                table: "FeedPostLikes",
                column: "FeedPostId",
                principalTable: "FeedPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedPostLikes_FeedPosts_FeedPostId",
                table: "FeedPostLikes");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedPostLikes_FeedPosts_FeedPostId",
                table: "FeedPostLikes",
                column: "FeedPostId",
                principalTable: "FeedPosts",
                principalColumn: "Id");
        }
    }
}
