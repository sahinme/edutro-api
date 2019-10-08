using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Microsoft.eShopWeb.Infrastructure.Data.Migrations
{
    public partial class Course_tenant_educator_manytomany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Tenants_TenantId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_GivenCourses_Tenants_TenantId",
                table: "GivenCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GivenCourses",
                table: "GivenCourses");

            migrationBuilder.DropIndex(
                name: "IX_GivenCourses_TenantId",
                table: "GivenCourses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_TenantId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Courses");

            migrationBuilder.AlterColumn<long>(
                name: "TenantId",
                table: "GivenCourses",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "GivenCourses",
                nullable: false,
                oldClrType: typeof(long))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_GivenCourses_Id",
                table: "GivenCourses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GivenCourses",
                table: "GivenCourses",
                columns: new[] { "TenantId", "CourseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GivenCourses_Tenants_TenantId",
                table: "GivenCourses",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GivenCourses_Tenants_TenantId",
                table: "GivenCourses");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_GivenCourses_Id",
                table: "GivenCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GivenCourses",
                table: "GivenCourses");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "GivenCourses",
                nullable: false,
                oldClrType: typeof(long))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "TenantId",
                table: "GivenCourses",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "TenantId",
                table: "Courses",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GivenCourses",
                table: "GivenCourses",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_GivenCourses_TenantId",
                table: "GivenCourses",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TenantId",
                table: "Courses",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Tenants_TenantId",
                table: "Courses",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GivenCourses_Tenants_TenantId",
                table: "GivenCourses",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
