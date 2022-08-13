using Microsoft.EntityFrameworkCore.Migrations;

namespace DbData.Dal.MsSql.Migrations
{
    public partial class v111 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TopView",
                table: "ItemView",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TopView",
                table: "ItemView");
        }
    }
}
