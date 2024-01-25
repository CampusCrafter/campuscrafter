using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampusCrafter.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCandidateApplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateApplications_Candidates_ApplicantUserId",
                table: "CandidateApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateApplications_Majors_MajorId",
                table: "CandidateApplications");

            migrationBuilder.RenameColumn(
                name: "ApplicantUserId",
                table: "CandidateApplications",
                newName: "ApplicantId");

            migrationBuilder.RenameIndex(
                name: "IX_CandidateApplications_ApplicantUserId",
                table: "CandidateApplications",
                newName: "IX_CandidateApplications_ApplicantId");

            migrationBuilder.AlterColumn<int>(
                name: "MajorId",
                table: "CandidateApplications",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateApplications_Candidates_ApplicantId",
                table: "CandidateApplications",
                column: "ApplicantId",
                principalTable: "Candidates",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateApplications_Majors_MajorId",
                table: "CandidateApplications",
                column: "MajorId",
                principalTable: "Majors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateApplications_Candidates_ApplicantId",
                table: "CandidateApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateApplications_Majors_MajorId",
                table: "CandidateApplications");

            migrationBuilder.RenameColumn(
                name: "ApplicantId",
                table: "CandidateApplications",
                newName: "ApplicantUserId");

            migrationBuilder.RenameIndex(
                name: "IX_CandidateApplications_ApplicantId",
                table: "CandidateApplications",
                newName: "IX_CandidateApplications_ApplicantUserId");

            migrationBuilder.AlterColumn<int>(
                name: "MajorId",
                table: "CandidateApplications",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateApplications_Candidates_ApplicantUserId",
                table: "CandidateApplications",
                column: "ApplicantUserId",
                principalTable: "Candidates",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateApplications_Majors_MajorId",
                table: "CandidateApplications",
                column: "MajorId",
                principalTable: "Majors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
