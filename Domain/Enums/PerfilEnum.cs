using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum PerfilEnum
    {
        [Description("COLABORADOR")]
        Colaborador = 1,

        [Description("SUPERVISOR")]
        Supervisor = 2,

        [Description("GERENTE")]
        Gerente = 3
    }
}
