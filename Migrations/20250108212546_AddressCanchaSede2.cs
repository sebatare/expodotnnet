using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proyectodotnet.Migrations
{
    /// <inheritdoc />
    public partial class AddressCanchaSede2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sede_Addresses_AddressId",
                table: "Sede");

            migrationBuilder.AddForeignKey(
                name: "FK_Sede_Addresses_AddressId",
                table: "Sede",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sede_Addresses_AddressId",
                table: "Sede");

            migrationBuilder.AddForeignKey(
                name: "FK_Sede_Addresses_AddressId",
                table: "Sede",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
