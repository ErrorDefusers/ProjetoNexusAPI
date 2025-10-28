using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexusAPI.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarImagemPerfilFuncionario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagemPerfil",
                table: "Funcionarios",
                type: "VARCHAR(255)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagemPerfil",
                table: "Funcionarios");
        }
    }
}
