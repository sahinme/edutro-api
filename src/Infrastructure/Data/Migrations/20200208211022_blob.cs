using Microsoft.EntityFrameworkCore.Migrations;

namespace Microsoft.eShopWeb.Infrastructure.Data.Migrations
{
    public partial class blob : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Courses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Courses");
        }
    }
}
