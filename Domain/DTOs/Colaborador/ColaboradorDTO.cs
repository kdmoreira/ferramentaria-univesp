using System;

namespace Domain.DTOs
{
    public class ColaboradorDTO
    {
        public Guid ID { get; set; }
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Cargo { get; set; }
        public string Departamento { get; set; }
        public string Perfil { get; set; }
    }
}
