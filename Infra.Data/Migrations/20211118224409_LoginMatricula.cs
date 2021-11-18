using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Data.Migrations
{
    public partial class LoginMatricula : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Colaboradores_CPF",
                table: "Colaboradores");

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "Usuarios",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(11)",
                oldMaxLength: 11);

            migrationBuilder.UpdateData(
                table: "VerificacoesEmprestimos",
                keyColumn: "ID",
                keyValue: new Guid("091e796b-5819-4fe3-8d45-38c86cec4e3b"),
                columns: new[] { "CriadoEm", "UltimaVerificacao" },
                values: new object[] { new DateTime(2021, 11, 18, 22, 44, 7, 334, DateTimeKind.Utc).AddTicks(4593), new DateTime(2021, 11, 18, 22, 44, 7, 334, DateTimeKind.Utc).AddTicks(3144) });

            migrationBuilder.CreateIndex(
                name: "IX_Colaboradores_Matricula",
                table: "Colaboradores",
                column: "Matricula",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Colaboradores_Matricula",
                table: "Colaboradores");

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "Usuarios",
                type: "varchar(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20);

            migrationBuilder.UpdateData(
                table: "VerificacoesEmprestimos",
                keyColumn: "ID",
                keyValue: new Guid("091e796b-5819-4fe3-8d45-38c86cec4e3b"),
                columns: new[] { "CriadoEm", "UltimaVerificacao" },
                values: new object[] { new DateTime(2021, 11, 6, 1, 0, 34, 212, DateTimeKind.Utc).AddTicks(8272), new DateTime(2021, 11, 6, 1, 0, 34, 212, DateTimeKind.Utc).AddTicks(5649) });

            migrationBuilder.CreateIndex(
                name: "IX_Colaboradores_CPF",
                table: "Colaboradores",
                column: "CPF",
                unique: true);
        }
    }
}
