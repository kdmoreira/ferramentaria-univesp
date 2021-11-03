using System;

namespace Domain.Models
{
    public class VerificacaoEmprestimo : BaseModel
    {
        public DateTime UltimaVerificacao { get; set; }

        public void Atualizar(DateTime dataAtual)
        {
            UltimaVerificacao = dataAtual;
        }
    }
}
