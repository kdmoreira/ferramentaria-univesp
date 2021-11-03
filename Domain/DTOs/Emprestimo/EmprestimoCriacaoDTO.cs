using System;

namespace Domain.DTOs
{
    public class EmprestimoCriacaoDTO
    {
        public Guid FerramentaID { get; set; }
        public Guid ColaboradorID { get; set; }
        public int Quantidade { get; set; }
        public int PrazoEmDias { get; set; }
        public string Observacao { get; set; }
        public string SenhaColaborador { get; set; }
    }
}
