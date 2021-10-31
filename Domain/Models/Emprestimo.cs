using Domain.Enums;
using System;

namespace Domain.Models
{
    public class Emprestimo : BaseModel
    {
        public Guid FerramentaID { get; set; }
        public Ferramenta Ferramenta { get; set; }
        public Guid ColaboradorID { get; set; }
        public Colaborador Colaborador { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucao { get; set; }
        public string Observacao { get; set; }
        public StatusEmprestimoEnum Status { get; set; } = StatusEmprestimoEnum.EmDia;
    }
}
