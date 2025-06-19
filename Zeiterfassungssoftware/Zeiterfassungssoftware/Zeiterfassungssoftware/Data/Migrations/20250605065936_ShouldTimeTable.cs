using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zeiterfassungssoftware.Data.Migrations
{
    /// <inheritdoc />
    public partial class ShouldTimeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShouldTimeFriday",
                table: "Class");

            migrationBuilder.DropColumn(
                name: "ShouldTimeMonday",
                table: "Class");

            migrationBuilder.DropColumn(
                name: "ShouldTimeThursday",
                table: "Class");

            migrationBuilder.DropColumn(
                name: "ShouldTimeTuesday",
                table: "Class");

            migrationBuilder.DropColumn(
                name: "ShouldTimeWednesday",
                table: "Class");

            migrationBuilder.CreateTable(
                name: "ShouldTimes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    Should = table.Column<TimeSpan>(type: "time", nullable: false),
                    ClassId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShouldTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShouldTimes_Class_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ClassId",
                table: "AspNetUsers",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ShouldTimes_ClassId",
                table: "ShouldTimes",
                column: "ClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Class_ClassId",
                table: "AspNetUsers",
                column: "ClassId",
                principalTable: "Class",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Class_ClassId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ShouldTimes");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ClassId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ShouldTimeFriday",
                table: "Class",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ShouldTimeMonday",
                table: "Class",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ShouldTimeThursday",
                table: "Class",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ShouldTimeTuesday",
                table: "Class",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ShouldTimeWednesday",
                table: "Class",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
