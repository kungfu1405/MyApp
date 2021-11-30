using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityStudy.Data.Migrations
{
    public partial class UpdateShopDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderEnity_Customer_CustomerId",
                table: "OrderEnity");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductEntity_CategoryEntity_CategoryId",
                table: "ProductEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrdersEntity_OrderEnity_OrderID",
                table: "ProductOrdersEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrdersEntity_ProductEntity_ProductID",
                table: "ProductOrdersEntity");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductOrdersEntity",
                table: "ProductOrdersEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductEntity",
                table: "ProductEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderEnity",
                table: "OrderEnity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryEntity",
                table: "CategoryEntity");

            migrationBuilder.RenameTable(
                name: "ProductOrdersEntity",
                newName: "ProductOrderTbl");

            migrationBuilder.RenameTable(
                name: "ProductEntity",
                newName: "ProductTbl");

            migrationBuilder.RenameTable(
                name: "OrderEnity",
                newName: "OrderTbl");

            migrationBuilder.RenameTable(
                name: "CategoryEntity",
                newName: "CategoryTbl");

            migrationBuilder.RenameIndex(
                name: "IX_ProductOrdersEntity_ProductID",
                table: "ProductOrderTbl",
                newName: "IX_ProductOrderTbl_ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductOrdersEntity_OrderID",
                table: "ProductOrderTbl",
                newName: "IX_ProductOrderTbl_OrderID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductEntity_CategoryId",
                table: "ProductTbl",
                newName: "IX_ProductTbl_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderEnity_CustomerId",
                table: "OrderTbl",
                newName: "IX_OrderTbl_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductOrderTbl",
                table: "ProductOrderTbl",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductTbl",
                table: "ProductTbl",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderTbl",
                table: "OrderTbl",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryTbl",
                table: "CategoryTbl",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderTbl_Customer_CustomerId",
                table: "OrderTbl",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrderTbl_OrderTbl_OrderID",
                table: "ProductOrderTbl",
                column: "OrderID",
                principalTable: "OrderTbl",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrderTbl_ProductTbl_ProductID",
                table: "ProductOrderTbl",
                column: "ProductID",
                principalTable: "ProductTbl",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTbl_CategoryTbl_CategoryId",
                table: "ProductTbl",
                column: "CategoryId",
                principalTable: "CategoryTbl",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderTbl_Customer_CustomerId",
                table: "OrderTbl");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrderTbl_OrderTbl_OrderID",
                table: "ProductOrderTbl");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrderTbl_ProductTbl_ProductID",
                table: "ProductOrderTbl");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTbl_CategoryTbl_CategoryId",
                table: "ProductTbl");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductTbl",
                table: "ProductTbl");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductOrderTbl",
                table: "ProductOrderTbl");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderTbl",
                table: "OrderTbl");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryTbl",
                table: "CategoryTbl");

            migrationBuilder.RenameTable(
                name: "ProductTbl",
                newName: "ProductEntity");

            migrationBuilder.RenameTable(
                name: "ProductOrderTbl",
                newName: "ProductOrdersEntity");

            migrationBuilder.RenameTable(
                name: "OrderTbl",
                newName: "OrderEnity");

            migrationBuilder.RenameTable(
                name: "CategoryTbl",
                newName: "CategoryEntity");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTbl_CategoryId",
                table: "ProductEntity",
                newName: "IX_ProductEntity_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductOrderTbl_ProductID",
                table: "ProductOrdersEntity",
                newName: "IX_ProductOrdersEntity_ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductOrderTbl_OrderID",
                table: "ProductOrdersEntity",
                newName: "IX_ProductOrdersEntity_OrderID");

            migrationBuilder.RenameIndex(
                name: "IX_OrderTbl_CustomerId",
                table: "OrderEnity",
                newName: "IX_OrderEnity_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductEntity",
                table: "ProductEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductOrdersEntity",
                table: "ProductOrdersEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderEnity",
                table: "OrderEnity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryEntity",
                table: "CategoryEntity",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderEnity_Customer_CustomerId",
                table: "OrderEnity",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductEntity_CategoryEntity_CategoryId",
                table: "ProductEntity",
                column: "CategoryId",
                principalTable: "CategoryEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrdersEntity_OrderEnity_OrderID",
                table: "ProductOrdersEntity",
                column: "OrderID",
                principalTable: "OrderEnity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrdersEntity_ProductEntity_ProductID",
                table: "ProductOrdersEntity",
                column: "ProductID",
                principalTable: "ProductEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
