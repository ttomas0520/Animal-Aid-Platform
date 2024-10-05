using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace AnimalAidPlatform.API.Migrations
{
    /// <inheritdoc />
    public partial class AddLikes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Point>(
                name: "Location",
                table: "NotificationSettings",
                type: "geography",
                nullable: true,
                oldClrType: typeof(Point),
                oldType: "geography");

            migrationBuilder.AlterColumn<Point>(
                name: "Location",
                table: "FeedPosts",
                type: "geography",
                nullable: true,
                oldClrType: typeof(Point),
                oldType: "geography");

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "FeedPosts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FeedPostLikes",
                columns: table => new
                {
                    FeedPostId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LikedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedPostLikes", x => new { x.FeedPostId, x.UserId });
                    table.ForeignKey(
                        name: "FK_FeedPostLikes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FeedPostLikes_FeedPosts_FeedPostId",
                        column: x => x.FeedPostId,
                        principalTable: "FeedPosts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeedPostLikes_UserId",
                table: "FeedPostLikes",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeedPostLikes");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "FeedPosts");

            migrationBuilder.AlterColumn<Point>(
                name: "Location",
                table: "NotificationSettings",
                type: "geography",
                nullable: false,
                oldClrType: typeof(Point),
                oldType: "geography",
                oldNullable: true);

            migrationBuilder.AlterColumn<Point>(
                name: "Location",
                table: "FeedPosts",
                type: "geography",
                nullable: false,
                oldClrType: typeof(Point),
                oldType: "geography",
                oldNullable: true);
        }
    }
}
