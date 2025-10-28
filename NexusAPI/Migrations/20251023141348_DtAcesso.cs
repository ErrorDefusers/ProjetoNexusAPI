using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexusAPI.Migrations
{
    /// <inheritdoc />
    public partial class DtAcesso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DtAcesso",
                columns: table => new
                {
                    IdDtAcesso = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdFuncionario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdFerramenta = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataAcesso = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DtAcesso", x => x.IdDtAcesso);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DtAcesso");
        }
    }
}
