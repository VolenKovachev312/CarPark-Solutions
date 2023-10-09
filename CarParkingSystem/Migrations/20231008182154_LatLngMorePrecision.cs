using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarParkingSystem.Migrations
{
    /// <inheritdoc />
    public partial class LatLngMorePrecision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "ParkingLots",
                type: "decimal(28,24)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,9)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "ParkingLots",
                type: "decimal(28,24)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,9)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "ParkingLots",
                type: "decimal(18,9)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(28,24)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "ParkingLots",
                type: "decimal(18,9)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(28,24)");
        }
    }
}
