using Microsoft.EntityFrameworkCore.Migrations;

namespace Microsoft.eShopWeb.Infrastructure.Data.Migrations
{
    public partial class givencourseeducator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CourseId1",
                table: "GivenCourses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GivenCourses_CourseId1",
                table: "GivenCourses",
                column: "CourseId1");

            migrationBuilder.AddForeignKey(
                name: "FK_GivenCourses_Courses_CourseId1",
                table: "GivenCourses",
                column: "CourseId1",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GivenCourses_Courses_CourseId1",
                table: "GivenCourses");

            migrationBuilder.DropIndex(
                name: "IX_GivenCourses_CourseId1",
                table: "GivenCourses");

            migrationBuilder.DropColumn(
                name: "CourseId1",
                table: "GivenCourses");
        }
    }
}
