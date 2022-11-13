using System;
using System.Collections.Generic;

namespace Domain.Models.AnaliseDados
{
    public class RelatorioEmprestimos
    {
        public int QuantidadeTotal { get; set; }
        public int QuantidadeEmAndamento { get; set; }
        public int QuantidadeEncerrados { get; set; }
        public List<EmprestimoFerramentasMes> QuantidadePorMes { get; set; }
        public double QuantidadeEmAtraso { get; set; }
    }

    public class EmprestimoFerramentasMes
    {
        public string Mes { get; set; }
        public int Quantidade { get; set; }
    }
}
