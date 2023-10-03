using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarParkingSystem.Migrations
{
    /// <inheritdoc />
    public partial class userRemoveCatMakeAndModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarMake",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CarModel",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "CarNumber",
                table: "AspNetUsers",
                newName: "LicensePlateNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LicensePlateNumber",
                table: "AspNetUsers",
                newName: "CarNumber");

            migrationBuilder.AddColumn<string>(
                name: "CarMake",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CarModel",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
