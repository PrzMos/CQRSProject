using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CQRS.CarRental.Core.Migrations
{
    public partial class CreateInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    CarId = table.Column<Guid>(nullable: false),
                    RegistrationNumber = table.Column<string>(maxLength: 7, nullable: false),
                    XPosition = table.Column<double>(nullable: false),
                    YPosition = table.Column<double>(nullable: false),
                    CurrentDistance = table.Column<double>(nullable: false),
                    TotalDistance = table.Column<double>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.CarId);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    DriverId = table.Column<Guid>(nullable: false),
                    LicenceNumber = table.Column<string>(maxLength: 14, nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.DriverId);
                });

            migrationBuilder.CreateTable(
                name: "RentalReadModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RentalId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Finished = table.Column<DateTime>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    DriverId = table.Column<Guid>(nullable: false),
                    Driver = table.Column<string>(nullable: true),
                    CarId = table.Column<Guid>(nullable: false),
                    RegistrationNumber = table.Column<string>(nullable: true),
                    StartXPosition = table.Column<double>(nullable: false),
                    StartYPosition = table.Column<double>(nullable: false),
                    StopXPosition = table.Column<double>(nullable: false),
                    StopYPosition = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalReadModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rentals",
                columns: table => new
                {
                    RentalId = table.Column<Guid>(nullable: false),
                    Started = table.Column<DateTime>(nullable: false),
                    Finished = table.Column<DateTime>(nullable: false),
                    CarId = table.Column<Guid>(nullable: false),
                    Total = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentals", x => x.RentalId);
                    table.ForeignKey(
                        name: "FK_Rentals_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_CarId",
                table: "Rentals",
                column: "CarId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "RentalReadModels");

            migrationBuilder.DropTable(
                name: "Rentals");

            migrationBuilder.DropTable(
                name: "Cars");
        }
    }
}
