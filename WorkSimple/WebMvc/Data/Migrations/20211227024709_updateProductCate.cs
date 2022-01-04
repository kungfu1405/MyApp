using Microsoft.EntityFrameworkCore.Migrations;

namespace WebMvc.Data.Migrations
{
    public partial class updateProductCate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "tblProduct",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "tblCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCategory", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblProduct_CateId",
                table: "tblProduct",
                column: "CateId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblProduct_tblCategory_CateId",
                table: "tblProduct",
                column: "CateId",
                principalTable: "tblCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblProduct_tblCategory_CateId",
                table: "tblProduct");

            migrationBuilder.DropTable(
                name: "tblCategory");

            migrationBuilder.DropIndex(
                name: "IX_tblProduct_CateId",
                table: "tblProduct");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "tblProduct",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);
        }
    }
}
