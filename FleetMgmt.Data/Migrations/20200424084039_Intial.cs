using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FleetMgmt.Data.Migrations
{
    public partial class Intial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    LicenseNumber = table.Column<string>(maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    Make = table.Column<string>(maxLength: 255, nullable: false),
                    Model = table.Column<string>(maxLength: 255, nullable: false),
                    Color = table.Column<string>(maxLength: 100, nullable: false),
                    PlateNumber = table.Column<string>(maxLength: 100, nullable: false),
                    EngineNumber = table.Column<string>(nullable: true),
                    ChassisNumber = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    KilometersDriven = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    Length = table.Column<long>(nullable: false),
                    VehicleId = table.Column<Guid>(nullable: false),
                    DiverId = table.Column<Guid>(nullable: false),
                    TripDate = table.Column<DateTime>(nullable: false),
                    VehicleId1 = table.Column<string>(nullable: true),
                    DriverId = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trips_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trips_Vehicles_VehicleId1",
                        column: x => x.VehicleId1,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Accidents",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    TripId = table.Column<Guid>(nullable: false),
                    AccidentTime = table.Column<DateTime>(nullable: false),
                    IsOwnFault = table.Column<bool>(nullable: false),
                    Fine = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CoveredByInsurance = table.Column<bool>(nullable: false),
                    TripId1 = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accidents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accidents_Trips_TripId1",
                        column: x => x.TripId1,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accidents_TripId1",
                table: "Accidents",
                column: "TripId1");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_DriverId",
                table: "Trips",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_VehicleId1",
                table: "Trips",
                column: "VehicleId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accidents");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
