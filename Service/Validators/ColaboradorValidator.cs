using Domain.Enums;
using Domain.Models;
using FluentValidation;
using System;

namespace Service.Validators
{
    public class ColaboradorValidator : AbstractValidator<Colaborador>
    {
        public ColaboradorValidator()
        {
            RuleFor(x => x.CPF)
                .NotEmpty().WithMessage("CPF é obrigatório.")
                .Length(11).WithMessage("O campo CPF deve ter 11 caracteres.");

            RuleFor(x => x.Matricula)
                .NotEmpty().WithMessage("Matrícula é obrigatória.")
                .MaximumLength(20).WithMessage("O campo matrícula deve ter 20 caracteres.");

            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório.")
                .MaximumLength(255).WithMessage("O campo nome deve ter 255 caracteres.");

            RuleFor(x => x.Sobrenome)
                .NotEmpty().WithMessage("Sobrenome é obrigatório.")
                .MaximumLength(255).WithMessage("O campo sobrenome deve ter 255 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email é obrigatório.")
                .EmailAddress().WithMessage("Informar um email válido.");

            RuleFor(x => x.Telefone)
                .NotEmpty().WithMessage("Telefone é obrigatório.")
                .MaximumLength(20).WithMessage("O campo telefone deve ter 20 caracteres.");

            RuleFor(x => x.Cargo)
                .NotEmpty().WithMessage("Cargo é obrigatório.")
                .MaximumLength(100).WithMessage("O campo cargo deve ter 100 caracteres.");

            RuleFor(x => x.Empresa)
                .NotEmpty().WithMessage("Empresa é obrigatória.")
                .MaximumLength(100).WithMessage("O campo empresa deve ter 100 caracteres.");

            RuleFor(x => (int)x.Perfil)
                .NotEmpty().WithMessage("Perfil é obrigatório.")
                .InclusiveBetween(0, Enum.GetNames(typeof(PerfilEnum)).Length)
                .WithMessage($"Perfil do colaborador com valores válidos entre 1 e {Enum.GetNames(typeof(PerfilEnum)).Length}.");
        }
    }
}
