using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DbData.Dal.MsSql.Migrations
{
    public partial class v101 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ExperienceSession",
                table: "ExperienceSession");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "ExperienceSessionImage",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExperienceSession",
                table: "ExperienceSession",
                columns: new[] { "Id", "LangCode" });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ExperienceSession",
                table: "ExperienceSession");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "ExperienceSessionImage");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExperienceSession",
                table: "ExperienceSession",
                column: "Id");
        }
    }
}
