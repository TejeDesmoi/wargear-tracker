using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WargearTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddArmyPublicFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Armies",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PublicSlug",
                table: "Armies",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Armies");

            migrationBuilder.DropColumn(
                name: "PublicSlug",
                table: "Armies");
        }
    }
}
