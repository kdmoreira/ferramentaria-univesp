using Domain.DTOs;
using FluentValidation;
using System.Linq;
using System.Text.RegularExpressions;

namespace Service.Validators
{
    public class NovaSenhaDTOValidator : AbstractValidator<NovaSenhaDTO>
    {
        public NovaSenhaDTOValidator()
        {
            RuleFor(x => x.SenhaNova)
                .NotEmpty().WithMessage("Informar a senha.")
                .MinimumLength(8).WithMessage("A nova senha deve ter no mínimo 8 caracteres.")
                .MaximumLength(255).WithMessage("A senha deve ter no máximo 255 caracteres")
                .Must(x => x.Any(x => char.IsDigit(x))).WithMessage("A senha deve ter pelo menos um número.")
                .Must(x => x.Any(x => char.IsUpper(x))).WithMessage("A senha deve ter pelo menos uma letra maiúscula.")
                .Must(x => x.Any(x => char.IsLower(x))).WithMessage("A senha deve ter pelo menos uma letra minúscula.")
                .Must(x => x.Any(x => Regex.IsMatch(x.ToString(), @"[!""#$%&'()*+,-./:;?@[\\\]_`{|}~]")))
                .WithMessage("A senha deve ter pelo menos um caractere especial.");
        }
    }
}
