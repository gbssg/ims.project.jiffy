using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zeiterfassungssoftware.Data.Migrations
{
    /// <inheritdoc />
    public partial class ApplyingConventions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "User_Id",
                table: "Activity",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "User_Id",
                table: "ActivityDescription",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "User_Id",
                table: "Entry",
                newName: "UserId");

            migrationBuilder.RenameTable(
                name: "Class",
                newName: "Classes");

            migrationBuilder.RenameTable(
                name: "Activity",
                newName: "ActivityTitles");

            migrationBuilder.RenameTable(
                name: "Entry",
                newName: "Entries");

            migrationBuilder.RenameTable(
                name: "ActivityDescription",
                newName: "ActivityDescriptions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ActivityTitles",
                newName: "User_Id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ActivityDescriptions",
                newName: "User_Id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Entries",
                newName: "User_Id");

            migrationBuilder.RenameTable(
                name: "Classes",
                newName: "Class");

            migrationBuilder.RenameTable(
                name: "ActivityTitles",
                newName: "Activity");

            migrationBuilder.RenameTable(
                name: "Entries",
                newName: "Entry");

            migrationBuilder.RenameTable(
                name: "ActivityDescriptions",
                newName: "ActivityDescription");
        }

    }
}
