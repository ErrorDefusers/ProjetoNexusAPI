using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexusAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddImagemCapaToCursos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagemCapa",
                table: "Cursos",
                type: "VARCHAR(255)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagemCapa",
                table: "Cursos");
        }
    }
}
