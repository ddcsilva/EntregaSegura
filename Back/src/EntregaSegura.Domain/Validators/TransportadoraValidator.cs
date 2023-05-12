using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Validators.Helpers;
using FluentValidation;

namespace EntregaSegura.Domain.Validators;

public class TransportadoraValidator : AbstractValidator<Transportadora>
{
    public TransportadoraValidator()
    {
        RuleFor(c => c.Nome)
            .NotEmpty().WithMessage("O campo {PropertyName} deve ser fornecido")
            .Length(2, 100).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(c => c.CNPJ)
            .Must(CNPJValidation.ValidarCNPJ).WithMessage("O campo {PropertyName} fornecido é inválido")
            .When(c => c.CNPJ != null);

        RuleFor(c => c.Telefone)
            .Must(TelefoneValidation.ValidarTelefone).WithMessage("O campo {PropertyName} fornecido é inválido")
            .When(c => c.Telefone != null);

        RuleFor(c => c.Email)
            .EmailAddress().WithMessage("O campo {PropertyName} fornecido é inválido")
            .When(c => c.Email != null);
    }
}