using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace P9YS.EntityFramework.Migrations
{
    public partial class Modify_Record : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Mark",
                table: "SuportRecords",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Mark",
                table: "RatingRecords",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mark",
                table: "SuportRecords");

            migrationBuilder.DropColumn(
                name: "Mark",
                table: "RatingRecords");
        }
    }
}
