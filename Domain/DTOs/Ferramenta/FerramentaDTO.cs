using System;

namespace Domain.DTOs
{
    public class FerramentaDTO
    {
        public Guid ID { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public int QuantidadeDisponivel { get; set; }
        public int QuantidadeTotal { get; set; }
        public double ValorCompra { get; set; }
        public string Localizacao { get; set; }
        public string Status { get; set; }        
        public string NumeroPatrimonial { get; set; }
        public string Fabricante { get; set; }       
        public string Categoria { get; set; }
        public AfericaoDTO Afericao { get; set; }
    }
}
