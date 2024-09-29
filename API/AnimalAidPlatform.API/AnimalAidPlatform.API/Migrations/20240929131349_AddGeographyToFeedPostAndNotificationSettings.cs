using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace AnimalAidPlatform.API.Migrations
{
    /// <inheritdoc />
    public partial class AddGeographyToFeedPostAndNotificationSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Point>(
                name: "Location",
                table: "NotificationSettings",
                type: "geography",
                nullable: false);

            migrationBuilder.AddColumn<Point>(
                name: "Location",
                table: "FeedPosts",
                type: "geography",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "NotificationSettings");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "FeedPosts");
        }
    }
}
