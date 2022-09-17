using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Afericoes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ferramenta_id = table.Column<Guid>(type: "uuid", nullable: false),
                    intervalo_dias = table.Column<int>(type: "int", nullable: false),
                    data_ultima = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ativo = table.Column<bool>(type: "boolean", nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    criado_por = table.Column<Guid>(type: "uuid", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    atualizado_por = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_afericoes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    descricao = table.Column<string>(type: "varchar", maxLength: 30, nullable: false),
                    ativo = table.Column<bool>(type: "boolean", nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    criado_por = table.Column<Guid>(type: "uuid", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    atualizado_por = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categorias", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Colaboradores",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    cpf = table.Column<string>(type: "varchar", maxLength: 11, nullable: false),
                    matricula = table.Column<string>(type: "varchar", maxLength: 20, nullable: false),
                    nome = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                    sobrenome = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "varchar", maxLength: 50, nullable: false),
                    telefone = table.Column<string>(type: "varchar", maxLength: 20, nullable: false),
                    cargo = table.Column<string>(type: "varchar", maxLength: 100, nullable: false),
                    empresa = table.Column<string>(type: "varchar", maxLength: 100, nullable: false),
                    supervisor_id = table.Column<Guid>(type: "uuid", nullable: true),
                    perfil = table.Column<int>(type: "int", nullable: false),
                    ativo = table.Column<bool>(type: "boolean", nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    criado_por = table.Column<Guid>(type: "uuid", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    atualizado_por = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_colaboradores", x => x.id);
                    table.ForeignKey(
                        name: "fk_colaboradores_colaboradores_supervisor_id",
                        column: x => x.supervisor_id,
                        principalTable: "Colaboradores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VerificacoesEmprestimos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ultima_verificacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ativo = table.Column<bool>(type: "boolean", nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    criado_por = table.Column<Guid>(type: "uuid", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    atualizado_por = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_verificacoes_emprestimos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Ferramentas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    codigo = table.Column<string>(type: "varchar", maxLength: 100, nullable: false),
                    descricao = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                    numero_patrimonial = table.Column<string>(type: "varchar", maxLength: 50, nullable: false),
                    fabricante = table.Column<string>(type: "varchar", maxLength: 50, nullable: false),
                    quantidade_disponivel = table.Column<int>(type: "int", nullable: false),
                    quantidade_total = table.Column<int>(type: "int", nullable: false),
                    valor_compra = table.Column<double>(type: "float", nullable: false),
                    localizacao = table.Column<string>(type: "varchar", maxLength: 100, nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    categoria_id = table.Column<Guid>(type: "uuid", nullable: false),
                    afericao_id = table.Column<Guid>(type: "uuid", nullable: true),
                    ativo = table.Column<bool>(type: "boolean", nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    criado_por = table.Column<Guid>(type: "uuid", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    atualizado_por = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ferramentas", x => x.id);
                    table.ForeignKey(
                        name: "fk_ferramentas_afericoes_afericao_id",
                        column: x => x.afericao_id,
                        principalTable: "Afericoes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_ferramentas_categorias_categoria_id",
                        column: x => x.categoria_id,
                        principalTable: "Categorias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    login = table.Column<string>(type: "varchar", maxLength: 20, nullable: false),
                    senha = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                    token = table.Column<string>(type: "varchar", maxLength: 1000, nullable: true),
                    role = table.Column<int>(type: "int", nullable: false),
                    colaborador_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ativo = table.Column<bool>(type: "boolean", nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    criado_por = table.Column<Guid>(type: "uuid", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    atualizado_por = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_usuarios", x => x.id);
                    table.ForeignKey(
                        name: "fk_usuarios_colaboradores_colaborador_id",
                        column: x => x.colaborador_id,
                        principalTable: "Colaboradores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Emprestimos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ferramenta_id = table.Column<Guid>(type: "uuid", nullable: false),
                    colaborador_id = table.Column<Guid>(type: "uuid", nullable: false),
                    data_emprestimo = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    data_devolucao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    quantidade = table.Column<int>(type: "int", nullable: false),
                    observacao = table.Column<string>(type: "varchar", maxLength: 255, nullable: true),
                    status = table.Column<int>(type: "int", nullable: false),
                    ativo = table.Column<bool>(type: "boolean", nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    criado_por = table.Column<Guid>(type: "uuid", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    atualizado_por = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_emprestimos", x => x.id);
                    table.ForeignKey(
                        name: "fk_emprestimos_colaboradores_colaborador_id",
                        column: x => x.colaborador_id,
                        principalTable: "Colaboradores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_emprestimos_ferramentas_ferramenta_id",
                        column: x => x.ferramenta_id,
                        principalTable: "Ferramentas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reparos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ferramenta_id = table.Column<Guid>(type: "uuid", nullable: false),
                    data_inicio = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    data_fim = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ativo = table.Column<bool>(type: "boolean", nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    criado_por = table.Column<Guid>(type: "uuid", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    atualizado_por = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reparos", x => x.id);
                    table.ForeignKey(
                        name: "fk_reparos_ferramentas_ferramenta_id",
                        column: x => x.ferramenta_id,
                        principalTable: "Ferramentas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "id", "ativo", "atualizado_em", "atualizado_por", "criado_em", "criado_por", "descricao" },
                values: new object[,]
                {
                    { new Guid("5138f09b-7dc6-4e06-a983-c182e6d7d173"), true, null, null, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Ferramenta" },
                    { new Guid("61858a59-e022-4ace-8531-8db2d62b739e"), true, null, null, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Dispositivo" },
                    { new Guid("70daebeb-22d5-4b70-8052-9211b92b9552"), true, null, null, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Instrumento" },
                    { new Guid("1d16e4d4-59ac-438f-b484-fa097d3be8f3"), true, null, null, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Equipamento" }
                });

            migrationBuilder.InsertData(
                table: "Colaboradores",
                columns: new[] { "id", "ativo", "atualizado_em", "atualizado_por", "cpf", "cargo", "criado_em", "criado_por", "email", "empresa", "matricula", "nome", "perfil", "sobrenome", "supervisor_id", "telefone" },
                values: new object[] { new Guid("c20cf935-802e-4dba-be10-14131ea6279a"), false, null, null, "12345678912", "Admin", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "N/A", "N/A", "000", "Admin", 1, "Admin", null, "(00)0000-0000" });

            migrationBuilder.InsertData(
                table: "VerificacoesEmprestimos",
                columns: new[] { "id", "ativo", "atualizado_em", "atualizado_por", "criado_em", "criado_por", "ultima_verificacao" },
                values: new object[] { new Guid("091e796b-5819-4fe3-8d45-38c86cec4e3b"), true, null, null, new DateTime(2022, 9, 17, 21, 41, 40, 134, DateTimeKind.Utc).AddTicks(4719), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 9, 17, 21, 41, 40, 134, DateTimeKind.Utc).AddTicks(3676) });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "id", "ativo", "atualizado_em", "atualizado_por", "colaborador_id", "criado_em", "criado_por", "login", "role", "senha", "token" },
                values: new object[] { new Guid("bb9ac2c8-c7d4-4c21-b6cf-84419b12a810"), true, null, null, new Guid("c20cf935-802e-4dba-be10-14131ea6279a"), new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "12345678912", 1, "$2a$12$ti.QT85lj9IANHP8VfAue.X4yzVM458OqmCtR0d1zBp9yv4mK2CGC", null });

            migrationBuilder.CreateIndex(
                name: "ix_colaboradores_matricula",
                table: "Colaboradores",
                column: "matricula",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_colaboradores_supervisor_id",
                table: "Colaboradores",
                column: "supervisor_id");

            migrationBuilder.CreateIndex(
                name: "ix_emprestimos_colaborador_id",
                table: "Emprestimos",
                column: "colaborador_id");

            migrationBuilder.CreateIndex(
                name: "ix_emprestimos_ferramenta_id",
                table: "Emprestimos",
                column: "ferramenta_id");

            migrationBuilder.CreateIndex(
                name: "ix_ferramentas_afericao_id",
                table: "Ferramentas",
                column: "afericao_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_ferramentas_categoria_id",
                table: "Ferramentas",
                column: "categoria_id");

            migrationBuilder.CreateIndex(
                name: "ix_ferramentas_codigo",
                table: "Ferramentas",
                column: "codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_reparos_ferramenta_id",
                table: "Reparos",
                column: "ferramenta_id");

            migrationBuilder.CreateIndex(
                name: "ix_usuarios_colaborador_id",
                table: "Usuarios",
                column: "colaborador_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_usuarios_login",
                table: "Usuarios",
                column: "login",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Emprestimos");

            migrationBuilder.DropTable(
                name: "Reparos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "VerificacoesEmprestimos");

            migrationBuilder.DropTable(
                name: "Ferramentas");

            migrationBuilder.DropTable(
                name: "Colaboradores");

            migrationBuilder.DropTable(
                name: "Afericoes");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
