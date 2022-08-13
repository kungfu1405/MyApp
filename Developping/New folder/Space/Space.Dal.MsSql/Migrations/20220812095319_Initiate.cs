using Microsoft.EntityFrameworkCore.Migrations;

namespace Space.Dal.MsSql.Migrations
{
    public partial class Initiate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    StateCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CountryCode = table.Column<string>(type: "char(2)", maxLength: 2, nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "City");
        }
    }
}
