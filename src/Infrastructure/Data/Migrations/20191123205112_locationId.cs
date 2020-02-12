using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Microsoft.eShopWeb.Infrastructure.Data.Migrations
{
    public partial class locationId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Courses",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LocationId",
                table: "Courses",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_LocationId",
                table: "Courses",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Locations_LocationId",
                table: "Courses",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Locations_LocationId",
                table: "Courses");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Courses_LocationId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Courses");
        }
    }
}
