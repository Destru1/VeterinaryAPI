using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace VeterinaryAPI.Database.Migrations
{
    public partial class Add_AppointmentDate_To_VetPetMappingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AppointmentDate",
                table: "VeterinariansPetsMapping",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "Age",
                table: "Pets",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentDate",
                table: "VeterinariansPetsMapping");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Pets");
        }
    }
}
