﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Microsoft.eShopWeb.Infrastructure.Data.Migrations
{
    public partial class UserModel_1212 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GivenCourses_Educators_EducatorId",
                table: "GivenCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_GivenCourses_Tenants_TenantId",
                table: "GivenCourses");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_GivenCourses_Id",
                table: "GivenCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GivenCourses",
                table: "GivenCourses");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_GivenCourses_TenantId_CourseId",
                table: "GivenCourses");

            migrationBuilder.AlterColumn<long>(
                name: "TenantId",
                table: "GivenCourses",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "EducatorId",
                table: "GivenCourses",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddPrimaryKey(
                name: "PK_GivenCourses",
                table: "GivenCourses",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_GivenCourses_EducatorId",
                table: "GivenCourses",
                column: "EducatorId");

            migrationBuilder.CreateIndex(
                name: "IX_GivenCourses_TenantId",
                table: "GivenCourses",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_GivenCourses_Educators_EducatorId",
                table: "GivenCourses",
                column: "EducatorId",
                principalTable: "Educators",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GivenCourses_Educators_EducatorId",
                table: "GivenCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_GivenCourses_Tenants_TenantId",
                table: "GivenCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GivenCourses",
                table: "GivenCourses");

            migrationBuilder.DropIndex(
                name: "IX_GivenCourses_EducatorId",
                table: "GivenCourses");

            migrationBuilder.DropIndex(
                name: "IX_GivenCourses_TenantId",
                table: "GivenCourses");

            migrationBuilder.AlterColumn<long>(
                name: "TenantId",
                table: "GivenCourses",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "EducatorId",
                table: "GivenCourses",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_GivenCourses_Id",
                table: "GivenCourses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GivenCourses",
                table: "GivenCourses",
                columns: new[] { "EducatorId", "CourseId" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_GivenCourses_TenantId_CourseId",
                table: "GivenCourses",
                columns: new[] { "TenantId", "CourseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GivenCourses_Educators_EducatorId",
                table: "GivenCourses",
                column: "EducatorId",
                principalTable: "Educators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GivenCourses_Tenants_TenantId",
                table: "GivenCourses",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
