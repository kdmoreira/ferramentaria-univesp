using Domain.Audits;
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
        public StatusFerramentaEnum Status { get; set; } = StatusFerramentaEnum.Disponivel;
        public Guid CategoriaID { get; set; }
        public Categoria Categoria { get; set; }
        public Guid? AfericaoID { get; set; }
        public Afericao Afericao { get; set; }

        public ICollection<Emprestimo> Emprestimos { get; set; }
        public ICollection<Reparo> Reparos { get; set; }

        public override void Ativar()
        {
            Ativo = false;
            Status = StatusFerramentaEnum.Disponivel;
        }

        public override void Inativar()
        {
            Ativo = false;
            Status = StatusFerramentaEnum.Indisponivel;
        }        

        public void AtualizarDisponibilidade(Ferramenta antiga)
        {
            QuantidadeDisponivel = antiga.QuantidadeDisponivel;
        }

        public void Cadastro()
        {
            QuantidadeDisponivel = QuantidadeTotal;
        }
    }
}
