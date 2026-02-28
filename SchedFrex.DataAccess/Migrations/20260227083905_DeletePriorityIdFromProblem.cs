using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchedFrex.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class DeletePriorityIdFromProblem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriorityId",
                table: "Problems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PriorityId",
                table: "Problems",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
