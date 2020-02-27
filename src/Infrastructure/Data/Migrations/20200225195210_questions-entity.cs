using Microsoft.EntityFrameworkCore.Migrations;

namespace Microsoft.eShopWeb.Infrastructure.Data.Migrations
{
    public partial class questionsentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Courses_CourseId",
                table: "Answers");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Answers",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Answers_CourseId",
                table: "Answers",
                newName: "IX_Answers_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Users_UserId",
                table: "Answers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Users_UserId",
                table: "Answers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Answers",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Answers_UserId",
                table: "Answers",
                newName: "IX_Answers_CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Courses_CourseId",
                table: "Answers",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
