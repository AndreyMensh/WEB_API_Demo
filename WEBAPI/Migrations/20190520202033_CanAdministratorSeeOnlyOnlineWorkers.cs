using Microsoft.EntityFrameworkCore.Migrations;

namespace WEBAPI.Migrations
{
    public partial class CanAdministratorSeeOnlyOnlineWorkers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CanAdministratorSeeAllWorkers",
                table: "UserSettings",
                newName: "CanAdministratorSeeOnlyOnlineWorkers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CanAdministratorSeeOnlyOnlineWorkers",
                table: "UserSettings",
                newName: "CanAdministratorSeeAllWorkers");
        }
    }
}
