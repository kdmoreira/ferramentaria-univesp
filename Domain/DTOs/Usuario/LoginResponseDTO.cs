namespace Domain.DTOs.Usuario
{
    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public bool Autenticado { get; set; }
        public string Mensagem { get; set; }
    }
}
