using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proyectodotnet.Migrations
{
    /// <inheritdoc />
    public partial class EquiposUsuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioEquipo_AspNetUsers_UsuarioId",
                table: "UsuarioEquipo");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioEquipo_Equipo_EquipoId",
                table: "UsuarioEquipo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioEquipo",
                table: "UsuarioEquipo");

            migrationBuilder.DropIndex(
                name: "IX_UsuarioEquipo_EquipoId",
                table: "UsuarioEquipo");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UsuarioEquipo");

            migrationBuilder.DropColumn(
                name: "EquipoId",
                table: "UsuarioEquipo");

            migrationBuilder.DropColumn(
                name: "Posicion",
                table: "UsuarioEquipo");

            migrationBuilder.DropColumn(
                name: "Puntuacion",
                table: "UsuarioEquipo");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "UsuarioEquipo",
                newName: "UsuariosId");

            migrationBuilder.RenameColumn(
                name: "Goles",
                table: "UsuarioEquipo",
                newName: "EquiposId");

            migrationBuilder.RenameIndex(
                name: "IX_UsuarioEquipo_UsuarioId",
                table: "UsuarioEquipo",
                newName: "IX_UsuarioEquipo_UsuariosId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioEquipo",
                table: "UsuarioEquipo",
                columns: new[] { "EquiposId", "UsuariosId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioEquipo_AspNetUsers_UsuariosId",
                table: "UsuarioEquipo",
                column: "UsuariosId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioEquipo_Equipo_EquiposId",
                table: "UsuarioEquipo",
                column: "EquiposId",
                principalTable: "Equipo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioEquipo_AspNetUsers_UsuariosId",
                table: "UsuarioEquipo");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioEquipo_Equipo_EquiposId",
                table: "UsuarioEquipo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioEquipo",
                table: "UsuarioEquipo");

            migrationBuilder.RenameColumn(
                name: "UsuariosId",
                table: "UsuarioEquipo",
                newName: "UsuarioId");

            migrationBuilder.RenameColumn(
                name: "EquiposId",
                table: "UsuarioEquipo",
                newName: "Goles");

            migrationBuilder.RenameIndex(
                name: "IX_UsuarioEquipo_UsuariosId",
                table: "UsuarioEquipo",
                newName: "IX_UsuarioEquipo_UsuarioId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UsuarioEquipo",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "EquipoId",
                table: "UsuarioEquipo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Posicion",
                table: "UsuarioEquipo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Puntuacion",
                table: "UsuarioEquipo",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioEquipo",
                table: "UsuarioEquipo",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioEquipo_EquipoId",
                table: "UsuarioEquipo",
                column: "EquipoId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioEquipo_AspNetUsers_UsuarioId",
                table: "UsuarioEquipo",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioEquipo_Equipo_EquipoId",
                table: "UsuarioEquipo",
                column: "EquipoId",
                principalTable: "Equipo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
