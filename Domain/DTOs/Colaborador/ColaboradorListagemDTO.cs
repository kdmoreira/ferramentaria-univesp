using System;

namespace Domain.DTOs
{
    public class ColaboradorListagemDTO : ListagemDTO
    {
        public Guid ID { get; set; }
        public string Matricula { get; set; }
        public string NomeCompleto { get; set; }
        public string SituacaoEmprestimo { get; set; }
    }
}
