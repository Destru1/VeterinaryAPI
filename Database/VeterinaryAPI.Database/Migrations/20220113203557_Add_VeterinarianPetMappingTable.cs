using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinaryAPI.Database.Migrations
{
    public partial class Add_VeterinarianPetMappingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VeterinariansPetsMappings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VeterinarianId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VeterinariansPetsMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VeterinariansPetsMappings_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VeterinariansPetsMappings_Veterinarians_VeterinarianId",
                        column: x => x.VeterinarianId,
                        principalTable: "Veterinarians",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VeterinariansPetsMappings_PetId",
                table: "VeterinariansPetsMappings",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_VeterinariansPetsMappings_VeterinarianId_PetId",
                table: "VeterinariansPetsMappings",
                columns: new[] { "VeterinarianId", "PetId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VeterinariansPetsMappings");
        }
    }
}
