using Microsoft.EntityFrameworkCore.Migrations;

namespace Microsoft.eShopWeb.Infrastructure.Data.Migrations
{
    public partial class ownerId_events : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OwnerId",
                table: "Events",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "OwnerType",
                table: "Events",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "OwnerType",
                table: "Events");
        }
    }
}
