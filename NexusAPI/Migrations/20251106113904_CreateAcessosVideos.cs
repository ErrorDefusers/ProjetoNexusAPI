using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexusAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreateAcessosVideos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Criação da tabela AcessosVideos
            migrationBuilder.CreateTable(
                name: "AcessosVideos",
                columns: table => new
                {
                    IdAcessoVideo = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FuncionarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CursoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DtAcesso = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcessosVideos", x => x.IdAcessoVideo);
                    table.ForeignKey(
                        name: "FK_AcessosVideos_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "IdCurso",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AcessosVideos_Funcionarios_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionarios",
                        principalColumn: "IdFuncionario",
                        onDelete: ReferentialAction.Cascade);
                });

            // Índices para melhorar performance
            migrationBuilder.CreateIndex(
                name: "IX_AcessosVideos_CursoId",
                table: "AcessosVideos",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_AcessosVideos_FuncionarioId",
                table: "AcessosVideos",
                column: "FuncionarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Dropa a tabela caso seja feito rollback
            migrationBuilder.DropTable(
                name: "AcessosVideos");
        }
    }
}
