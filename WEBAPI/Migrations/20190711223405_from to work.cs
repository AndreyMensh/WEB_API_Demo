using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WEBAPI.Migrations
{
    public partial class fromtowork : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FromWork",
                table: "CompanySettings",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ToWork",
                table: "CompanySettings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromWork",
                table: "CompanySettings");

            migrationBuilder.DropColumn(
                name: "ToWork",
                table: "CompanySettings");
        }
    }
}
