﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proyectodotnet.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReserva : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reserva_AspNetUsers_UsuarioId",
                table: "Reserva");

            migrationBuilder.DropForeignKey(
                name: "FK_Reserva_Cancha_CanchaId",
                table: "Reserva");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Reserva",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "CanchaId",
                table: "Reserva",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Reserva_AspNetUsers_UsuarioId",
                table: "Reserva",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reserva_Cancha_CanchaId",
                table: "Reserva",
                column: "CanchaId",
                principalTable: "Cancha",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reserva_AspNetUsers_UsuarioId",
                table: "Reserva");

            migrationBuilder.DropForeignKey(
                name: "FK_Reserva_Cancha_CanchaId",
                table: "Reserva");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Reserva",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CanchaId",
                table: "Reserva",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reserva_AspNetUsers_UsuarioId",
                table: "Reserva",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reserva_Cancha_CanchaId",
                table: "Reserva",
                column: "CanchaId",
                principalTable: "Cancha",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
