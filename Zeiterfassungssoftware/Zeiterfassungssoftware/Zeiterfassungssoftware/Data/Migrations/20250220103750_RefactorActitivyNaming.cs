using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zeiterfassungssoftware.Data.Migrations
{
    /// <inheritdoc />
    public partial class RefactorActitivyNaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "ActivityTitle",
                newName: "Title");
            migrationBuilder.RenameTable("ActivityTitle", newName: "Activity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable("Activity", newName: "ActivityTitle");
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "ActivityTitle",
                newName: "Value");
        }
    }
}
