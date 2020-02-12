using Microsoft.EntityFrameworkCore.Migrations;

namespace Microsoft.eShopWeb.Infrastructure.Data.Migrations
{
    public partial class tenantLocation_required : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tenants_Locations_LocationId",
                table: "Tenants");

            migrationBuilder.AlterColumn<long>(
                name: "LocationId",
                table: "Tenants",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_Locations_LocationId",
                table: "Tenants",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tenants_Locations_LocationId",
                table: "Tenants");

            migrationBuilder.AlterColumn<long>(
                name: "LocationId",
                table: "Tenants",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_Locations_LocationId",
                table: "Tenants",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
