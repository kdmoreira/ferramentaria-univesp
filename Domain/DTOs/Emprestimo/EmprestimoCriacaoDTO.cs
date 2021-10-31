using System;

namespace Domain.DTOs
{
    public class EmprestimoCriacaoDTO
    {
        public Guid ID { get; set; }
        public Guid FerramentaID { get; set; }
        public Guid ColaboradorID { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucao { get; set; }
    }
}
