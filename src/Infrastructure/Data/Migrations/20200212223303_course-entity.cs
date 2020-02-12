using Microsoft.EntityFrameworkCore.Migrations;

namespace Microsoft.eShopWeb.Infrastructure.Data.Migrations
{
    public partial class courseentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Certificate",
                table: "Courses",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CertificateOfParticipation",
                table: "Courses",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPrice",
                table: "Courses",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Duration",
                table: "Courses",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "OnlineVideo",
                table: "Courses",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Requirements",
                table: "Courses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Teachings",
                table: "Courses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Certificate",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CertificateOfParticipation",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "DiscountPrice",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "OnlineVideo",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Requirements",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Teachings",
                table: "Courses");
        }
    }
}
