using Microsoft.EntityFrameworkCore.Migrations;

namespace Microsoft.eShopWeb.Infrastructure.Data.Migrations
{
    public partial class questionsSSS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EducatorId",
                table: "Questions",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TenantId",
                table: "Questions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_EducatorId",
                table: "Questions",
                column: "EducatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_TenantId",
                table: "Questions",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Educators_EducatorId",
                table: "Questions",
                column: "EducatorId",
                principalTable: "Educators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Tenants_TenantId",
                table: "Questions",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Educators_EducatorId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Tenants_TenantId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_EducatorId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_TenantId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "EducatorId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Questions");
        }
    }
}
