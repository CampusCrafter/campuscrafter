using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampusCrafter.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReworkStudyPlans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Majors_StudyPlans_StudyPlanId",
                table: "Majors");

            migrationBuilder.DropIndex(
                name: "IX_Majors_StudyPlanId",
                table: "Majors");

            migrationBuilder.DropColumn(
                name: "StudyPlanId",
                table: "Majors");

            migrationBuilder.AddColumn<int>(
                name: "MajorId",
                table: "StudyPlans",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StudyType",
                table: "CandidateApplications",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StudyPlans_MajorId",
                table: "StudyPlans",
                column: "MajorId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudyPlans_Majors_MajorId",
                table: "StudyPlans",
                column: "MajorId",
                principalTable: "Majors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudyPlans_Majors_MajorId",
                table: "StudyPlans");

            migrationBuilder.DropIndex(
                name: "IX_StudyPlans_MajorId",
                table: "StudyPlans");

            migrationBuilder.DropColumn(
                name: "MajorId",
                table: "StudyPlans");

            migrationBuilder.DropColumn(
                name: "StudyType",
                table: "CandidateApplications");

            migrationBuilder.AddColumn<int>(
                name: "StudyPlanId",
                table: "Majors",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Majors_StudyPlanId",
                table: "Majors",
                column: "StudyPlanId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Majors_StudyPlans_StudyPlanId",
                table: "Majors",
                column: "StudyPlanId",
                principalTable: "StudyPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
