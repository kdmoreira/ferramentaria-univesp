using System;

namespace Domain.DTOs
{
    public class FerramentaCriacaoDTO
    {
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public int QuantidadeTotal { get; set; }
        public double ValorCompra { get; set; }
        public string Localizacao { get; set; }
        public string NumeroPatrimonial { get; set; }
        public string Fabricante { get; set; }
        public Guid CategoriaID { get; set; }
    }
}
