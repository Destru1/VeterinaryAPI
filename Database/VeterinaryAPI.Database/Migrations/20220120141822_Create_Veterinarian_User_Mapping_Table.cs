using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinaryAPI.Database.Migrations
{
    public partial class Create_Veterinarian_User_Mapping_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VeterinariansUsersMapping",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VeterinarianId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VeterinariansUsersMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VeterinariansUsersMapping_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VeterinariansUsersMapping_Veterinarians_VeterinarianId",
                        column: x => x.VeterinarianId,
                        principalTable: "Veterinarians",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VeterinariansUsersMapping_UserId",
                table: "VeterinariansUsersMapping",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VeterinariansUsersMapping_VeterinarianId_UserId",
                table: "VeterinariansUsersMapping",
                columns: new[] { "VeterinarianId", "UserId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VeterinariansUsersMapping");
        }
    }
}
