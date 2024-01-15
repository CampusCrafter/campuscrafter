using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampusCrafter.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddScoreWeightTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScoreWeight_AcceptanceCriteria_AcceptanceCriteriaId",
                table: "ScoreWeight");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScoreWeight",
                table: "ScoreWeight");

            migrationBuilder.RenameTable(
                name: "ScoreWeight",
                newName: "ScoreWeights");

            migrationBuilder.RenameIndex(
                name: "IX_ScoreWeight_AcceptanceCriteriaId",
                table: "ScoreWeights",
                newName: "IX_ScoreWeights_AcceptanceCriteriaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScoreWeights",
                table: "ScoreWeights",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScoreWeights_AcceptanceCriteria_AcceptanceCriteriaId",
                table: "ScoreWeights",
                column: "AcceptanceCriteriaId",
                principalTable: "AcceptanceCriteria",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScoreWeights_AcceptanceCriteria_AcceptanceCriteriaId",
                table: "ScoreWeights");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScoreWeights",
                table: "ScoreWeights");

            migrationBuilder.RenameTable(
                name: "ScoreWeights",
                newName: "ScoreWeight");

            migrationBuilder.RenameIndex(
                name: "IX_ScoreWeights_AcceptanceCriteriaId",
                table: "ScoreWeight",
                newName: "IX_ScoreWeight_AcceptanceCriteriaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScoreWeight",
                table: "ScoreWeight",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScoreWeight_AcceptanceCriteria_AcceptanceCriteriaId",
                table: "ScoreWeight",
                column: "AcceptanceCriteriaId",
                principalTable: "AcceptanceCriteria",
                principalColumn: "Id");
        }
    }
}
