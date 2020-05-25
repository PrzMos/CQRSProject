using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CQRS.CarRental.Core.Migrations
{
    public partial class ChangeKeyInRentalReadModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RentalReadModels",
                table: "RentalReadModels");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RentalReadModels");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RentalReadModels",
                table: "RentalReadModels",
                column: "RentalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RentalReadModels",
                table: "RentalReadModels");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "RentalReadModels",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_RentalReadModels",
                table: "RentalReadModels",
                column: "Id");
        }
    }
}
