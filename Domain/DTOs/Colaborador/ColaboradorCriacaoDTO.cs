using Domain.Enums;
using System;

namespace Domain.DTOs
{
    public class ColaboradorCriacaoDTO
    {
        public string CPF { get; set; }
        public string Matricula { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Cargo { get; set; }
        public string Empresa { get; set; }
        public Guid? SupervisorID { get; set; }        
        public PerfilEnum Perfil { get; set; }
        public RoleEnum Role { get; set; }
    }
}
