using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CQRS.CarRental.Core.Migrations
{
    public partial class AddReadModelsToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarViewModels",
                columns: table => new
                {
                    CarId = table.Column<Guid>(nullable: false),
                    RegistrationNumber = table.Column<string>(maxLength: 7, nullable: false),
                    CurrentDistance = table.Column<double>(nullable: false),
                    TotalDistance = table.Column<double>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    XPosition = table.Column<double>(nullable: false),
                    YPosition = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarViewModels", x => x.CarId);
                });

            migrationBuilder.CreateTable(
                name: "DriverViewModels",
                columns: table => new
                {
                    DriverId = table.Column<Guid>(nullable: false),
                    LicenceNumber = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverViewModels", x => x.DriverId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarViewModels");

            migrationBuilder.DropTable(
                name: "DriverViewModels");
        }
    }
}
