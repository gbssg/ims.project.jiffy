using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zeiterfassungssoftware.Migrations
{
    /// <inheritdoc />
    public partial class ApplyingShouldTimeVersions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShouldTime",
                table: "Entries");

            migrationBuilder.AddColumn<DateTime>(
                name: "ValidUntil",
                table: "ShouldTimes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "ShouldTimeId",
                table: "Entries",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Entries_ShouldTimeId",
                table: "Entries",
                column: "ShouldTimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_ShouldTimes_ShouldTimeId",
                table: "Entries",
                column: "ShouldTimeId",
                principalTable: "ShouldTimes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entries_ShouldTimes_ShouldTimeId",
                table: "Entries");

            migrationBuilder.DropIndex(
                name: "IX_Entries_ShouldTimeId",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "ValidUntil",
                table: "ShouldTimes");

            migrationBuilder.DropColumn(
                name: "ShouldTimeId",
                table: "Entries");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ShouldTime",
                table: "Entries",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
