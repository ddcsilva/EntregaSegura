using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Validators.Helpers;
using FluentValidation;

namespace EntregaSegura.Domain.Validators;

public class MoradorValidator : AbstractValidator<Morador>
{
    public MoradorValidator()
    {
        RuleFor(m => m.Nome)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(m => m.CPF)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Must(CPFValidation.ValidarCPF).WithMessage("O campo {PropertyName} fornecido é inválido")
            .When(c => c.CPF != null);

        RuleFor(c => c.Telefone)
           .NotEmpty().WithMessage("O campo {PropertyName} deve ser fornecido")
           .Must(TelefoneValidation.ValidarTelefone).WithMessage("O campo {PropertyName} fornecido é inválido")
           .When(c => c.Telefone != null);

        RuleFor(c => c.Ramal)
            .NotEmpty().WithMessage("O campo {PropertyName} deve ser fornecido")
            .Length(1, 5).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres")
            .When(c => c.Ramal != null);

        RuleFor(m => m.Email)
            .NotEmpty().WithMessage("O campo {PropertyName} deve ser fornecido")
            .EmailAddress().WithMessage("O campo {PropertyName} fornecido é inválido")
            .When(c => c.Email != null);

        RuleFor(m => m.UnidadeId)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
    }
}