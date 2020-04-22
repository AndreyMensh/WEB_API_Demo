using Microsoft.EntityFrameworkCore.Migrations;

namespace WEBAPI.Migrations
{
    public partial class managgetrtypenew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ManagerType",
                table: "UserSettings");

            migrationBuilder.AddColumn<bool>(
                name: "ManagerTypeOne",
                table: "UserSettings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ManagerTypeTwo",
                table: "UserSettings",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ManagerTypeOne",
                table: "UserSettings");

            migrationBuilder.DropColumn(
                name: "ManagerTypeTwo",
                table: "UserSettings");

            migrationBuilder.AddColumn<int>(
                name: "ManagerType",
                table: "UserSettings",
                nullable: false,
                defaultValue: 0);
        }
    }
}
