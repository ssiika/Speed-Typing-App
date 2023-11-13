using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Typing_App_API.Migrations
{
    /// <inheritdoc />
    public partial class AddRecordCreatedTimestamp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Length",
                table: "Records");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Records",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Records");

            migrationBuilder.AddColumn<int>(
                name: "Length",
                table: "Records",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
