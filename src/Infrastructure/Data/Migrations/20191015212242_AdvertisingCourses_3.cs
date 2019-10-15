﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Microsoft.eShopWeb.Infrastructure.Data.Migrations
{
    public partial class AdvertisingCourses_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AdvertisingCourses_CourseId",
                table: "AdvertisingCourses");

            migrationBuilder.AddColumn<long>(
                name: "AdvertisingId",
                table: "Courses",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsEnded",
                table: "AdvertisingCourses",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_AdvertisingId",
                table: "Courses",
                column: "AdvertisingId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisingCourses_CourseId",
                table: "AdvertisingCourses",
                column: "CourseId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AdvertisingCourses_AdvertisingId",
                table: "Courses",
                column: "AdvertisingId",
                principalTable: "AdvertisingCourses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AdvertisingCourses_AdvertisingId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_AdvertisingId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_AdvertisingCourses_CourseId",
                table: "AdvertisingCourses");

            migrationBuilder.DropColumn(
                name: "AdvertisingId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "IsEnded",
                table: "AdvertisingCourses");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisingCourses_CourseId",
                table: "AdvertisingCourses",
                column: "CourseId");
        }
    }
}