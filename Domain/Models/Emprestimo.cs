using Domain.Enums;
using System;

namespace Domain.Models
{
    public class Emprestimo : BaseModel
    {
        public Guid FerramentaID { get; private set; }
        public Ferramenta Ferramenta { get; set; }
        public Guid ColaboradorID { get; private set; }
        public Colaborador Colaborador { get; set; }
        public DateTime DataEmprestimo { get; private set; }
        public DateTime DataDevolucao { get; private set; }
        public int Quantidade { get; private set; }
        public string Observacao { get; private set; }
        public StatusEmprestimoEnum Status { get; private set; } = StatusEmprestimoEnum.EmDia;

        public void Realizar(int prazoEmDias)
        {
            DataEmprestimo = DateTime.UtcNow;
            DataDevolucao = DataEmprestimo.AddDays(prazoEmDias);
        }

        public void Finalizar()
        {
            Status = StatusEmprestimoEnum.Finalizado;
            Inativar();
        }

        public bool VerificarAtraso(DateTime dataAtual)
        {
            if (DataDevolucao.Date < dataAtual)
            {
                Status = StatusEmprestimoEnum.EmAtraso;
                return true;
            }
            return false;
        }
    }
}
