using Domain.Models;
using FluentValidation;

namespace Service.Validators
{
    public class FerramentaValidator : AbstractValidator<Ferramenta>
    {
        public FerramentaValidator()
        {
            RuleFor(x => x.CategoriaID)
                .NotEmpty().WithMessage("Categoria é obrigatória.");

            RuleFor(x => x.Codigo)
                .NotEmpty().WithMessage("Código é obrigatório.")
                .MaximumLength(100).WithMessage("O campo código deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage("Descrição é obrigatória.")
                .MaximumLength(255).WithMessage("O campo descrição deve ter no máximo 255 caracteres.");

            RuleFor(x => x.Fabricante)
                .NotEmpty().WithMessage("Fabricante é obrigatório.")
                .MaximumLength(50).WithMessage("O campo fabricante deve ter no máximo 50 caracteres.");

            RuleFor(x => x.Localizacao)
                .NotEmpty().WithMessage("Localização é obrigatória.")
                .MaximumLength(100).WithMessage("O campo localização deve ter no máximo 100 caracteres.");

            RuleFor(x => x.NumeroPatrimonial)
                .NotEmpty().WithMessage("Número Patrimonial é obrigatório.")
                .MaximumLength(50).WithMessage("O campo número patrimonial deve ter no máximo 50 caracteres.");

            RuleFor(x => x.QuantidadeTotal)
                .NotEmpty().WithMessage("Quantidade Total é obrigatória.")
                .GreaterThan(0).WithMessage("Quantidade total deve ser maior que zero.");

            RuleFor(x => x.ValorCompra)
                .NotEmpty().WithMessage("Valor de Compra é obrigatório.")
                .GreaterThan(0).WithMessage("Valor de compra deve ser maior que zero.");
        }
    }
}
