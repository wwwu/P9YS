using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace P9YS.EntityFramework.Migrations
{
    public partial class Create_RatingRecords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RatingRecords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MovieId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Score = table.Column<decimal>(nullable: false),
                    AddTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RatingRecords_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RatingRecords_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SuportRecords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MovieQuestionAnswerId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    AddTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuportRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuportRecords_MovieQuestionAnswers_MovieQuestionAnswerId",
                        column: x => x.MovieQuestionAnswerId,
                        principalTable: "MovieQuestionAnswers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SuportRecords_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RatingRecords_MovieId",
                table: "RatingRecords",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingRecords_UserId",
                table: "RatingRecords",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SuportRecords_MovieQuestionAnswerId",
                table: "SuportRecords",
                column: "MovieQuestionAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_SuportRecords_UserId",
                table: "SuportRecords",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RatingRecords");

            migrationBuilder.DropTable(
                name: "SuportRecords");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");
        }
    }
}
