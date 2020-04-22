using Microsoft.EntityFrameworkCore.Migrations;

namespace WEBAPI.Migrations
{
    public partial class tablesettingsuppdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Act",
                table: "TableSettings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Email",
                table: "TableSettings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ManagerComment",
                table: "TableSettings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Phone",
                table: "TableSettings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Photo",
                table: "TableSettings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Sign",
                table: "TableSettings",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Act",
                table: "TableSettings");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "TableSettings");

            migrationBuilder.DropColumn(
                name: "ManagerComment",
                table: "TableSettings");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "TableSettings");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "TableSettings");

            migrationBuilder.DropColumn(
                name: "Sign",
                table: "TableSettings");
        }
    }
}
