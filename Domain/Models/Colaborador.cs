using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Colaborador : BaseModel
    {
        public string CPF { get; private set; }
        public string Matricula { get; private set; }
        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public string Email { get; private set; }
        public string Telefone { get; private set; }
        public string Cargo { get; private set; }
        public string Empresa { get; private set; }
        public Guid? SupervisorID { get; private set; }
        public Colaborador Supervisor { get; private set; }
        public PerfilEnum Perfil { get; private set; }

        public Usuario Usuario { get; set; }
        public ICollection<Emprestimo> Emprestimos { get; set; }
        public ICollection<Colaborador> Supervisionados { get; set; }

        public Colaborador() { }

        public Colaborador(Guid id, DateTime data, string cpf, string matricula, string nome, string sobrenome, string email,
            string telefone, string cargo, string empresa, PerfilEnum perfil, bool ativo)
        {
            ID = id;
            CPF = cpf;
            Matricula = matricula;
            Nome = nome;
            Sobrenome = sobrenome;
            Email = email;
            Telefone = telefone;
            Cargo = cargo;
            Empresa = empresa;
            Perfil = perfil;
            CriadoEm = data;
            CriadoPor = Guid.Empty;
            Ativo = ativo;
        }
    }
}
