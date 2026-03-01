using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SchedFrex.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixValueTypeLogic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Problems_PriorityEntity_PriorityEntityId",
                table: "Problems");

            migrationBuilder.DropTable(
                name: "CalendarEntryEntity");

            migrationBuilder.DropTable(
                name: "PriorityEntity");

            migrationBuilder.DropTable(
                name: "TimeIntervalEntity");

            migrationBuilder.DropIndex(
                name: "IX_Problems_PriorityEntityId",
                table: "Problems");

            migrationBuilder.DropColumn(
                name: "PriorityEntityId",
                table: "Problems");

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "Problems",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EntryEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Start = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    End = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CalendarId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntryEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntryEntity_Calendars_CalendarId",
                        column: x => x.CalendarId,
                        principalTable: "Calendars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProblemIntervals",
                columns: table => new
                {
                    ProblemId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Start = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    End = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProblemIntervals", x => new { x.ProblemId, x.Id });
                    table.ForeignKey(
                        name: "FK_ProblemIntervals_Problems_ProblemId",
                        column: x => x.ProblemId,
                        principalTable: "Problems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntryEntity_CalendarId",
                table: "EntryEntity",
                column: "CalendarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntryEntity");

            migrationBuilder.DropTable(
                name: "ProblemIntervals");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Problems");

            migrationBuilder.AddColumn<Guid>(
                name: "PriorityEntityId",
                table: "Problems",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
                name: "TimeIntervalEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    End = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ProblemEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    Start = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                    CalendarId = table.Column<Guid>(type: "uuid", nullable: false),
                    SlotId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
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
                name: "IX_Problems_PriorityEntityId",
                table: "Problems",
                column: "PriorityEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarEntryEntity_CalendarId",
                table: "CalendarEntryEntity",
                column: "CalendarId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarEntryEntity_SlotId",
                table: "CalendarEntryEntity",
                column: "SlotId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeIntervalEntity_ProblemEntityId",
                table: "TimeIntervalEntity",
                column: "ProblemEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Problems_PriorityEntity_PriorityEntityId",
                table: "Problems",
                column: "PriorityEntityId",
                principalTable: "PriorityEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
