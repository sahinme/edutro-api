using Microsoft.EntityFrameworkCore.Migrations;

namespace Microsoft.eShopWeb.Infrastructure.Data.Migrations
{
    public partial class edition_eheh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Educators_Editions_EditionId",
                table: "Educators");

            migrationBuilder.DropIndex(
                name: "IX_Educators_EditionId",
                table: "Educators");

            migrationBuilder.DropColumn(
                name: "EditionId",
                table: "Educators");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EditionId",
                table: "Educators",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Educators_EditionId",
                table: "Educators",
                column: "EditionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Educators_Editions_EditionId",
                table: "Educators",
                column: "EditionId",
                principalTable: "Editions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
