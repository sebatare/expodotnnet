using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proyectodotnet.Migrations
{
    /// <inheritdoc />
    public partial class AddImageUrlToCancha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Cancha",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Cancha");
        }
    }
}
