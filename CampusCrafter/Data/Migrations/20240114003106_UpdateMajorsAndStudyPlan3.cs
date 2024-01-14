using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampusCrafter.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMajorsAndStudyPlan3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Courses",
                type: "TEXT",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MajorId1",
                table: "Courses",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_MajorId1",
                table: "Courses",
                column: "MajorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Majors_MajorId1",
                table: "Courses",
                column: "MajorId1",
                principalTable: "Majors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Majors_MajorId1",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_MajorId1",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "MajorId1",
                table: "Courses");
        }
    }
}
