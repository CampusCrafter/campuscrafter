using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampusCrafter.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExplicitSpecializationKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Majors_MajorId1",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_MajorId1",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "MajorId1",
                table: "Courses");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Courses",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Majors_Id",
                table: "Courses",
                column: "Id",
                principalTable: "Majors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Majors_Id",
                table: "Courses");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Courses",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

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
    }
}
