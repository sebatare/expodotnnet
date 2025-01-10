using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proyectodotnet.Migrations
{
    /// <inheritdoc />
    public partial class AllowDeleteSedeWithoutDeleteCancha : Migration
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
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
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
                principalColumn: "Id");
        }
    }
}
