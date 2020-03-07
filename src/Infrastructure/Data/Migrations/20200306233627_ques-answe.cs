using Microsoft.EntityFrameworkCore.Migrations;

namespace Microsoft.eShopWeb.Infrastructure.Data.Migrations
{
    public partial class quesanswe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Educators_EntityId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Tenants_EntityId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Users_EntityId",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_EntityId",
                table: "Answers");

            migrationBuilder.AddColumn<long>(
                name: "EducatorId",
                table: "Answers",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TenantId",
                table: "Answers",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Answers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answers_EducatorId",
                table: "Answers",
                column: "EducatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_TenantId",
                table: "Answers",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_UserId",
                table: "Answers",
                column: "UserId");

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
                name: "FK_Answers_Educators_EducatorId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Tenants_TenantId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Users_UserId",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_EducatorId",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_TenantId",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_UserId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "EducatorId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Answers");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_EntityId",
                table: "Answers",
                column: "EntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Educators_EntityId",
                table: "Answers",
                column: "EntityId",
                principalTable: "Educators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Tenants_EntityId",
                table: "Answers",
                column: "EntityId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Users_EntityId",
                table: "Answers",
                column: "EntityId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
