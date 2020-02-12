using Microsoft.EntityFrameworkCore.Migrations;

namespace Microsoft.eShopWeb.Infrastructure.Data.Migrations
{
    public partial class pricetodecimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Courses",
                nullable: false,
                oldClrType: typeof(double));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Courses",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
