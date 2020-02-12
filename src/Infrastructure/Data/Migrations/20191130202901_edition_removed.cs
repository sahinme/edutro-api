using Microsoft.EntityFrameworkCore.Migrations;

namespace Microsoft.eShopWeb.Infrastructure.Data.Migrations
{
    public partial class edition_removed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tenants_Editions_EditionId",
                table: "Tenants");

            migrationBuilder.DropIndex(
                name: "IX_Tenants_EditionId",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "EditionId",
                table: "Tenants");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EditionId",
                table: "Tenants",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_EditionId",
                table: "Tenants",
                column: "EditionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_Editions_EditionId",
                table: "Tenants",
                column: "EditionId",
                principalTable: "Editions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
