using Microsoft.EntityFrameworkCore.Migrations;

namespace WEBAPI.Migrations
{
    public partial class RenameuserSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomDepartment",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Signature",
                table: "UserSettings",
                newName: "CanAdministratorSignature");

            migrationBuilder.RenameColumn(
                name: "Photo",
                table: "UserSettings",
                newName: "CanAdministratorSeeAllWorkers");

            migrationBuilder.RenameColumn(
                name: "OnlyMonitoring",
                table: "UserSettings",
                newName: "CanAdministratorPhoto");

            migrationBuilder.RenameColumn(
                name: "CanSeeAllWorkers",
                table: "UserSettings",
                newName: "CanAdministratorOnlyMonitoring");

            migrationBuilder.RenameColumn(
                name: "CanComment",
                table: "UserSettings",
                newName: "CanAdministratorComment");

            migrationBuilder.RenameColumn(
                name: "Calendar",
                table: "UserSettings",
                newName: "CanAdministratorCalendar");

            migrationBuilder.RenameColumn(
                name: "AllFunctionality",
                table: "UserSettings",
                newName: "CanAdministratorAllFunctionality");

            migrationBuilder.RenameColumn(
                name: "Act",
                table: "UserSettings",
                newName: "CanAdministratorAct");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CanAdministratorSignature",
                table: "UserSettings",
                newName: "Signature");

            migrationBuilder.RenameColumn(
                name: "CanAdministratorSeeAllWorkers",
                table: "UserSettings",
                newName: "Photo");

            migrationBuilder.RenameColumn(
                name: "CanAdministratorPhoto",
                table: "UserSettings",
                newName: "OnlyMonitoring");

            migrationBuilder.RenameColumn(
                name: "CanAdministratorOnlyMonitoring",
                table: "UserSettings",
                newName: "CanSeeAllWorkers");

            migrationBuilder.RenameColumn(
                name: "CanAdministratorComment",
                table: "UserSettings",
                newName: "CanComment");

            migrationBuilder.RenameColumn(
                name: "CanAdministratorCalendar",
                table: "UserSettings",
                newName: "Calendar");

            migrationBuilder.RenameColumn(
                name: "CanAdministratorAllFunctionality",
                table: "UserSettings",
                newName: "AllFunctionality");

            migrationBuilder.RenameColumn(
                name: "CanAdministratorAct",
                table: "UserSettings",
                newName: "Act");

            migrationBuilder.AddColumn<string>(
                name: "CustomDepartment",
                table: "Users",
                nullable: true);
        }
    }
}
