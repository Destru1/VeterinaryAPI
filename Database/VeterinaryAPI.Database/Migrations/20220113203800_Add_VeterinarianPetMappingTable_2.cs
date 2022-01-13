using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinaryAPI.Database.Migrations
{
    public partial class Add_VeterinarianPetMappingTable_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VeterinariansPetsMappings_Pets_PetId",
                table: "VeterinariansPetsMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_VeterinariansPetsMappings_Veterinarians_VeterinarianId",
                table: "VeterinariansPetsMappings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VeterinariansPetsMappings",
                table: "VeterinariansPetsMappings");

            migrationBuilder.RenameTable(
                name: "VeterinariansPetsMappings",
                newName: "VeterinariansPetsMapping");

            migrationBuilder.RenameIndex(
                name: "IX_VeterinariansPetsMappings_VeterinarianId_PetId",
                table: "VeterinariansPetsMapping",
                newName: "IX_VeterinariansPetsMapping_VeterinarianId_PetId");

            migrationBuilder.RenameIndex(
                name: "IX_VeterinariansPetsMappings_PetId",
                table: "VeterinariansPetsMapping",
                newName: "IX_VeterinariansPetsMapping_PetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VeterinariansPetsMapping",
                table: "VeterinariansPetsMapping",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VeterinariansPetsMapping_Pets_PetId",
                table: "VeterinariansPetsMapping",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VeterinariansPetsMapping_Veterinarians_VeterinarianId",
                table: "VeterinariansPetsMapping",
                column: "VeterinarianId",
                principalTable: "Veterinarians",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VeterinariansPetsMapping_Pets_PetId",
                table: "VeterinariansPetsMapping");

            migrationBuilder.DropForeignKey(
                name: "FK_VeterinariansPetsMapping_Veterinarians_VeterinarianId",
                table: "VeterinariansPetsMapping");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VeterinariansPetsMapping",
                table: "VeterinariansPetsMapping");

            migrationBuilder.RenameTable(
                name: "VeterinariansPetsMapping",
                newName: "VeterinariansPetsMappings");

            migrationBuilder.RenameIndex(
                name: "IX_VeterinariansPetsMapping_VeterinarianId_PetId",
                table: "VeterinariansPetsMappings",
                newName: "IX_VeterinariansPetsMappings_VeterinarianId_PetId");

            migrationBuilder.RenameIndex(
                name: "IX_VeterinariansPetsMapping_PetId",
                table: "VeterinariansPetsMappings",
                newName: "IX_VeterinariansPetsMappings_PetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VeterinariansPetsMappings",
                table: "VeterinariansPetsMappings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VeterinariansPetsMappings_Pets_PetId",
                table: "VeterinariansPetsMappings",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VeterinariansPetsMappings_Veterinarians_VeterinarianId",
                table: "VeterinariansPetsMappings",
                column: "VeterinarianId",
                principalTable: "Veterinarians",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
