using Microsoft.EntityFrameworkCore.Migrations;

namespace Microsoft.eShopWeb.Infrastructure.Data.Migrations
{
    public partial class questions3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "Questions",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CourseId",
                table: "Answers",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EducatorId",
                table: "Answers",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EntityId",
                table: "Answers",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "EntityType",
                table: "Answers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "TenantId",
                table: "Answers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_UserId",
                table: "Questions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_CourseId",
                table: "Answers",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_EducatorId",
                table: "Answers",
                column: "EducatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_TenantId",
                table: "Answers",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Courses_CourseId",
                table: "Answers",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Educators_EducatorId",
                table: "Answers",
                column: "EducatorId",
                principalTable: "Educators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Tenants_TenantId",
                table: "Answers",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Users_UserId",
                table: "Questions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Courses_CourseId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Educators_EducatorId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Tenants_TenantId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Users_UserId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_UserId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Answers_CourseId",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_EducatorId",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_TenantId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "EducatorId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "EntityId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "EntityType",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Answers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Questions",
                nullable: true,
                oldClrType: typeof(long));
        }
    }
}
