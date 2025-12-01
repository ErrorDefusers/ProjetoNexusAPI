using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexusAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddFuncionarioFerramentas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FuncionariosFerramentas",
                columns: table => new
                {
                    IdFuncionarioFerramenta = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FuncionarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FerramentaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuncionariosFerramentas", x => x.IdFuncionarioFerramenta);
                    table.ForeignKey(
                        name: "FK_FuncionariosFerramentas_Ferramentas_FerramentaId",
                        column: x => x.FerramentaId,
                        principalTable: "Ferramentas",
                        principalColumn: "IdFerramenta",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FuncionariosFerramentas_Funcionarios_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionarios",
                        principalColumn: "IdFuncionario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FuncionariosFerramentas_FerramentaId",
                table: "FuncionariosFerramentas",
                column: "FerramentaId");

            migrationBuilder.CreateIndex(
                name: "IX_FuncionariosFerramentas_FuncionarioId",
                table: "FuncionariosFerramentas",
                column: "FuncionarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FuncionariosFerramentas");
        }
    }
}
