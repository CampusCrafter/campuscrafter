using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampusCrafter.Data.Migrations
{
    /// <inheritdoc />
    public partial class SpecializationMajorForeignKey3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Majors_Majors_SpecializationId",
                table: "Majors");

            migrationBuilder.DropForeignKey(
                name: "FK_Majors_Majors_SpecializationId1",
                table: "Majors");

            migrationBuilder.DropIndex(
                name: "IX_Majors_SpecializationId",
                table: "Majors");

            migrationBuilder.DropIndex(
                name: "IX_Majors_SpecializationId1",
                table: "Majors");

            migrationBuilder.DropColumn(
                name: "SpecializationId",
                table: "Majors");

            migrationBuilder.DropColumn(
                name: "SpecializationId1",
                table: "Majors");

            migrationBuilder.CreateTable(
                name: "MajorMajor",
                columns: table => new
                {
                    AllowsStudyingId = table.Column<int>(type: "INTEGER", nullable: false),
                    PrerequisitesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MajorMajor", x => new { x.AllowsStudyingId, x.PrerequisitesId });
                    table.ForeignKey(
                        name: "FK_MajorMajor_Majors_AllowsStudyingId",
                        column: x => x.AllowsStudyingId,
                        principalTable: "Majors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MajorMajor_Majors_PrerequisitesId",
                        column: x => x.PrerequisitesId,
                        principalTable: "Majors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Majors_MajorId",
                table: "Majors",
                column: "MajorId");

            migrationBuilder.CreateIndex(
                name: "IX_MajorMajor_PrerequisitesId",
                table: "MajorMajor",
                column: "PrerequisitesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Majors_Majors_MajorId",
                table: "Majors",
                column: "MajorId",
                principalTable: "Majors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Majors_Majors_MajorId",
                table: "Majors");

            migrationBuilder.DropTable(
                name: "MajorMajor");

            migrationBuilder.DropIndex(
                name: "IX_Majors_MajorId",
                table: "Majors");

            migrationBuilder.AddColumn<int>(
                name: "SpecializationId",
                table: "Majors",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SpecializationId1",
                table: "Majors",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Majors_SpecializationId",
                table: "Majors",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_Majors_SpecializationId1",
                table: "Majors",
                column: "SpecializationId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Majors_Majors_SpecializationId",
                table: "Majors",
                column: "SpecializationId",
                principalTable: "Majors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Majors_Majors_SpecializationId1",
                table: "Majors",
                column: "SpecializationId1",
                principalTable: "Majors",
                principalColumn: "Id");
        }
    }
}
