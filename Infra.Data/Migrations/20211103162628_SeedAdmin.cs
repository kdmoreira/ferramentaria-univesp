using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Data.Migrations
{
    public partial class SeedAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Colaboradores",
                columns: new[] { "ID", "Ativo", "AtualizadoEm", "AtualizadoPor", "CPF", "Cargo", "CriadoEm", "CriadoPor", "Email", "Empresa", "Matricula", "Nome", "Perfil", "Sobrenome", "SupervisorID", "Telefone" },
                values: new object[] { new Guid("c20cf935-802e-4dba-be10-14131ea6279a"), false, null, null, "12345678912", "Admin", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "N/A", "N/A", "000", "Admin", 1, "Admin", null, "(00)0000-0000" });

            migrationBuilder.UpdateData(
                table: "VerificacoesEmprestimos",
                keyColumn: "ID",
                keyValue: new Guid("091e796b-5819-4fe3-8d45-38c86cec4e3b"),
                columns: new[] { "CriadoEm", "UltimaVerificacao" },
                values: new object[] { new DateTime(2021, 11, 3, 16, 26, 27, 235, DateTimeKind.Utc).AddTicks(6595), new DateTime(2021, 11, 3, 16, 26, 27, 235, DateTimeKind.Utc).AddTicks(5056) });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "ID", "Ativo", "AtualizadoEm", "AtualizadoPor", "ColaboradorID", "CriadoEm", "CriadoPor", "Login", "Role", "Senha", "Token" },
                values: new object[] { new Guid("bb9ac2c8-c7d4-4c21-b6cf-84419b12a810"), true, null, null, new Guid("c20cf935-802e-4dba-be10-14131ea6279a"), new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "12345678912", 1, "$2a$12$ti.QT85lj9IANHP8VfAue.X4yzVM458OqmCtR0d1zBp9yv4mK2CGC", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "ID",
                keyValue: new Guid("bb9ac2c8-c7d4-4c21-b6cf-84419b12a810"));

            migrationBuilder.DeleteData(
                table: "Colaboradores",
                keyColumn: "ID",
                keyValue: new Guid("c20cf935-802e-4dba-be10-14131ea6279a"));

            migrationBuilder.UpdateData(
                table: "VerificacoesEmprestimos",
                keyColumn: "ID",
                keyValue: new Guid("091e796b-5819-4fe3-8d45-38c86cec4e3b"),
                columns: new[] { "CriadoEm", "UltimaVerificacao" },
                values: new object[] { new DateTime(2021, 11, 3, 0, 31, 59, 33, DateTimeKind.Utc).AddTicks(9810), new DateTime(2021, 11, 3, 0, 31, 59, 33, DateTimeKind.Utc).AddTicks(7392) });
        }
    }
}
