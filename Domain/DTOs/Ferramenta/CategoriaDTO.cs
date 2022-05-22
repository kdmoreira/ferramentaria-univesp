using System;

namespace Domain.DTOs
{
    public class CategoriaDTO : ListagemDTO
    {
        public Guid ID { get; set; }
        public string Descricao { get; set; }
    }
}
