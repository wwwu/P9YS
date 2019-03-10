using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace P9YS.EntityFramework.Migrations
{
    public partial class CreateMovieDraft : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovieDrafts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DyUrl = table.Column<string>(maxLength: 200, nullable: false),
                    DoubanUrl = table.Column<string>(maxLength: 200, nullable: false),
                    MovieName = table.Column<string>(maxLength: 50, nullable: false),
                    Resoures = table.Column<string>(nullable: true),
                    DoubanHtml = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    AddTime = table.Column<DateTime>(nullable: false),
                    Timestamp = table.Column<DateTime>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieDrafts", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 1,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 962, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 2,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 962, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 3,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 962, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 4,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 962, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 5,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 962, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 6,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 962, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 7,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 962, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 8,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 962, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 9,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 962, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 10,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 962, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 11,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 962, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 12,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 962, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 13,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 962, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 14,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 962, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 15,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 962, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 16,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 962, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 17,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 962, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 18,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 962, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 19,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 962, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 20,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 962, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 21,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 962, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 22,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 962, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 23,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 962, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 24,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 962, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 25,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 962, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 26,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 962, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 27,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 962, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 28,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 962, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 1,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 965, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 2,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 965, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 3,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 965, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 4,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 965, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 5,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 965, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 6,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 965, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 7,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 965, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 8,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 965, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 9,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 965, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 10,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 965, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 11,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 965, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 12,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 965, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 13,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 965, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 14,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 965, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 15,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 965, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 16,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 965, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 17,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 965, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 18,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 965, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 19,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 965, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 20,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 965, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 21,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 965, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 22,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 965, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 23,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 965, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 24,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 965, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 25,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 965, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 26,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 965, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 27,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 965, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 28,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 965, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 29,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 965, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 30,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 965, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 31,
                column: "AddTime",
                value: new DateTime(2019, 3, 8, 21, 41, 11, 965, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieDrafts");

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 1,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 2,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 3,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 4,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 5,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 6,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 7,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 8,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 9,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 10,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 11,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 12,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 13,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 14,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 15,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 16,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 17,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 18,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 19,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 20,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 21,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 22,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 23,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 24,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 25,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 26,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 27,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 28,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 1,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 2,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 3,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 4,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 5,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 6,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 7,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 8,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 9,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 10,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 11,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 12,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 13,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 14,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 15,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 16,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 17,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 18,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 19,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 20,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 21,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 22,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 23,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 24,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 25,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 26,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 27,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 28,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 29,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 30,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 31,
                column: "AddTime",
                value: new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local));
        }
    }
}
