using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityStudy.Data.Migrations
{
    public partial class updatedbShop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductTbl_CategoryTbl_CategoryId",
                table: "ProductTbl");

            migrationBuilder.DropIndex(
                name: "IX_ProductTbl_CategoryId",
                table: "ProductTbl");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "ProductTbl");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTbl_CateId",
                table: "ProductTbl",
                column: "CateId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTbl_CategoryTbl_CateId",
                table: "ProductTbl",
                column: "CateId",
                principalTable: "CategoryTbl",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductTbl_CategoryTbl_CateId",
                table: "ProductTbl");

            migrationBuilder.DropIndex(
                name: "IX_ProductTbl_CateId",
                table: "ProductTbl");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "ProductTbl",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductTbl_CategoryId",
                table: "ProductTbl",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTbl_CategoryTbl_CategoryId",
                table: "ProductTbl",
                column: "CategoryId",
                principalTable: "CategoryTbl",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
