using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Categoria : BaseModel
    {
        public string Descricao { get; set; }

        public ICollection<Ferramenta> Ferramentas { get; set; }

        public Categoria() { }

        public Categoria(Guid id, DateTime data, string descricao, bool ativo)
        {
            ID = id;
            Descricao = descricao;
            CriadoEm = data;
            CriadoPor = Guid.Empty;
            Ativo = ativo;
        }
    }
}