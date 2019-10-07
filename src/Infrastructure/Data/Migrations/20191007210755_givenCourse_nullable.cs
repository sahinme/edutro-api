using Microsoft.EntityFrameworkCore.Migrations;

namespace Microsoft.eShopWeb.Infrastructure.Data.Migrations
{
    public partial class givenCourse_nullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GivenCourses_Educators_EducatorId",
                table: "GivenCourses");

            migrationBuilder.AlterColumn<long>(
                name: "EducatorId",
                table: "GivenCourses",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_GivenCourses_Educators_EducatorId",
                table: "GivenCourses",
                column: "EducatorId",
                principalTable: "Educators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GivenCourses_Educators_EducatorId",
                table: "GivenCourses");

            migrationBuilder.AlterColumn<long>(
                name: "EducatorId",
                table: "GivenCourses",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GivenCourses_Educators_EducatorId",
                table: "GivenCourses",
                column: "EducatorId",
                principalTable: "Educators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
