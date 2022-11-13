using System;
using System.Collections.Generic;

namespace Domain.DTOs.AnaliseDados
{
    public class RelatorioEmprestimosDTO
    {
        public int QuantidadeTotal { get; set; }
        public int QuantidadeEmAndamento { get; set; }
        public int QuantidadeEncerrados { get; set; }
        public List<EmprestimoFerramentasMesDTO> QuantidadePorMes { get; set; }
        public double QuantidadeEmAtraso { get; set; }
    }

    public class EmprestimoFerramentasMesDTO
    {
        public string Mes { get; set; }
        public int Quantidade { get; set; }
    }
}
