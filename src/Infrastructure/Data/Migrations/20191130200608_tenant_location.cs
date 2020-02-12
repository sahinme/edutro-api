using Microsoft.EntityFrameworkCore.Migrations;

namespace Microsoft.eShopWeb.Infrastructure.Data.Migrations
{
    public partial class tenant_location : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "LocationId",
                table: "Tenants",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_LocationId",
                table: "Tenants",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_Locations_LocationId",
                table: "Tenants",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tenants_Locations_LocationId",
                table: "Tenants");

            migrationBuilder.DropIndex(
                name: "IX_Tenants_LocationId",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Tenants");
        }
    }
}
