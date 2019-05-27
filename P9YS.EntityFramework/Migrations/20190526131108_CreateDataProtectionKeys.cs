using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace P9YS.EntityFramework.Migrations
{
    public partial class CreateDataProtectionKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "MovieDrafts",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "DataProtectionKeys",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FriendlyName = table.Column<string>(nullable: true),
                    Xml = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataProtectionKeys", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 1,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 986, DateTimeKind.Local).AddTicks(4755));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 2,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 986, DateTimeKind.Local).AddTicks(4755));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 3,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 986, DateTimeKind.Local).AddTicks(4755));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 4,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 986, DateTimeKind.Local).AddTicks(4755));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 5,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 986, DateTimeKind.Local).AddTicks(4755));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 6,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 986, DateTimeKind.Local).AddTicks(4755));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 7,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 986, DateTimeKind.Local).AddTicks(4755));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 8,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 986, DateTimeKind.Local).AddTicks(4755));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 9,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 986, DateTimeKind.Local).AddTicks(4755));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 10,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 986, DateTimeKind.Local).AddTicks(4755));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 11,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 986, DateTimeKind.Local).AddTicks(4755));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 12,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 986, DateTimeKind.Local).AddTicks(4755));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 13,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 986, DateTimeKind.Local).AddTicks(4755));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 14,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 986, DateTimeKind.Local).AddTicks(4755));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 15,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 986, DateTimeKind.Local).AddTicks(4755));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 16,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 986, DateTimeKind.Local).AddTicks(4755));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 17,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 986, DateTimeKind.Local).AddTicks(4755));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 18,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 986, DateTimeKind.Local).AddTicks(4755));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 19,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 986, DateTimeKind.Local).AddTicks(4755));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 20,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 986, DateTimeKind.Local).AddTicks(4755));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 21,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 986, DateTimeKind.Local).AddTicks(4755));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 22,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 986, DateTimeKind.Local).AddTicks(4755));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 23,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 986, DateTimeKind.Local).AddTicks(4755));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 24,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 986, DateTimeKind.Local).AddTicks(4755));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 25,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 986, DateTimeKind.Local).AddTicks(4755));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 26,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 986, DateTimeKind.Local).AddTicks(4755));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 27,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 986, DateTimeKind.Local).AddTicks(4755));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 28,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 986, DateTimeKind.Local).AddTicks(4755));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 1,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 988, DateTimeKind.Local).AddTicks(2851));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 2,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 988, DateTimeKind.Local).AddTicks(2851));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 3,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 988, DateTimeKind.Local).AddTicks(2851));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 4,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 988, DateTimeKind.Local).AddTicks(2851));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 5,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 988, DateTimeKind.Local).AddTicks(2851));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 6,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 988, DateTimeKind.Local).AddTicks(2851));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 7,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 988, DateTimeKind.Local).AddTicks(2851));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 8,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 988, DateTimeKind.Local).AddTicks(2851));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 9,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 988, DateTimeKind.Local).AddTicks(2851));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 10,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 988, DateTimeKind.Local).AddTicks(2851));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 11,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 988, DateTimeKind.Local).AddTicks(2851));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 12,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 988, DateTimeKind.Local).AddTicks(2851));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 13,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 988, DateTimeKind.Local).AddTicks(2851));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 14,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 988, DateTimeKind.Local).AddTicks(2851));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 15,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 988, DateTimeKind.Local).AddTicks(2851));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 16,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 988, DateTimeKind.Local).AddTicks(2851));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 17,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 988, DateTimeKind.Local).AddTicks(2851));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 18,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 988, DateTimeKind.Local).AddTicks(2851));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 19,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 988, DateTimeKind.Local).AddTicks(2851));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 20,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 988, DateTimeKind.Local).AddTicks(2851));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 21,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 988, DateTimeKind.Local).AddTicks(2851));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 22,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 988, DateTimeKind.Local).AddTicks(2851));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 23,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 988, DateTimeKind.Local).AddTicks(2851));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 24,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 988, DateTimeKind.Local).AddTicks(2851));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 25,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 988, DateTimeKind.Local).AddTicks(2851));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 26,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 988, DateTimeKind.Local).AddTicks(2851));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 27,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 988, DateTimeKind.Local).AddTicks(2851));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 28,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 988, DateTimeKind.Local).AddTicks(2851));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 29,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 988, DateTimeKind.Local).AddTicks(2851));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 30,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 988, DateTimeKind.Local).AddTicks(2851));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 31,
                column: "AddTime",
                value: new DateTime(2019, 5, 26, 21, 11, 7, 988, DateTimeKind.Local).AddTicks(2851));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataProtectionKeys");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "MovieDrafts",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldRowVersion: true,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 1,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 118, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 2,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 118, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 3,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 118, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 4,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 118, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 5,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 118, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 6,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 118, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 7,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 118, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 8,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 118, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 9,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 118, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 10,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 118, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 11,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 118, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 12,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 118, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 13,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 118, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 14,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 118, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 15,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 118, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 16,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 118, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 17,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 118, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 18,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 118, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 19,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 118, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 20,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 118, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 21,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 118, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 22,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 118, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 23,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 118, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 24,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 118, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 25,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 118, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 26,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 118, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 27,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 118, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieAreas",
                keyColumn: "Id",
                keyValue: 28,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 118, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 1,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 121, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 2,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 121, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 3,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 121, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 4,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 121, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 5,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 121, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 6,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 121, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 7,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 121, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 8,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 121, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 9,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 121, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 10,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 121, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 11,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 121, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 12,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 121, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 13,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 121, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 14,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 121, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 15,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 121, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 16,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 121, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 17,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 121, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 18,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 121, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 19,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 121, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 20,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 121, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 21,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 121, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 22,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 121, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 23,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 121, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 24,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 121, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 25,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 121, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 26,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 121, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 27,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 121, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 28,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 121, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 29,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 121, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 30,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 121, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValue: 31,
                column: "AddTime",
                value: new DateTime(2019, 4, 18, 21, 10, 52, 121, DateTimeKind.Local));
        }
    }
}
