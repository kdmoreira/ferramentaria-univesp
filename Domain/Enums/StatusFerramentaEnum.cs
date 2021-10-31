using System.ComponentModel;

namespace Domain.Enums
{
    public enum StatusFerramentaEnum
    {
        [Description("DISPONÍVEL")]
        Disponivel = 1,

        [Description("EMPRESTADA")]
        Emprestada = 2,
        
        [Description("EM REPARO")]
        Reparo = 3,

        [Description("INDISPONÍVEL")]
        Indisponivel = 4
    }
}
