using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchedFrex.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserAndCalendarsRelationshipToOneToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Calendars_CalendarId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CalendarId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CalendarId",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Calendars",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Calendars_UserId",
                table: "Calendars",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Calendars_Users_UserId",
                table: "Calendars",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calendars_Users_UserId",
                table: "Calendars");

            migrationBuilder.DropIndex(
                name: "IX_Calendars_UserId",
                table: "Calendars");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Calendars");

            migrationBuilder.AddColumn<Guid>(
                name: "CalendarId",
                table: "Users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Users_CalendarId",
                table: "Users",
                column: "CalendarId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Calendars_CalendarId",
                table: "Users",
                column: "CalendarId",
                principalTable: "Calendars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
