using System;

namespace Domain.DTOs
{
    public class EmprestimoDTO
    {
        public Guid ID { get; set; }
        public string Codigo { get; set; }
        public string Ferramenta { get; set; }
        public string CPF { get; set; }
        public string Colaborador { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucao { get; set; }
        public string Status { get; set; }
    }
}
