using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Colaborador : BaseModel
    {
        public string CPF { get; set; }
        public string Matricula { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Cargo { get; set; }
        public string Empresa { get; set; }
        public RoleEnum Role { get; set; }
        public Guid? SupervisorID { get; set; }
        public Colaborador Supervisor { get; set; }
        public PerfilEnum Perfil { get; set; }

        public ICollection<Emprestimo> Emprestimos { get; set; }
        public ICollection<Colaborador> Supervisionados { get; set; }
    }
}
