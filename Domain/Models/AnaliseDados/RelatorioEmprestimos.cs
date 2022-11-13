using System;
using System.Collections.Generic;

namespace Domain.Models.AnaliseDados
{
    public class RelatorioEmprestimos
    {
        public int QuantidadeTotal { get; set; }
        public int QuantidadeEmAndamento { get; set; }
        public int QuantidadeEncerrados { get; set; }
        public List<Tuple<int, int>> QuantidadePorMes { get; set; }
        public double QuantidadeEmAtraso { get; set; }
    }
}
