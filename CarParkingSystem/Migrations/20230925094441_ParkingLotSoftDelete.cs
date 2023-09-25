using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarParkingSystem.Migrations
{
    /// <inheritdoc />
    public partial class ParkingLotSoftDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "ParkingLots",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "ParkingLots");
        }
    }
}
