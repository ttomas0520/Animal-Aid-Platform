using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalAidPlatform.API.Migrations
{
    /// <inheritdoc />
    public partial class Category_AssetIconHref : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssestIconHref",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssestIconHref",
                table: "Categories");
        }
    }
}
