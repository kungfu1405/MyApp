using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityStudy.Data.Migrations
{
    public partial class UpdateSchoolDB1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Grade_GradeId",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.RenameTable(
                name: "Students",
                newName: "StudentInfo");

            migrationBuilder.RenameIndex(
                name: "IX_Students_GradeId",
                table: "StudentInfo",
                newName: "IX_StudentInfo_GradeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentInfo",
                table: "StudentInfo",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentInfo_Grade_GradeId",
                table: "StudentInfo",
                column: "GradeId",
                principalTable: "Grade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentInfo_Grade_GradeId",
                table: "StudentInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentInfo",
                table: "StudentInfo");

            migrationBuilder.RenameTable(
                name: "StudentInfo",
                newName: "Students");

            migrationBuilder.RenameIndex(
                name: "IX_StudentInfo_GradeId",
                table: "Students",
                newName: "IX_Students_GradeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Grade_GradeId",
                table: "Students",
                column: "GradeId",
                principalTable: "Grade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
