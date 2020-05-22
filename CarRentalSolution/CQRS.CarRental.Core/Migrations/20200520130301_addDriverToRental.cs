using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CQRS.CarRental.Core.Migrations
{
    public partial class addDriverToRental : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DriverId",
                table: "Rentals",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_DriverId",
                table: "Rentals",
                column: "DriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Drivers_DriverId",
                table: "Rentals",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "DriverId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Drivers_DriverId",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_DriverId",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "Rentals");
        }
    }
}
