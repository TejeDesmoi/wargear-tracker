using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WargearTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPublicFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Miniatures_ArmyId",
                table: "Miniatures",
                column: "ArmyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Miniatures_Armies_ArmyId",
                table: "Miniatures",
                column: "ArmyId",
                principalTable: "Armies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Miniatures_Armies_ArmyId",
                table: "Miniatures");

            migrationBuilder.DropIndex(
                name: "IX_Miniatures_ArmyId",
                table: "Miniatures");
        }
    }
}
