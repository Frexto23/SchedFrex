using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchedFrex.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Calendars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriorityEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriorityEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CalendarId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Calendars_CalendarId",
                        column: x => x.CalendarId,
                        principalTable: "Calendars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Problems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    PriorityId = table.Column<Guid>(type: "uuid", nullable: false),
                    PriorityEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Deadline = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Problems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Problems_PriorityEntity_PriorityEntityId",
                        column: x => x.PriorityEntityId,
                        principalTable: "PriorityEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Problems_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeIntervalEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Start = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    End = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ProblemEntityId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeIntervalEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeIntervalEntity_Problems_ProblemEntityId",
                        column: x => x.ProblemEntityId,
                        principalTable: "Problems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CalendarEntryEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    SlotId = table.Column<Guid>(type: "uuid", nullable: false),
                    CalendarId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarEntryEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalendarEntryEntity_Calendars_CalendarId",
                        column: x => x.CalendarId,
                        principalTable: "Calendars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CalendarEntryEntity_TimeIntervalEntity_SlotId",
                        column: x => x.SlotId,
                        principalTable: "TimeIntervalEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalendarEntryEntity_CalendarId",
                table: "CalendarEntryEntity",
                column: "CalendarId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarEntryEntity_SlotId",
                table: "CalendarEntryEntity",
                column: "SlotId");

            migrationBuilder.CreateIndex(
                name: "IX_Problems_PriorityEntityId",
                table: "Problems",
                column: "PriorityEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Problems_UserId",
                table: "Problems",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeIntervalEntity_ProblemEntityId",
                table: "TimeIntervalEntity",
                column: "ProblemEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CalendarId",
                table: "Users",
                column: "CalendarId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalendarEntryEntity");

            migrationBuilder.DropTable(
                name: "TimeIntervalEntity");

            migrationBuilder.DropTable(
                name: "Problems");

            migrationBuilder.DropTable(
                name: "PriorityEntity");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Calendars");
        }
    }
}
