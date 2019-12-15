using Microsoft.EntityFrameworkCore.Migrations;

namespace app.web.Migrations
{
    public partial class AddRoads : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roads",
                columns: table => new
                {
                    Id = table.Column<uint>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Distance = table.Column<double>(nullable: false),
                    Place1Id = table.Column<uint>(nullable: false),
                    Place2Id = table.Column<uint>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roads_Places_Place1Id",
                        column: x => x.Place1Id,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Roads_Places_Place2Id",
                        column: x => x.Place2Id,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Roads_Place1Id",
                table: "Roads",
                column: "Place1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Roads_Place2Id",
                table: "Roads",
                column: "Place2Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Roads");
        }
    }
}
