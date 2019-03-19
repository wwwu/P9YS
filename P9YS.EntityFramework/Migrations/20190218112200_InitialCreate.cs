using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace P9YS.EntityFramework.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carousels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Link = table.Column<string>(maxLength: 200, nullable: false),
                    ImgUrl = table.Column<string>(maxLength: 200, nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    State = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carousels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovieAreas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Area = table.Column<string>(maxLength: 50, nullable: false),
                    Other = table.Column<int>(nullable: false),
                    AddTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieAreas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovieGenres",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    AddTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieGenres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(maxLength: 200, nullable: false),
                    NickName = table.Column<string>(maxLength: 50, nullable: false),
                    Avatar = table.Column<string>(maxLength: 200, nullable: false),
                    Password = table.Column<string>(maxLength: 100, nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Role = table.Column<int>(nullable: false),
                    RegisterTime = table.Column<DateTime>(nullable: false),
                    LastLoginTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ShortName = table.Column<string>(maxLength: 50, nullable: false),
                    FullName = table.Column<string>(maxLength: 200, nullable: false),
                    OtherName = table.Column<string>(maxLength: 500, nullable: true),
                    Director = table.Column<string>(maxLength: 200, nullable: true),
                    Actor = table.Column<string>(maxLength: 500, nullable: true),
                    ReleaseDate = table.Column<DateTime>(nullable: false),
                    MovieTime = table.Column<int>(nullable: false),
                    ImgUrl = table.Column<string>(nullable: true),
                    Intro = table.Column<string>(nullable: true),
                    Score = table.Column<decimal>(nullable: false),
                    ScoreSum = table.Column<decimal>(nullable: false),
                    ScoreCount = table.Column<long>(nullable: false),
                    Hot = table.Column<int>(nullable: false),
                    HotSum = table.Column<long>(nullable: false),
                    SeriesId = table.Column<int>(nullable: false),
                    AddTime = table.Column<DateTime>(nullable: false),
                    UpdTime = table.Column<DateTime>(nullable: false),
                    MovieAreaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movies_MovieAreas_MovieAreaId",
                        column: x => x.MovieAreaId,
                        principalTable: "MovieAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ParentId = table.Column<int>(nullable: false),
                    Content = table.Column<string>(nullable: false),
                    AddTime = table.Column<DateTime>(nullable: false),
                    UpdTime = table.Column<DateTime>(nullable: false),
                    MovieId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieComments_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieComments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieOnlinePlays",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    WebSiteName = table.Column<string>(maxLength: 50, nullable: true),
                    Price = table.Column<string>(maxLength: 50, nullable: true),
                    Url = table.Column<string>(nullable: true),
                    MovieId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieOnlinePlays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieOnlinePlays_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieOrigins",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MovieId = table.Column<int>(nullable: false),
                    OriginType = table.Column<int>(nullable: false),
                    Score = table.Column<decimal>(nullable: false),
                    Url = table.Column<string>(maxLength: 200, nullable: true),
                    AddTime = table.Column<DateTime>(nullable: false),
                    UpdTime = table.Column<DateTime>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "MovieQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 200, nullable: false),
                    Content = table.Column<string>(nullable: false),
                    AnswerCount = table.Column<int>(nullable: false),
                    AddTime = table.Column<DateTime>(nullable: false),
                    UpdTime = table.Column<DateTime>(nullable: false),
                    MovieId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieQuestions_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieQuestions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieRecommends",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MovieId = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Sort = table.Column<int>(nullable: false),
                    AddTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieRecommends", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieRecommends_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieResources",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(nullable: false),
                    Size = table.Column<int>(nullable: false),
                    Resolution = table.Column<string>(maxLength: 50, nullable: false),
                    Subtitle = table.Column<string>(maxLength: 50, nullable: false),
                    Dub = table.Column<string>(maxLength: 50, nullable: false),
                    AddTime = table.Column<DateTime>(nullable: false),
                    UpdTime = table.Column<DateTime>(nullable: false),
                    MovieId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieResources_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieResources_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MovieId = table.Column<int>(nullable: false),
                    MovieGenreId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieType_MovieGenres_MovieGenreId",
                        column: x => x.MovieGenreId,
                        principalTable: "MovieGenres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieType_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RatingRecords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MovieId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Score = table.Column<decimal>(nullable: false),
                    Mark = table.Column<DateTime>(nullable: true),
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
                name: "MovieQuestionAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(nullable: false),
                    Support = table.Column<int>(nullable: false),
                    AddTime = table.Column<DateTime>(nullable: false),
                    UpdTime = table.Column<DateTime>(nullable: false),
                    MovieQuestionId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieQuestionAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieQuestionAnswers_MovieQuestions_MovieQuestionId",
                        column: x => x.MovieQuestionId,
                        principalTable: "MovieQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieQuestionAnswers_Users_UserId",
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
                    Mark = table.Column<DateTime>(nullable: true),
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

            migrationBuilder.InsertData(
                table: "MovieAreas",
                columns: new[] { "Id", "AddTime", "Area", "Other" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local), "中国", 0 },
                    { 28, new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local), "其它", 1 },
                    { 27, new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local), "比利时", 1 },
                    { 26, new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local), "奥地利", 1 },
                    { 25, new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local), "阿根廷", 1 },
                    { 24, new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local), "波兰", 1 },
                    { 23, new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local), "捷克", 1 },
                    { 22, new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local), "土耳其", 1 },
                    { 20, new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local), "新西兰", 1 },
                    { 19, new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local), "墨西哥", 1 },
                    { 18, new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local), "巴西", 1 },
                    { 17, new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local), "丹麦", 1 },
                    { 16, new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local), "瑞典", 1 },
                    { 15, new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local), "爱尔兰", 1 },
                    { 21, new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local), "新加坡", 1 },
                    { 13, new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local), "俄罗斯", 1 },
                    { 14, new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local), "伊朗", 1 },
                    { 2, new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local), "香港", 0 },
                    { 3, new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local), "台湾", 0 },
                    { 4, new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local), "美国", 0 },
                    { 6, new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local), "日本", 0 },
                    { 7, new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local), "韩国", 0 },
                    { 5, new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local), "英国", 0 },
                    { 9, new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local), "印度", 1 },
                    { 10, new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local), "泰国", 1 },
                    { 11, new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local), "加拿大", 1 },
                    { 12, new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local), "澳大利亚", 1 },
                    { 8, new DateTime(2019, 2, 18, 19, 21, 59, 511, DateTimeKind.Local), "法国", 0 }
                });

            migrationBuilder.InsertData(
                table: "MovieGenres",
                columns: new[] { "Id", "AddTime", "Name" },
                values: new object[,]
                {
                    { 18, new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local), "恐怖" },
                    { 22, new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local), "黑帮" },
                    { 19, new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local), "同性" },
                    { 20, new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local), "悬疑" },
                    { 21, new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local), "缉毒" },
                    { 23, new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local), "卧底" },
                    { 29, new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local), "监狱" },
                    { 25, new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local), "宇宙" },
                    { 26, new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local), "星战" },
                    { 27, new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local), "武侠" },
                    { 28, new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local), "纪录" },
                    { 17, new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local), "暴力" },
                    { 24, new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local), "枪战" },
                    { 16, new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local), "家庭" },
                    { 2, new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local), "动作" },
                    { 14, new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local), "奇幻" },
                    { 13, new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local), "童话" },
                    { 12, new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local), "动画" },
                    { 11, new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local), "音乐" },
                    { 10, new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local), "喜剧" },
                    { 9, new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local), "文艺" },
                    { 8, new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local), "烂片" },
                    { 7, new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local), "爱情" },
                    { 6, new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local), "青春" },
                    { 5, new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local), "剧情" },
                    { 4, new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local), "战争" },
                    { 3, new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local), "犯罪" },
                    { 30, new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local), "历史" },
                    { 1, new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local), "科幻" },
                    { 15, new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local), "史诗" },
                    { 31, new DateTime(2019, 2, 18, 19, 21, 59, 515, DateTimeKind.Local), "传记" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieComments_MovieId",
                table: "MovieComments",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieComments_UserId",
                table: "MovieComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieOnlinePlays_MovieId",
                table: "MovieOnlinePlays",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieOrigins_MovieId",
                table: "MovieOrigins",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieQuestionAnswers_MovieQuestionId",
                table: "MovieQuestionAnswers",
                column: "MovieQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieQuestionAnswers_UserId",
                table: "MovieQuestionAnswers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieQuestions_MovieId",
                table: "MovieQuestions",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieQuestions_UserId",
                table: "MovieQuestions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieRecommends_MovieId",
                table: "MovieRecommends",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieResources_MovieId",
                table: "MovieResources",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieResources_UserId",
                table: "MovieResources",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_MovieAreaId",
                table: "Movies",
                column: "MovieAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieType_MovieGenreId",
                table: "MovieType",
                column: "MovieGenreId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieType_MovieId",
                table: "MovieType",
                column: "MovieId");

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
                name: "Carousels");

            migrationBuilder.DropTable(
                name: "MovieComments");

            migrationBuilder.DropTable(
                name: "MovieOnlinePlays");

            migrationBuilder.DropTable(
                name: "MovieOrigins");

            migrationBuilder.DropTable(
                name: "MovieRecommends");

            migrationBuilder.DropTable(
                name: "MovieResources");

            migrationBuilder.DropTable(
                name: "MovieType");

            migrationBuilder.DropTable(
                name: "RatingRecords");

            migrationBuilder.DropTable(
                name: "SuportRecords");

            migrationBuilder.DropTable(
                name: "MovieGenres");

            migrationBuilder.DropTable(
                name: "MovieQuestionAnswers");

            migrationBuilder.DropTable(
                name: "MovieQuestions");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "MovieAreas");
        }
    }
}
