using Microsoft.EntityFrameworkCore.Migrations;

namespace Microsoft.eShopWeb.Infrastructure.Data.Migrations
{
    public partial class password_filed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Tenants",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Educators",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Educators",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Educators");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Educators");
        }
    }
}
