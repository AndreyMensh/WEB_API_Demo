using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WEBAPI.Migrations
{
    public partial class fromtocansee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DaysCanSee",
                table: "UserSettings",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FromTimeCanSee",
                table: "UserSettings",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ToTimeCanSee",
                table: "UserSettings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaysCanSee",
                table: "UserSettings");

            migrationBuilder.DropColumn(
                name: "FromTimeCanSee",
                table: "UserSettings");

            migrationBuilder.DropColumn(
                name: "ToTimeCanSee",
                table: "UserSettings");
        }
    }
}
