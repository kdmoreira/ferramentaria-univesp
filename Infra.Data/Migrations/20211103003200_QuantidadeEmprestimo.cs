using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Data.Migrations
{
    public partial class QuantidadeEmprestimo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantidade",
                table: "Emprestimos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "VerificacoesEmprestimos",
                keyColumn: "ID",
                keyValue: new Guid("091e796b-5819-4fe3-8d45-38c86cec4e3b"),
                columns: new[] { "CriadoEm", "UltimaVerificacao" },
                values: new object[] { new DateTime(2021, 11, 3, 0, 31, 59, 33, DateTimeKind.Utc).AddTicks(9810), new DateTime(2021, 11, 3, 0, 31, 59, 33, DateTimeKind.Utc).AddTicks(7392) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantidade",
                table: "Emprestimos");

            migrationBuilder.UpdateData(
                table: "VerificacoesEmprestimos",
                keyColumn: "ID",
                keyValue: new Guid("091e796b-5819-4fe3-8d45-38c86cec4e3b"),
                columns: new[] { "CriadoEm", "UltimaVerificacao" },
                values: new object[] { new DateTime(2021, 11, 2, 1, 28, 39, 702, DateTimeKind.Utc).AddTicks(9391), new DateTime(2021, 11, 2, 1, 28, 39, 702, DateTimeKind.Utc).AddTicks(6169) });
        }
    }
}
