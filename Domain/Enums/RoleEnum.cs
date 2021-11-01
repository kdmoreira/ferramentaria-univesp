using System.ComponentModel;

namespace Domain.Enums
{
    public enum RoleEnum
    {
        [Description("ADMINISTRADOR")]
        Administrador = 1,

        [Description("COLABORADOR")]
        Colaborador = 2
    }
}