using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zeiterfassungssoftware.Migrations
{
    /// <inheritdoc />
    public partial class EntryShouldTimeIdOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entries_ShouldTimes_ShouldTimeId",
                table: "Entries");

            migrationBuilder.AlterColumn<Guid>(
                name: "ShouldTimeId",
                table: "Entries",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_ShouldTimes_ShouldTimeId",
                table: "Entries",
                column: "ShouldTimeId",
                principalTable: "ShouldTimes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entries_ShouldTimes_ShouldTimeId",
                table: "Entries");

            migrationBuilder.AlterColumn<Guid>(
                name: "ShouldTimeId",
                table: "Entries",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_ShouldTimes_ShouldTimeId",
                table: "Entries",
                column: "ShouldTimeId",
                principalTable: "ShouldTimes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
