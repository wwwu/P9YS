using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace P9YS.EntityFramework.Migrations
{
    public partial class Modify_Movie_Origin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DouBanScore",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "DouBanUrl",
                table: "Movies");

            migrationBuilder.CreateTable(
                name: "MovieOrigins",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MovieId = table.Column<int>(nullable: false),
                    OriginType = table.Column<int>(nullable: false),
                    Score = table.Column<decimal>(nullable: false),
                    Url = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieOrigins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieOrigins_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieOrigins_MovieId",
                table: "MovieOrigins",
                column: "MovieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieOrigins");

            migrationBuilder.AddColumn<decimal>(
                name: "DouBanScore",
                table: "Movies",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "DouBanUrl",
                table: "Movies",
                maxLength: 200,
                nullable: true);
        }
    }
}
