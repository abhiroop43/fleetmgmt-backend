using Microsoft.EntityFrameworkCore.Migrations;

namespace FleetMgmt.Data.Migrations
{
    public partial class DrivenKilometersAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "KilometersDriven",
                table: "Vehicles",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Length",
                table: "Trips",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KilometersDriven",
                table: "Vehicles");

            migrationBuilder.AlterColumn<int>(
                name: "Length",
                table: "Trips",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
