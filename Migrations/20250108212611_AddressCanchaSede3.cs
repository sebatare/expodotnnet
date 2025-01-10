using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proyectodotnet.Migrations
{
    /// <inheritdoc />
    public partial class AddressCanchaSede3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cancha_Sede_SedeId",
                table: "Cancha");

            migrationBuilder.AddForeignKey(
                name: "FK_Cancha_Sede_SedeId",
                table: "Cancha",
                column: "SedeId",
                principalTable: "Sede",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cancha_Sede_SedeId",
                table: "Cancha");

            migrationBuilder.AddForeignKey(
                name: "FK_Cancha_Sede_SedeId",
                table: "Cancha",
                column: "SedeId",
                principalTable: "Sede",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
