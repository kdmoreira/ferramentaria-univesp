using Domain.Models;
using FluentValidation;

namespace Service.Validators
{
    public class EmprestimoValidator : AbstractValidator<Emprestimo>
    {
        public EmprestimoValidator()
        {
            RuleFor(x => x.ColaboradorID)
                .NotEmpty().WithMessage("Colaborador é obrigatório.");

            RuleFor(x => x.FerramentaID)
                .NotEmpty().WithMessage("Ferramenta é obrigatória.");

            RuleFor(x => x.DataDevolucao.Date)
                .GreaterThan(x => x.DataEmprestimo).WithMessage("A data de devolução deve ser maior que a data de empréstimo.");

            RuleFor(x => x.Quantidade)
                .GreaterThan(0).WithMessage("A quantidade a ser emprestada deve ser maior que zero.");
        }
    }
}
