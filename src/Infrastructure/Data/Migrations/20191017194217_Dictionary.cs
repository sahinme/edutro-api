using Microsoft.EntityFrameworkCore.Migrations;

namespace Microsoft.eShopWeb.Infrastructure.Data.Migrations
{
    public partial class Dictionary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "_Content",
                table: "Notifications");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Notifications",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Notifications");

            migrationBuilder.AddColumn<string>(
                name: "_Content",
                table: "Notifications",
                nullable: true);
        }
    }
}
