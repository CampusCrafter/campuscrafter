using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampusCrafter.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAllowsStudying : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Majors_Majors_MajorId",
                table: "Majors");

            migrationBuilder.DropTable(
                name: "MajorMajor");

            migrationBuilder.RenameColumn(
                name: "Discriminator",
                table: "Majors",
                newName: "MajorType");

            migrationBuilder.AddColumn<int>(
                name: "MajorId1",
                table: "Majors",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Majors_MajorId1",
                table: "Majors",
                column: "MajorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Majors_Majors_MajorId",
                table: "Majors",
                column: "MajorId",
                principalTable: "Majors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Majors_Majors_MajorId1",
                table: "Majors",
                column: "MajorId1",
                principalTable: "Majors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Majors_Majors_MajorId",
                table: "Majors");

            migrationBuilder.DropForeignKey(
                name: "FK_Majors_Majors_MajorId1",
                table: "Majors");

            migrationBuilder.DropIndex(
                name: "IX_Majors_MajorId1",
                table: "Majors");

            migrationBuilder.DropColumn(
                name: "MajorId1",
                table: "Majors");

            migrationBuilder.RenameColumn(
                name: "MajorType",
                table: "Majors",
                newName: "Discriminator");

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
    }
}
