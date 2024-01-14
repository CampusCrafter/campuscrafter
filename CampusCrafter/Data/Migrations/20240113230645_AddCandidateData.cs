using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampusCrafter.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCandidateData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Progresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Score = table.Column<decimal>(type: "TEXT", nullable: false),
                    CandidateUserId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Progresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Progresses_Candidates_CandidateUserId",
                        column: x => x.CandidateUserId,
                        principalTable: "Candidates",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "ScholarlyAchievements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    CandidateUserId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScholarlyAchievements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScholarlyAchievements_Candidates_CandidateUserId",
                        column: x => x.CandidateUserId,
                        principalTable: "Candidates",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Progresses_CandidateUserId",
                table: "Progresses",
                column: "CandidateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ScholarlyAchievements_CandidateUserId",
                table: "ScholarlyAchievements",
                column: "CandidateUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Progresses");

            migrationBuilder.DropTable(
                name: "ScholarlyAchievements");

            migrationBuilder.DropTable(
                name: "Candidates");
        }
    }
}
