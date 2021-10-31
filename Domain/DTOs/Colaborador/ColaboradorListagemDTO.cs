using System;

namespace Domain.DTOs
{
    public class ColaboradorListagemDTO
    {
        public Guid ID { get; set; }
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string Cargo { get; set; }
        public string Status { get; set; }
    }
}
