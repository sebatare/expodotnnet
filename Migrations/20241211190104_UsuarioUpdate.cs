using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proyectodotnet.Migrations
{
    /// <inheritdoc />
    public partial class UsuarioUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SegundoNombre",
                table: "AspNetUsers",
                newName: "SecondName");

            migrationBuilder.RenameColumn(
                name: "PrimerNombre",
                table: "AspNetUsers",
                newName: "SecondLastName");

            migrationBuilder.RenameColumn(
                name: "FechaNacimiento",
                table: "AspNetUsers",
                newName: "DateOfBirth");

            migrationBuilder.RenameColumn(
                name: "Edad",
                table: "AspNetUsers",
                newName: "Age");

            migrationBuilder.AlterColumn<string>(
                name: "Rut",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "SecondName",
                table: "AspNetUsers",
                newName: "SegundoNombre");

            migrationBuilder.RenameColumn(
                name: "SecondLastName",
                table: "AspNetUsers",
                newName: "PrimerNombre");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "AspNetUsers",
                newName: "FechaNacimiento");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "AspNetUsers",
                newName: "Edad");

            migrationBuilder.AlterColumn<string>(
                name: "Rut",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
