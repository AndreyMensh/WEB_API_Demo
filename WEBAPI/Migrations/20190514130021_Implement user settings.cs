using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WEBAPI.Migrations
{
    public partial class Implementusersettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomDepartment",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExportCode",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserSettings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AutomaticSchedule = table.Column<bool>(nullable: false),
                    ExcludeWeekends = table.Column<bool>(nullable: false),
                    WorkingShift = table.Column<bool>(nullable: false),
                    ExcludePublicHolidays = table.Column<bool>(nullable: false),
                    NotificationIfAbsentByEmail = table.Column<bool>(nullable: false),
                    CanSeeWorkingHours = table.Column<bool>(nullable: false),
                    Act = table.Column<bool>(nullable: false),
                    Photo = table.Column<bool>(nullable: false),
                    Signature = table.Column<bool>(nullable: false),
                    Calendar = table.Column<bool>(nullable: false),
                    OnlyMonitoring = table.Column<bool>(nullable: false),
                    CanComment = table.Column<bool>(nullable: false),
                    CanSeeAllWorkers = table.Column<bool>(nullable: false),
                    AllFunctionality = table.Column<bool>(nullable: false),
                    BreakDurationMinutes = table.Column<int>(nullable: false),
                    AfterWorkSubtractBreakMinutes = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSettings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSettings_UserId",
                table: "UserSettings",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSettings");

            migrationBuilder.DropColumn(
                name: "CustomDepartment",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ExportCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Users");
        }
    }
}
