using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampusCrafter.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMajorsAndStudyPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudyPlanId",
                table: "Majors",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MajorId1",
                table: "Courses",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AcceptanceCriteria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MaxStudents = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcceptanceCriteria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScoreWeight",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProgressType = table.Column<int>(type: "INTEGER", nullable: false),
                    Weight = table.Column<decimal>(type: "TEXT", nullable: false),
                    AcceptanceCriteriaId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoreWeight", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScoreWeight_AcceptanceCriteria_AcceptanceCriteriaId",
                        column: x => x.AcceptanceCriteriaId,
                        principalTable: "AcceptanceCriteria",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudyPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StageOfStudy = table.Column<int>(type: "INTEGER", nullable: false),
                    StudyType = table.Column<int>(type: "INTEGER", nullable: false),
                    EducationProfile = table.Column<int>(type: "INTEGER", nullable: false),
                    Language = table.Column<string>(type: "TEXT", nullable: false),
                    AcceptanceCriteriaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyPlans_AcceptanceCriteria_AcceptanceCriteriaId",
                        column: x => x.AcceptanceCriteriaId,
                        principalTable: "AcceptanceCriteria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Majors_StudyPlanId",
                table: "Majors",
                column: "StudyPlanId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_MajorId1",
                table: "Courses",
                column: "MajorId1");

            migrationBuilder.CreateIndex(
                name: "IX_ScoreWeight_AcceptanceCriteriaId",
                table: "ScoreWeight",
                column: "AcceptanceCriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyPlans_AcceptanceCriteriaId",
                table: "StudyPlans",
                column: "AcceptanceCriteriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Majors_MajorId1",
                table: "Courses",
                column: "MajorId1",
                principalTable: "Majors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Majors_StudyPlans_StudyPlanId",
                table: "Majors",
                column: "StudyPlanId",
                principalTable: "StudyPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Majors_MajorId1",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Majors_StudyPlans_StudyPlanId",
                table: "Majors");

            migrationBuilder.DropTable(
                name: "ScoreWeight");

            migrationBuilder.DropTable(
                name: "StudyPlans");

            migrationBuilder.DropTable(
                name: "AcceptanceCriteria");

            migrationBuilder.DropIndex(
                name: "IX_Majors_StudyPlanId",
                table: "Majors");

            migrationBuilder.DropIndex(
                name: "IX_Courses_MajorId1",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "StudyPlanId",
                table: "Majors");

            migrationBuilder.DropColumn(
                name: "MajorId1",
                table: "Courses");
        }
    }
}
