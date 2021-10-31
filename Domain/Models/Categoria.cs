using System.Collections.Generic;

namespace Domain.Models
{
    public class Categoria : BaseModel
    {
        public string Descricao { get; set; }

        public ICollection<Ferramenta> Ferramentas { get; set; }
    }
}