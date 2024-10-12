using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace AnimalAidPlatform.API.Migrations
{
    /// <inheritdoc />
    public partial class AnimalShelterModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AnimalShelters_AdminWorkingPlaceId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AnimalShelters_AnimalShelterId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AdminWorkingPlaceId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AnimalShelterId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AdminWorkingPlaceId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AnimalShelterId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "AnimalShelters");

            migrationBuilder.DropColumn(
                name: "EstablishedDate",
                table: "AnimalShelters");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "AnimalShelters",
                newName: "Weekend");

            migrationBuilder.RenameColumn(
                name: "ContactPhone",
                table: "AnimalShelters",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "ContactEmail",
                table: "AnimalShelters",
                newName: "PhoneNumber");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AnimalShelters",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "Adoption",
                table: "AnimalShelters",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ContactName",
                table: "AnimalShelters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContactPosition",
                table: "AnimalShelters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Donations",
                table: "AnimalShelters",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "AnimalShelters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "GeoLat",
                table: "AnimalShelters",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "GeoLong",
                table: "AnimalShelters",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<Point>(
                name: "Location",
                table: "AnimalShelters",
                type: "geography",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MedicalCare",
                table: "AnimalShelters",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Visiting",
                table: "AnimalShelters",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Volunteering",
                table: "AnimalShelters",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Weekdays",
                table: "AnimalShelters",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adoption",
                table: "AnimalShelters");

            migrationBuilder.DropColumn(
                name: "ContactName",
                table: "AnimalShelters");

            migrationBuilder.DropColumn(
                name: "ContactPosition",
                table: "AnimalShelters");

            migrationBuilder.DropColumn(
                name: "Donations",
                table: "AnimalShelters");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "AnimalShelters");

            migrationBuilder.DropColumn(
                name: "GeoLat",
                table: "AnimalShelters");

            migrationBuilder.DropColumn(
                name: "GeoLong",
                table: "AnimalShelters");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "AnimalShelters");

            migrationBuilder.DropColumn(
                name: "MedicalCare",
                table: "AnimalShelters");

            migrationBuilder.DropColumn(
                name: "Visiting",
                table: "AnimalShelters");

            migrationBuilder.DropColumn(
                name: "Volunteering",
                table: "AnimalShelters");

            migrationBuilder.DropColumn(
                name: "Weekdays",
                table: "AnimalShelters");

            migrationBuilder.RenameColumn(
                name: "Weekend",
                table: "AnimalShelters",
                newName: "PersonId");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "AnimalShelters",
                newName: "ContactPhone");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "AnimalShelters",
                newName: "ContactEmail");

            migrationBuilder.AddColumn<int>(
                name: "AdminWorkingPlaceId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AnimalShelterId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AnimalShelters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "AnimalShelters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EstablishedDate",
                table: "AnimalShelters",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AdminWorkingPlaceId",
                table: "AspNetUsers",
                column: "AdminWorkingPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AnimalShelterId",
                table: "AspNetUsers",
                column: "AnimalShelterId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AnimalShelters_AdminWorkingPlaceId",
                table: "AspNetUsers",
                column: "AdminWorkingPlaceId",
                principalTable: "AnimalShelters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AnimalShelters_AnimalShelterId",
                table: "AspNetUsers",
                column: "AnimalShelterId",
                principalTable: "AnimalShelters",
                principalColumn: "Id");
        }
    }
}
