using Microsoft.EntityFrameworkCore.Migrations;

namespace WEBAPI.Migrations
{
    public partial class breakenabled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Break_Jobs_JobId",
                table: "Break");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Break",
                table: "Break");

            migrationBuilder.RenameTable(
                name: "Break",
                newName: "Breaks");

            migrationBuilder.RenameIndex(
                name: "IX_Break_JobId",
                table: "Breaks",
                newName: "IX_Breaks_JobId");

            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "Breaks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Breaks",
                table: "Breaks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Breaks_Jobs_JobId",
                table: "Breaks",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Breaks_Jobs_JobId",
                table: "Breaks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Breaks",
                table: "Breaks");

            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "Breaks");

            migrationBuilder.RenameTable(
                name: "Breaks",
                newName: "Break");

            migrationBuilder.RenameIndex(
                name: "IX_Breaks_JobId",
                table: "Break",
                newName: "IX_Break_JobId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Break",
                table: "Break",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Break_Jobs_JobId",
                table: "Break",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
