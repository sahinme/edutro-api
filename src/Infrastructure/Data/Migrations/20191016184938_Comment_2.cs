using Microsoft.EntityFrameworkCore.Migrations;

namespace Microsoft.eShopWeb.Infrastructure.Data.Migrations
{
    public partial class Comment_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Courses_CourseId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Educators_EducatorId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Tenants_TenantId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_CourseId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_EducatorId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_TenantId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "EducatorId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Comments");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_EntityId",
                table: "Comments",
                column: "EntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Courses_EntityId",
                table: "Comments",
                column: "EntityId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Educators_EntityId",
                table: "Comments",
                column: "EntityId",
                principalTable: "Educators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Tenants_EntityId",
                table: "Comments",
                column: "EntityId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Courses_EntityId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Educators_EntityId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Tenants_EntityId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_EntityId",
                table: "Comments");

            migrationBuilder.AddColumn<long>(
                name: "CourseId",
                table: "Comments",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EducatorId",
                table: "Comments",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TenantId",
                table: "Comments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CourseId",
                table: "Comments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_EducatorId",
                table: "Comments",
                column: "EducatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_TenantId",
                table: "Comments",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Courses_CourseId",
                table: "Comments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Educators_EducatorId",
                table: "Comments",
                column: "EducatorId",
                principalTable: "Educators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Tenants_TenantId",
                table: "Comments",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
