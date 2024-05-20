using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalAidPlatform.API.Migrations
{
    /// <inheritdoc />
    public partial class FeedPostUpdate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GeoLang",
                table: "FeedPosts",
                newName: "GeoLong");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "FeedPosts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "FeedPosts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FeedPosts_CategoryId",
                table: "FeedPosts",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedPosts_Categories_CategoryId",
                table: "FeedPosts",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedPosts_Categories_CategoryId",
                table: "FeedPosts");

            migrationBuilder.DropIndex(
                name: "IX_FeedPosts_CategoryId",
                table: "FeedPosts");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "FeedPosts");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "FeedPosts");

            migrationBuilder.RenameColumn(
                name: "GeoLong",
                table: "FeedPosts",
                newName: "GeoLang");
        }
    }
}
