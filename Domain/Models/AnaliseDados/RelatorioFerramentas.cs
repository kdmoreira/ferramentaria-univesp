﻿namespace Domain.Models.AnaliseDados
{
    public class RelatorioFerramentas
    {
        public int QuantidadeTotal { get; set; }
        public int QuantidadeDisponivel { get; set; }
        public int QuantidadeEmprestada { get; set; }
        public double MediaQuantidadePorFerramenta { get; set; }
        public int QuantidadeInativas { get; set; }
    }
}