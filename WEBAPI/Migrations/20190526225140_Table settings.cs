using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WEBAPI.Migrations
{
    public partial class Tablesettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TableSettings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Worker = table.Column<bool>(nullable: false),
                    Date = table.Column<bool>(nullable: false),
                    Start = table.Column<bool>(nullable: false),
                    End = table.Column<bool>(nullable: false),
                    Duration = table.Column<bool>(nullable: false),
                    Break = table.Column<bool>(nullable: false),
                    GpsStart = table.Column<bool>(nullable: false),
                    GpsEnd = table.Column<bool>(nullable: false),
                    Comment = table.Column<bool>(nullable: false),
                    Summ = table.Column<bool>(nullable: false),
                    AutomaticRefresh = table.Column<bool>(nullable: false),
                    GroupByWorker = table.Column<bool>(nullable: false),
                    GroupByDate = table.Column<bool>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TableSettings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TableSettings_UserId",
                table: "TableSettings",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TableSettings");
        }
    }
}
