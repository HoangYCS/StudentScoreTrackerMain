using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataApp.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EducationDatas",
                columns: table => new
                {
                    SBD = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaHS = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DiemToan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiemVan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiemAnh = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ThongTinDiem = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CurrentYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationDatas", x => x.SBD);
                });

            migrationBuilder.CreateTable(
                name: "DualDegreeScores",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SubjectName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Score = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EducationDataKey = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DualDegreeScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DualDegreeScores_EducationDatas_EducationDataKey",
                        column: x => x.EducationDataKey,
                        principalTable: "EducationDatas",
                        principalColumn: "SBD",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpecializedScores",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SubjectName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Score = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EducationDataKey = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecializedScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecializedScores_EducationDatas_EducationDataKey",
                        column: x => x.EducationDataKey,
                        principalTable: "EducationDatas",
                        principalColumn: "SBD",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DualDegreeScores_EducationDataKey",
                table: "DualDegreeScores",
                column: "EducationDataKey");

            migrationBuilder.CreateIndex(
                name: "IX_SpecializedScores_EducationDataKey",
                table: "SpecializedScores",
                column: "EducationDataKey");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DualDegreeScores");

            migrationBuilder.DropTable(
                name: "SpecializedScores");

            migrationBuilder.DropTable(
                name: "EducationDatas");
        }
    }
}
