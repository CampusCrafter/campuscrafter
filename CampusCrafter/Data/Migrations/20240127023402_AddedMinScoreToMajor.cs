using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampusCrafter.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedMinScoreToMajor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateApplications_Candidates_ApplicantId",
                table: "CandidateApplications");

            migrationBuilder.AddColumn<float>(
                name: "MinScore",
                table: "Majors",
                type: "REAL",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApplicantId",
                table: "CandidateApplications",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateApplications_Candidates_ApplicantId",
                table: "CandidateApplications",
                column: "ApplicantId",
                principalTable: "Candidates",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateApplications_Candidates_ApplicantId",
                table: "CandidateApplications");

            migrationBuilder.DropColumn(
                name: "MinScore",
                table: "Majors");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicantId",
                table: "CandidateApplications",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateApplications_Candidates_ApplicantId",
                table: "CandidateApplications",
                column: "ApplicantId",
                principalTable: "Candidates",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
