using Microsoft.EntityFrameworkCore.Migrations;

namespace DbData.Dal.MsSql.Migrations
{
    public partial class v112 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RouteUri",
                table: "ItemView",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RouteUri",
                table: "ItemView");
        }
    }
}
