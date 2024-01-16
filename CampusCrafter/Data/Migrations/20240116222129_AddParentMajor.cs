using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampusCrafter.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddParentMajor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentMajorId",
                table: "Majors",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Majors_ParentMajorId",
                table: "Majors",
                column: "ParentMajorId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Majors_Majors_ParentMajorId",
                table: "Majors",
                column: "ParentMajorId",
                principalTable: "Majors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Majors_Majors_ParentMajorId",
                table: "Majors");

            migrationBuilder.DropIndex(
                name: "IX_Majors_ParentMajorId",
                table: "Majors");

            migrationBuilder.DropColumn(
                name: "ParentMajorId",
                table: "Majors");
        }
    }
}
