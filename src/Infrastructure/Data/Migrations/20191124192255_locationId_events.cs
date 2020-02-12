using Microsoft.EntityFrameworkCore.Migrations;

namespace Microsoft.eShopWeb.Infrastructure.Data.Migrations
{
    public partial class locationId_events : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Events",
                newName: "Address");

            migrationBuilder.AddColumn<long>(
                name: "LocationId",
                table: "Events",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Events_LocationId",
                table: "Events",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Locations_LocationId",
                table: "Events",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Locations_LocationId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_LocationId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Events",
                newName: "Location");
        }
    }
}
