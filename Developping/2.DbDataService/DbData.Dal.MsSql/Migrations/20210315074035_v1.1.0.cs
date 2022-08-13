using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DbData.Dal.MsSql.Migrations
{
    public partial class v110 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "City",
                type: "decimal(18,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(11,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "City",
                type: "decimal(18,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,8)");

            migrationBuilder.CreateTable(
                name: "ItemView",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    DetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImgThumb = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Author = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SavedInStopBy = table.Column<int>(type: "int", nullable: false),
                    Saved = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<int>(type: "int", nullable: false),
                    RankingByAdmin = table.Column<int>(type: "int", nullable: false),
                    RankingByUser = table.Column<int>(type: "int", nullable: false),
                    Like = table.Column<int>(type: "int", nullable: false),
                    NumberItemInCollection = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemView", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemView");

            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "City",
                type: "decimal(11,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "City",
                type: "decimal(10,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)");
        }
    }
}
