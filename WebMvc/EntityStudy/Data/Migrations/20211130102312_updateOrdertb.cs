using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityStudy.Data.Migrations
{
    public partial class updateOrdertb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductTbl_CategoryTbl_CateId",
                table: "ProductTbl");

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "OrderTbl",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "OrderTbl",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "OrderTbl",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderTbl_ProductId",
                table: "OrderTbl",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderTbl_ProductTbl_ProductId",
                table: "OrderTbl",
                column: "ProductId",
                principalTable: "ProductTbl",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTbl_CategoryTbl_CateId",
                table: "ProductTbl",
                column: "CateId",
                principalTable: "CategoryTbl",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderTbl_ProductTbl_ProductId",
                table: "OrderTbl");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTbl_CategoryTbl_CateId",
                table: "ProductTbl");

            migrationBuilder.DropIndex(
                name: "IX_OrderTbl_ProductId",
                table: "OrderTbl");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "OrderTbl");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "OrderTbl");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "OrderTbl");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTbl_CategoryTbl_CateId",
                table: "ProductTbl",
                column: "CateId",
                principalTable: "CategoryTbl",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
