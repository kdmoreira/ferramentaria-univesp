using Domain.Enums;

namespace Domain.DTOs
{
    public class ColaboradorCriacaoDTO
    {
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Cargo { get; set; }
        public string Departamento { get; set; }
        public RoleEnum Perfil { get; set; }
    }
}
