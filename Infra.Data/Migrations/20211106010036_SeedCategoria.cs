using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Data.Migrations
{
    public partial class SeedCategoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "ID", "Ativo", "AtualizadoEm", "AtualizadoPor", "CriadoEm", "CriadoPor", "Descricao" },
                values: new object[,]
                {
                    { new Guid("5138f09b-7dc6-4e06-a983-c182e6d7d173"), true, null, null, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Ferramenta" },
                    { new Guid("61858a59-e022-4ace-8531-8db2d62b739e"), true, null, null, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Dispositivo" },
                    { new Guid("70daebeb-22d5-4b70-8052-9211b92b9552"), true, null, null, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Instrumento" },
                    { new Guid("1d16e4d4-59ac-438f-b484-fa097d3be8f3"), true, null, null, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Equipamento" }
                });

            migrationBuilder.UpdateData(
                table: "VerificacoesEmprestimos",
                keyColumn: "ID",
                keyValue: new Guid("091e796b-5819-4fe3-8d45-38c86cec4e3b"),
                columns: new[] { "CriadoEm", "UltimaVerificacao" },
                values: new object[] { new DateTime(2021, 11, 6, 1, 0, 34, 212, DateTimeKind.Utc).AddTicks(8272), new DateTime(2021, 11, 6, 1, 0, 34, 212, DateTimeKind.Utc).AddTicks(5649) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "ID",
                keyValue: new Guid("1d16e4d4-59ac-438f-b484-fa097d3be8f3"));

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "ID",
                keyValue: new Guid("5138f09b-7dc6-4e06-a983-c182e6d7d173"));

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "ID",
                keyValue: new Guid("61858a59-e022-4ace-8531-8db2d62b739e"));

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "ID",
                keyValue: new Guid("70daebeb-22d5-4b70-8052-9211b92b9552"));

            migrationBuilder.UpdateData(
                table: "VerificacoesEmprestimos",
                keyColumn: "ID",
                keyValue: new Guid("091e796b-5819-4fe3-8d45-38c86cec4e3b"),
                columns: new[] { "CriadoEm", "UltimaVerificacao" },
                values: new object[] { new DateTime(2021, 11, 3, 16, 26, 27, 235, DateTimeKind.Utc).AddTicks(6595), new DateTime(2021, 11, 3, 16, 26, 27, 235, DateTimeKind.Utc).AddTicks(5056) });
        }
    }
}
