using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DbData.Dal.MySql.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Continent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50) CHARACTER SET utf8mb4", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Continent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ContinentId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "varchar(50) CHARACTER SET utf8mb4", maxLength: 50, nullable: false),
                    Iso2 = table.Column<string>(type: "char(2)", maxLength: 2, nullable: true),
                    Iso3 = table.Column<string>(type: "char(3)", maxLength: 3, nullable: true),
                    PhoneCode = table.Column<string>(type: "varchar(50) CHARACTER SET utf8mb4", maxLength: 50, nullable: true),
                    Capital = table.Column<string>(type: "varchar(50) CHARACTER SET utf8mb4", maxLength: 50, nullable: true),
                    Currency = table.Column<string>(type: "varchar(50) CHARACTER SET utf8mb4", maxLength: 50, nullable: true),
                    Native = table.Column<string>(type: "varchar(50) CHARACTER SET utf8mb4", maxLength: 50, nullable: true),
                    Emoji = table.Column<string>(type: "varchar(50) CHARACTER SET utf8mb4", maxLength: 50, nullable: true),
                    EmojiU = table.Column<string>(type: "varchar(50) CHARACTER SET utf8mb4", maxLength: 50, nullable: true),
                    WikiDataId = table.Column<string>(type: "varchar(50) CHARACTER SET utf8mb4", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Country_Continent_ContinentId",
                        column: x => x.ContinentId,
                        principalTable: "Continent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100) CHARACTER SET utf8mb4", maxLength: 100, nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CountryCode = table.Column<string>(type: "char(2)", maxLength: 2, nullable: false),
                    FipsCode = table.Column<string>(type: "varchar(50) CHARACTER SET utf8mb4", maxLength: 50, nullable: true),
                    Iso2 = table.Column<string>(type: "varchar(50) CHARACTER SET utf8mb4", maxLength: 50, nullable: true),
                    WikiDataId = table.Column<string>(type: "varchar(50) CHARACTER SET utf8mb4", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.Id);
                    table.ForeignKey(
                        name: "FK_State_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50) CHARACTER SET utf8mb4", maxLength: 50, nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    StateCode = table.Column<string>(type: "varchar(50) CHARACTER SET utf8mb4", maxLength: 50, nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CountryCode = table.Column<string>(type: "char(2)", maxLength: 2, nullable: false),
                    Latitude = table.Column<double>(type: "double", nullable: false),
                    Longitude = table.Column<double>(type: "double", nullable: false),
                    WikiDataId = table.Column<string>(type: "varchar(50) CHARACTER SET utf8mb4", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_City_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_City_State_StateId",
                        column: x => x.StateId,
                        principalTable: "State",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_City_CountryId",
                table: "City",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_City_StateId",
                table: "City",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_ContinentId",
                table: "Country",
                column: "ContinentId");

            migrationBuilder.CreateIndex(
                name: "IX_State_CountryId",
                table: "State",
                column: "CountryId");

            migrationBuilder.Sql(@"
INSERT Continent (Id, Name) VALUES (1, 'Asia');
INSERT Continent (Id, Name) VALUES (2, 'Africa');
INSERT Continent (Id, Name) VALUES (3, 'Europe');
INSERT Continent (Id, Name) VALUES (4, 'Oceania');
INSERT Continent (Id, Name) VALUES (5, 'Latin America');
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "State");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Continent");
        }
    }
}
