using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WargearTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameStatusToPaintStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Miniatures",
                newName: "PaintStatus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaintStatus",
                table: "Miniatures",
                newName: "Status");
        }
    }
}
