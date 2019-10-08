using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Microsoft.eShopWeb.Infrastructure.Data.Migrations
{
    public partial class Tenant_educator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Educators_Tenants_TenantId",
                table: "Educators");

            migrationBuilder.DropIndex(
                name: "IX_Educators_TenantId",
                table: "Educators");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Educators");

            migrationBuilder.CreateTable(
                name: "TenantEducator",
                columns: table => new
                {
                    TenantId = table.Column<long>(nullable: false),
                    EducatorId = table.Column<long>(nullable: false),
                    Id = table.Column<long>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantEducator", x => new { x.TenantId, x.EducatorId });
                    table.UniqueConstraint("AK_TenantEducator_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantEducator_Educators_EducatorId",
                        column: x => x.EducatorId,
                        principalTable: "Educators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TenantEducator_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TenantEducator_EducatorId",
                table: "TenantEducator",
                column: "EducatorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TenantEducator");

            migrationBuilder.AddColumn<long>(
                name: "TenantId",
                table: "Educators",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Educators_TenantId",
                table: "Educators",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Educators_Tenants_TenantId",
                table: "Educators",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
