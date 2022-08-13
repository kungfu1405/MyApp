using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DbData.Dal.MsSql.Migrations
{
    public partial class v100 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "City",
                type: "decimal(11,8)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "City",
                type: "decimal(10,8)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.CreateTable(
                name: "AttProperty",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyGroup = table.Column<int>(type: "int", nullable: false),
                    DataType = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CssIcon = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Ordinal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttProperty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Destination",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Continent = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StateId = table.Column<int>(type: "int", nullable: true),
                    StateName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    CityName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DefaultName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    RouteUri = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ThumbnailUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    BannerUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DestinationOverviewId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DefaultGalleryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(10,8)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(11,8)", nullable: true),
                    MapImageUri = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TotalLikes = table.Column<long>(type: "bigint", nullable: false),
                    TotalRates = table.Column<long>(type: "bigint", nullable: false),
                    AvgRates = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destination", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Destination_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Destination_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Destination_State_StateId",
                        column: x => x.StateId,
                        principalTable: "State",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DestinationOverview",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LangCode = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SubTitle = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    RouteUri = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThumbnailUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Author = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DestinationOverview", x => new { x.Id, x.LangCode });
                });

            migrationBuilder.CreateTable(
                name: "ExperienceSessionImage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExperienceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExperienceSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImagerUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Ordinal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperienceSessionImage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicle",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CssIcon = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Ordinal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attraction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DestinationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RouteUri = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DefaultName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AttractionTypes = table.Column<int>(type: "int", nullable: false),
                    TouristClasses = table.Column<int>(type: "int", nullable: false),
                    TouristObjects = table.Column<int>(type: "int", nullable: false),
                    PropertyGroups = table.Column<int>(type: "int", nullable: false),
                    ThumbnailUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DefaultGalleryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DefaultExperienceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Contact = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Website = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    VisitDuration = table.Column<int>(type: "int", nullable: true),
                    PriceFrom = table.Column<double>(type: "float", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(10,8)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(11,8)", nullable: true),
                    MapImageUri = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TotalLikes = table.Column<long>(type: "bigint", nullable: false),
                    TotalComments = table.Column<int>(type: "int", nullable: false),
                    TotalRates = table.Column<int>(type: "int", nullable: false),
                    AvgRates = table.Column<double>(type: "float", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FromExperience = table.Column<bool>(type: "bit", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attraction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attraction_Destination_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Destination",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "DestinationLanguage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DestinationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LangCode = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DestinationLanguage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DestinationLanguage_Destination_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Destination",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Experience",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DestinationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RouteUri = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ThumbnailUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RefPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Author = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalComments = table.Column<int>(type: "int", nullable: false),
                    TotalLikes = table.Column<int>(type: "int", nullable: false),
                    TotalRates = table.Column<int>(type: "int", nullable: false),
                    AvgRates = table.Column<double>(type: "float", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experience", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experience_Destination_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Destination",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DestinationTag",
                columns: table => new
                {
                    DestinationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DestinationTag", x => new { x.DestinationId, x.TagId });
                    table.ForeignKey(
                        name: "FK_DestinationTag_Destination_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Destination",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_DestinationTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "DestinationLink",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromDestinationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToDestinationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Distance = table.Column<int>(type: "int", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: true),
                    PriceFrom = table.Column<double>(type: "float", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LinkedCount = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DestinationLink", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DestinationLink_Destination_FromDestinationId",
                        column: x => x.FromDestinationId,
                        principalTable: "Destination",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_DestinationLink_Destination_ToDestinationId",
                        column: x => x.ToDestinationId,
                        principalTable: "Destination",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_DestinationLink_Vehicle_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AttractionLanguage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttractionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LangCode = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttractionLanguage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttractionLanguage_Attraction_AttractionId",
                        column: x => x.AttractionId,
                        principalTable: "Attraction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AttractionLink",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromAttractionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToAttractionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Distance = table.Column<int>(type: "int", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: true),
                    PriceFrom = table.Column<double>(type: "float", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LinkedCount = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttractionLink", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttractionLink_Attraction_FromAttractionId",
                        column: x => x.FromAttractionId,
                        principalTable: "Attraction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AttractionLink_Attraction_ToAttractionId",
                        column: x => x.ToAttractionId,
                        principalTable: "Attraction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AttractionLink_Vehicle_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AttractionProperty",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttractionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Ordinal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttractionProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttractionProperty_AttProperty_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "AttProperty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AttractionProperty_Attraction_AttractionId",
                        column: x => x.AttractionId,
                        principalTable: "Attraction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AttractionTag",
                columns: table => new
                {
                    AttractionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttractionTag", x => new { x.AttractionId, x.TagId });
                    table.ForeignKey(
                        name: "FK_AttractionTag_Attraction_AttractionId",
                        column: x => x.AttractionId,
                        principalTable: "Attraction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AttractionTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AttractionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExperienceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultLangCode = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    StarVoted = table.Column<int>(type: "int", nullable: false),
                    TotalLikes = table.Column<int>(type: "int", nullable: false),
                    TotalDislike = table.Column<int>(type: "int", nullable: false),
                    TotalReply = table.Column<int>(type: "int", nullable: false),
                    CreateByUserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserAvatarUrl = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Attraction_AttractionId",
                        column: x => x.AttractionId,
                        principalTable: "Attraction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comment_Comment_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Comment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comment_Experience_ExperienceId",
                        column: x => x.ExperienceId,
                        principalTable: "Experience",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExperienceAttractionRef",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExperienceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttractionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperienceAttractionRef", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExperienceAttractionRef_Attraction_AttractionId",
                        column: x => x.AttractionId,
                        principalTable: "Attraction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ExperienceAttractionRef_Experience_ExperienceId",
                        column: x => x.ExperienceId,
                        principalTable: "Experience",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ExperienceLanguage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExperienceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LangCode = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperienceLanguage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExperienceLanguage_Experience_ExperienceId",
                        column: x => x.ExperienceId,
                        principalTable: "Experience",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ExperienceSession",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExperienceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttractionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DefaultGalleryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ImageDisplayType = table.Column<int>(type: "int", nullable: false),
                    LangCode = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    SubTitle = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TranslateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperienceSession", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExperienceSession_Attraction_AttractionId",
                        column: x => x.AttractionId,
                        principalTable: "Attraction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExperienceSession_Experience_ExperienceId",
                        column: x => x.ExperienceId,
                        principalTable: "Experience",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ExperienceTag",
                columns: table => new
                {
                    ExperienceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperienceTag", x => new { x.ExperienceId, x.TagId });
                    table.ForeignKey(
                        name: "FK_ExperienceTag_Experience_ExperienceId",
                        column: x => x.ExperienceId,
                        principalTable: "Experience",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ExperienceTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Comment_LikeLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LikeStatus = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment_LikeLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_LikeLog_Comment_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "CommentImage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentImage_Comment_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attraction_DestinationId",
                table: "Attraction",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_AttractionLanguage_AttractionId",
                table: "AttractionLanguage",
                column: "AttractionId");

            migrationBuilder.CreateIndex(
                name: "IX_AttractionLink_FromAttractionId",
                table: "AttractionLink",
                column: "FromAttractionId");

            migrationBuilder.CreateIndex(
                name: "IX_AttractionLink_ToAttractionId",
                table: "AttractionLink",
                column: "ToAttractionId");

            migrationBuilder.CreateIndex(
                name: "IX_AttractionLink_VehicleId",
                table: "AttractionLink",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_AttractionProperty_AttractionId",
                table: "AttractionProperty",
                column: "AttractionId");

            migrationBuilder.CreateIndex(
                name: "IX_AttractionProperty_PropertyId",
                table: "AttractionProperty",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_AttractionTag_TagId",
                table: "AttractionTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_AttractionId",
                table: "Comment",
                column: "AttractionId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ExperienceId",
                table: "Comment",
                column: "ExperienceId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ParentId",
                table: "Comment",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_LikeLog_CommentId",
                table: "Comment_LikeLog",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentImage_CommentId",
                table: "CommentImage",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Destination_CityId",
                table: "Destination",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Destination_CountryId",
                table: "Destination",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Destination_StateId",
                table: "Destination",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_DestinationLanguage_DestinationId",
                table: "DestinationLanguage",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_DestinationLink_FromDestinationId",
                table: "DestinationLink",
                column: "FromDestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_DestinationLink_ToDestinationId",
                table: "DestinationLink",
                column: "ToDestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_DestinationLink_VehicleId",
                table: "DestinationLink",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_DestinationTag_TagId",
                table: "DestinationTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Experience_DestinationId",
                table: "Experience",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperienceAttractionRef_AttractionId",
                table: "ExperienceAttractionRef",
                column: "AttractionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperienceAttractionRef_ExperienceId",
                table: "ExperienceAttractionRef",
                column: "ExperienceId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperienceLanguage_ExperienceId",
                table: "ExperienceLanguage",
                column: "ExperienceId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperienceSession_AttractionId",
                table: "ExperienceSession",
                column: "AttractionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperienceSession_ExperienceId",
                table: "ExperienceSession",
                column: "ExperienceId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperienceTag_TagId",
                table: "ExperienceTag",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttractionLanguage");

            migrationBuilder.DropTable(
                name: "AttractionLink");

            migrationBuilder.DropTable(
                name: "AttractionProperty");

            migrationBuilder.DropTable(
                name: "AttractionTag");

            migrationBuilder.DropTable(
                name: "Comment_LikeLog");

            migrationBuilder.DropTable(
                name: "CommentImage");

            migrationBuilder.DropTable(
                name: "DestinationLanguage");

            migrationBuilder.DropTable(
                name: "DestinationLink");

            migrationBuilder.DropTable(
                name: "DestinationOverview");

            migrationBuilder.DropTable(
                name: "DestinationTag");

            migrationBuilder.DropTable(
                name: "ExperienceAttractionRef");

            migrationBuilder.DropTable(
                name: "ExperienceLanguage");

            migrationBuilder.DropTable(
                name: "ExperienceSession");

            migrationBuilder.DropTable(
                name: "ExperienceSessionImage");

            migrationBuilder.DropTable(
                name: "ExperienceTag");

            migrationBuilder.DropTable(
                name: "AttProperty");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Vehicle");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Attraction");

            migrationBuilder.DropTable(
                name: "Experience");

            migrationBuilder.DropTable(
                name: "Destination");

            migrationBuilder.AlterColumn<double>(
                name: "Longitude",
                table: "City",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(11,8)");

            migrationBuilder.AlterColumn<double>(
                name: "Latitude",
                table: "City",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,8)");
        }
    }
}
