using System.ComponentModel;

namespace Domain.Enums
{
    public enum StatusEmprestimoEnum
    {
        [Description("EM DIA")]
        EmDia = 1,

        [Description("EM ATRASO")]
        EmAtraso = 2,

        [Description("FINALIZADO")]
        Finalizado = 3,
    }
}
