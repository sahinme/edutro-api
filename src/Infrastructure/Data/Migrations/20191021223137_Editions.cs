using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Microsoft.eShopWeb.Infrastructure.Data.Migrations
{
    public partial class Editions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EditionId",
                table: "Tenants",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "EditionId",
                table: "Educators",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Editions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CourseCount = table.Column<int>(nullable: false),
                    EventCount = table.Column<int>(nullable: false),
                    LiveSupport = table.Column<bool>(nullable: false),
                    Price = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Editions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_EditionId",
                table: "Tenants",
                column: "EditionId");

            migrationBuilder.CreateIndex(
                name: "IX_Educators_EditionId",
                table: "Educators",
                column: "EditionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Educators_Editions_EditionId",
                table: "Educators",
                column: "EditionId",
                principalTable: "Editions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_Editions_EditionId",
                table: "Tenants",
                column: "EditionId",
                principalTable: "Editions",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Educators_Editions_EditionId",
                table: "Educators");

            migrationBuilder.DropForeignKey(
                name: "FK_Tenants_Editions_EditionId",
                table: "Tenants");

            migrationBuilder.DropTable(
                name: "Editions");

            migrationBuilder.DropIndex(
                name: "IX_Tenants_EditionId",
                table: "Tenants");

            migrationBuilder.DropIndex(
                name: "IX_Educators_EditionId",
                table: "Educators");

            migrationBuilder.DropColumn(
                name: "EditionId",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "EditionId",
                table: "Educators");
        }
    }
}
