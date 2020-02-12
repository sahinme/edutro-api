using Microsoft.EntityFrameworkCore.Migrations;

namespace Microsoft.eShopWeb.Infrastructure.Data.Migrations
{
    public partial class tenantadds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AboutUs",
                table: "Tenants",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Tenants",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AboutUs",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Tenants");
        }
    }
}
