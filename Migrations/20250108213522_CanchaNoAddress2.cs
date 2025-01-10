using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proyectodotnet.Migrations
{
    /// <inheritdoc />
    public partial class CanchaNoAddress2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Cancha_CanchaId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_CanchaId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "CanchaId",
                table: "Addresses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CanchaId",
                table: "Addresses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CanchaId",
                table: "Addresses",
                column: "CanchaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Cancha_CanchaId",
                table: "Addresses",
                column: "CanchaId",
                principalTable: "Cancha",
                principalColumn: "Id");
        }
    }
}
