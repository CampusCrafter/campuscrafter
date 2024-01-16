using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampusCrafter.Data.Migrations
{
    /// <inheritdoc />
    public partial class SpecializationSufferingIsOverHopefully : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Majors_Majors_MajorId",
                table: "Majors");

            migrationBuilder.DropForeignKey(
                name: "FK_Majors_Majors_MajorId1",
                table: "Majors");

            migrationBuilder.DropIndex(
                name: "IX_Majors_MajorId",
                table: "Majors");

            migrationBuilder.DropIndex(
                name: "IX_Majors_MajorId1",
                table: "Majors");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Majors");

            migrationBuilder.DropColumn(
                name: "MajorId",
                table: "Majors");

            migrationBuilder.DropColumn(
                name: "MajorId1",
                table: "Majors");

            migrationBuilder.AddColumn<int>(
                name: "MajorType",
                table: "Majors",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MajorMajor",
                columns: table => new
                {
                    PrerequisitesId = table.Column<int>(type: "INTEGER", nullable: false),
                    SpecializationsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MajorMajor", x => new { x.PrerequisitesId, x.SpecializationsId });
                    table.ForeignKey(
                        name: "FK_MajorMajor_Majors_PrerequisitesId",
                        column: x => x.PrerequisitesId,
                        principalTable: "Majors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MajorMajor_Majors_SpecializationsId",
                        column: x => x.SpecializationsId,
                        principalTable: "Majors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MajorMajor_SpecializationsId",
                table: "MajorMajor",
                column: "SpecializationsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MajorMajor");

            migrationBuilder.DropColumn(
                name: "MajorType",
                table: "Majors");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Majors",
                type: "TEXT",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MajorId",
                table: "Majors",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MajorId1",
                table: "Majors",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Majors_MajorId",
                table: "Majors",
                column: "MajorId");

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
    }
}
