using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarParkingSystem.Migrations
{
    /// <inheritdoc />
    public partial class parkingLotAddedWorkingHours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "ClosingHour",
                table: "ParkingLots",
                type: "TIME(7)",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<bool>(
                name: "IsNonStop",
                table: "ParkingLots",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "OpeningHour",
                table: "ParkingLots",
                type: "TIME(7)",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClosingHour",
                table: "ParkingLots");

            migrationBuilder.DropColumn(
                name: "IsNonStop",
                table: "ParkingLots");

            migrationBuilder.DropColumn(
                name: "OpeningHour",
                table: "ParkingLots");
        }
    }
}
