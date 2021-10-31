using System;

namespace Domain.Models
{
    // TODO: Revisar regras de negócio
    public class Reparo : BaseModel
    {
        public Guid FerramentaID { get; set; }
        public Ferramenta Ferramenta { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
    }
}
