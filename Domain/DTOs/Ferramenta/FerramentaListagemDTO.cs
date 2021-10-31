using System;

namespace Domain.DTOs
{
    public class FerramentaListagemDTO : ListagemDTO
    {
        public Guid ID { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public string Status { get; set; }              
    }
}
