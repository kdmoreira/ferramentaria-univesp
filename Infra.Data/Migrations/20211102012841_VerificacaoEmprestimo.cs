using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Data.Migrations
{
    public partial class VerificacaoEmprestimo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VerificacoesEmprestimos",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UltimaVerificacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CriadoPor = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AtualizadoPor = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerificacoesEmprestimos", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "VerificacoesEmprestimos",
                columns: new[] { "ID", "Ativo", "AtualizadoEm", "AtualizadoPor", "CriadoEm", "CriadoPor", "UltimaVerificacao" },
                values: new object[] { new Guid("091e796b-5819-4fe3-8d45-38c86cec4e3b"), true, null, null, new DateTime(2021, 11, 2, 1, 28, 39, 702, DateTimeKind.Utc).AddTicks(9391), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2021, 11, 2, 1, 28, 39, 702, DateTimeKind.Utc).AddTicks(6169) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VerificacoesEmprestimos");
        }
    }
}
