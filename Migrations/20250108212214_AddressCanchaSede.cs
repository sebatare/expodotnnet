using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proyectodotnet.Migrations
{
    /// <inheritdoc />
    public partial class AddressCanchaSede : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SedeId",
                table: "Cancha",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Sede",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sede", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sede_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cancha_SedeId",
                table: "Cancha",
                column: "SedeId");

            migrationBuilder.CreateIndex(
                name: "IX_Sede_AddressId",
                table: "Sede",
                column: "AddressId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cancha_Sede_SedeId",
                table: "Cancha",
                column: "SedeId",
                principalTable: "Sede",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cancha_Sede_SedeId",
                table: "Cancha");

            migrationBuilder.DropTable(
                name: "Sede");

            migrationBuilder.DropIndex(
                name: "IX_Cancha_SedeId",
                table: "Cancha");

            migrationBuilder.DropColumn(
                name: "SedeId",
                table: "Cancha");
        }
    }
}
