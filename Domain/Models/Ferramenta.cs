using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Ferramenta : BaseModel
    {
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public string NumeroPatrimonial { get; set; }
        public string Fabricante { get; set; }
        public int QuantidadeDisponivel { get; set; }
        public int QuantidadeTotal { get; set; }
        public double ValorCompra { get; set; }
        public string Localizacao { get; set; }
        public StatusFerramentaEnum Status { get; private set; } = StatusFerramentaEnum.Disponivel;
        public Guid CategoriaID { get; set; }
        public Categoria Categoria { get; set; }
        public Guid? AfericaoID { get; private set; }
        public Afericao Afericao { get; set; }

        public ICollection<Emprestimo> Emprestimos { get; set; }
        public ICollection<Reparo> Reparos { get; set; }

        public override void Ativar()
        {
            Ativo = true;
            Status = StatusFerramentaEnum.Disponivel;
        }

        public override void Inativar()
        {
            Ativo = false;
            Status = StatusFerramentaEnum.Indisponivel;
        }

        public void RecuperarPropriedades(Ferramenta antiga)
        {
            if (QuantidadeTotal > antiga.QuantidadeTotal)
            {
                var diferenca = QuantidadeTotal - antiga.QuantidadeTotal;
                QuantidadeDisponivel = antiga.QuantidadeDisponivel + diferenca;
            }
            else
            {
                QuantidadeDisponivel = antiga.QuantidadeDisponivel;
            }
            AfericaoID = antiga.AfericaoID;
            AtualizarStatus();
        }

        public void Cadastrar()
        {
            QuantidadeDisponivel = QuantidadeTotal;
        }

        public void Emprestar(int quantidade)
        {
            var quantidadeRestante = QuantidadeDisponivel - quantidade;
            if (quantidadeRestante < 0)
                throw new InvalidOperationException("Não há ferramenta suficiente na quantidade solicitada.");

            QuantidadeDisponivel = quantidadeRestante;
            AtualizarStatus();
        }        

        public void Devolver(int quantidade)
        {            
            QuantidadeDisponivel += quantidade;
            Status = StatusFerramentaEnum.Disponivel;
        }

        // Private Methods
        private void AtualizarStatus()
        {
            if (QuantidadeDisponivel == 0)
                Status = StatusFerramentaEnum.Emprestada;
            else
                Status = StatusFerramentaEnum.Disponivel;
        }
    }
}
