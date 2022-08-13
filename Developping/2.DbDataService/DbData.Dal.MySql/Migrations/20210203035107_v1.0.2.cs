using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DbData.Dal.MySql.Migrations
{
    public partial class v102 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Ordinal",
                table: "ExperienceSession",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DefaultName",
                table: "Experience",
                type: "varchar(200) CHARACTER SET utf8mb4",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Experience",
                type: "varchar(500) CHARACTER SET utf8mb4",
                maxLength: 500,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserFollow",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    UserFollowingId = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFollow", x => new { x.UserId, x.UserFollowingId });
                });

            migrationBuilder.CreateTable(
                name: "UserProfile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    BannerUrl = table.Column<string>(type: "varchar(200) CHARACTER SET utf8mb4", maxLength: 200, nullable: true),
                    Intro = table.Column<string>(type: "varchar(500) CHARACTER SET utf8mb4", maxLength: 500, nullable: true),
                    TotalExperiences = table.Column<int>(type: "int", nullable: false),
                    TotalPlans = table.Column<int>(type: "int", nullable: false),
                    TotalComments = table.Column<long>(type: "bigint", nullable: false),
                    TotalRates = table.Column<long>(type: "bigint", nullable: false),
                    AvgRates = table.Column<double>(type: "double", nullable: false),
                    TotalFollowers = table.Column<int>(type: "int", nullable: false),
                    TotalFollowings = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile", x => x.Id);
                });

            migrationBuilder.Sql(@"update Experience
                set DefaultName = (select Title from ExperienceLanguage where ExperienceId = Experience.Id and LangCode='vn'),
                    Description = (select Description from ExperienceLanguage where ExperienceId = Experience.Id and LangCode='vn')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFollow");

            migrationBuilder.DropTable(
                name: "UserProfile");

            migrationBuilder.DropColumn(
                name: "Ordinal",
                table: "ExperienceSession");

            migrationBuilder.DropColumn(
                name: "DefaultName",
                table: "Experience");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Experience");
        }
    }
}
