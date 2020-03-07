using Microsoft.EntityFrameworkCore.Migrations;

namespace Microsoft.eShopWeb.Infrastructure.Data.Migrations
{
    public partial class courseisActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseState",
                table: "Courses");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Courses",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Courses");

            migrationBuilder.AddColumn<int>(
                name: "CourseState",
                table: "Courses",
                nullable: false,
                defaultValue: 0);
        }
    }
}
