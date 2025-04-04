using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proyectodotnet.Migrations
{
    /// <inheritdoc />
    public partial class EquiposUsuariosMyTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_UsuarioEquipo_UsuariosId",
                table: "UsuarioEquipo");

            migrationBuilder.RenameColumn(
                name: "UsuariosId",
                table: "UsuarioEquipo",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "EquiposId",
                table: "UsuarioEquipo",
                newName: "EquipoId");

            migrationBuilder.AddColumn<bool>(
                name: "Confirmado",
                table: "UsuarioEquipo",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioEquipo",
                table: "UsuarioEquipo",
                columns: new[] { "UserId", "EquipoId" });

            migrationBuilder.CreateTable(
                name: "EquipoUser",
                columns: table => new
                {
                    EquiposId = table.Column<int>(type: "int", nullable: false),
                    UsuariosId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipoUser", x => new { x.EquiposId, x.UsuariosId });
                    table.ForeignKey(
                        name: "FK_EquipoUser_AspNetUsers_UsuariosId",
                        column: x => x.UsuariosId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipoUser_Equipo_EquiposId",
                        column: x => x.EquiposId,
                        principalTable: "Equipo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioEquipo_EquipoId",
                table: "UsuarioEquipo",
                column: "EquipoId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipoUser_UsuariosId",
                table: "EquipoUser",
                column: "UsuariosId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioEquipo_AspNetUsers_UserId",
                table: "UsuarioEquipo",
                column: "UserId",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioEquipo_AspNetUsers_UserId",
                table: "UsuarioEquipo");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioEquipo_Equipo_EquipoId",
                table: "UsuarioEquipo");

            migrationBuilder.DropTable(
                name: "EquipoUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioEquipo",
                table: "UsuarioEquipo");

            migrationBuilder.DropIndex(
                name: "IX_UsuarioEquipo_EquipoId",
                table: "UsuarioEquipo");

            migrationBuilder.DropColumn(
                name: "Confirmado",
                table: "UsuarioEquipo");

            migrationBuilder.RenameColumn(
                name: "EquipoId",
                table: "UsuarioEquipo",
                newName: "EquiposId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UsuarioEquipo",
                newName: "UsuariosId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioEquipo",
                table: "UsuarioEquipo",
                columns: new[] { "EquiposId", "UsuariosId" });

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioEquipo_UsuariosId",
                table: "UsuarioEquipo",
                column: "UsuariosId");

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
    }
}
