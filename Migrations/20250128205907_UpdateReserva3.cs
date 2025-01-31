using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proyectodotnet.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReserva3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HoraTermino",
                table: "Reserva",
                newName: "FechaTermino");

            migrationBuilder.RenameColumn(
                name: "HoraInicio",
                table: "Reserva",
                newName: "FechaInicio");

            migrationBuilder.RenameColumn(
                name: "Fecha",
                table: "Reserva",
                newName: "FechaCreacion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FechaTermino",
                table: "Reserva",
                newName: "HoraTermino");

            migrationBuilder.RenameColumn(
                name: "FechaInicio",
                table: "Reserva",
                newName: "HoraInicio");

            migrationBuilder.RenameColumn(
                name: "FechaCreacion",
                table: "Reserva",
                newName: "Fecha");
        }
    }
}
