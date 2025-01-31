using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proyectodotnet.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReserva4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FechaTermino",
                table: "Reserva",
                newName: "HoraTermino");

            migrationBuilder.RenameColumn(
                name: "FechaInicio",
                table: "Reserva",
                newName: "HoraInicio");

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha",
                table: "Reserva",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fecha",
                table: "Reserva");

            migrationBuilder.RenameColumn(
                name: "HoraTermino",
                table: "Reserva",
                newName: "FechaTermino");

            migrationBuilder.RenameColumn(
                name: "HoraInicio",
                table: "Reserva",
                newName: "FechaInicio");
        }
    }
}
