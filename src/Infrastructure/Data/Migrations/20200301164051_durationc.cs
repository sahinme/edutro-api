using Microsoft.EntityFrameworkCore.Migrations;

namespace Microsoft.eShopWeb.Infrastructure.Data.Migrations
{
    public partial class durationc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "Courses",
                newName: "DurationType");

            migrationBuilder.AddColumn<int>(
                name: "DurationCount",
                table: "Courses",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationCount",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "DurationType",
                table: "Courses",
                newName: "Duration");
        }
    }
}
