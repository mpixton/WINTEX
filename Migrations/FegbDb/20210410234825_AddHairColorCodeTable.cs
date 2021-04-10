using Microsoft.EntityFrameworkCore.Migrations;

namespace WINTEX.Migrations.FegbDb
{
    public partial class AddHairColorCodeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HairColorCodes",
                columns: table => new
                {
                    HairColorCode = table.Column<string>(type: "text", nullable: false),
                    HairColorDescription = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HairColorCodes", x => x.HairColorCode);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HairColorCodes");
        }
    }
}
