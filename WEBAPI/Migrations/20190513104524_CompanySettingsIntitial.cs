using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WEBAPI.Migrations
{
    public partial class CompanySettingsIntitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanySettings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ObjectRequired = table.Column<bool>(nullable: false),
                    WorkTypeRequired = table.Column<bool>(nullable: false),
                    WorkRequired = table.Column<bool>(nullable: false),
                    EditCommentRequired = table.Column<bool>(nullable: false),
                    WorkAtNigth = table.Column<bool>(nullable: false),
                    MaximumWorkMinutes = table.Column<int>(nullable: false),
                    BreakTimeMinutes = table.Column<int>(nullable: false),
                    SubtractBreakWorkMinutes = table.Column<int>(nullable: false),
                    AutomaticBreak = table.Column<bool>(nullable: false),
                    GpsRequired = table.Column<bool>(nullable: false),
                    ActRequired = table.Column<bool>(nullable: false),
                    ContactNameRequired = table.Column<bool>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanySettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanySettings_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanySettings_CompanyId",
                table: "CompanySettings",
                column: "CompanyId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanySettings");
        }
    }
}
