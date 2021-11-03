using System;

namespace Domain.DTOs
{
    public class EmprestimoListagemDTO : ListagemDTO
    {
        public Guid ID { get; set; }
        public string Codigo { get; set; }
        public string Ferramenta { get; set; }
        public string ColaboradorMatricula { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucao { get; set; }
        public string Status { get; set; }
    }
}
