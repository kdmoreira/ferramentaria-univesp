using System.ComponentModel;

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
