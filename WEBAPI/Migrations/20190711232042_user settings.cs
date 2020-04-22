using Microsoft.EntityFrameworkCore.Migrations;

namespace WEBAPI.Migrations
{
    public partial class usersettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanAdministratorAddTime",
                table: "UserSettings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanAdministratorSettings",
                table: "UserSettings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanAdministratorWorkers",
                table: "UserSettings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanAdministratorWriteContactEmail",
                table: "UserSettings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanAdministratorWritePhone",
                table: "UserSettings",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanAdministratorAddTime",
                table: "UserSettings");

            migrationBuilder.DropColumn(
                name: "CanAdministratorSettings",
                table: "UserSettings");

            migrationBuilder.DropColumn(
                name: "CanAdministratorWorkers",
                table: "UserSettings");

            migrationBuilder.DropColumn(
                name: "CanAdministratorWriteContactEmail",
                table: "UserSettings");

            migrationBuilder.DropColumn(
                name: "CanAdministratorWritePhone",
                table: "UserSettings");
        }
    }
}
