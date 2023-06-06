using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Validations.Helpers;
using FluentValidation;

namespace EntregaSegura.Domain.Validations;

public class TransportadoraValidator : AbstractValidator<Transportadora>
{
    public TransportadoraValidator()
    {
        RuleFor(c => c.Nome)
            .NotEmpty().WithMessage("O campo {PropertyName} deve ser fornecido")
            .Length(2, 100).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(c => c.Cnpj)
            .Must(CNPJValidation.ValidarCNPJ).WithMessage("O campo {PropertyName} fornecido é inválido")
            .When(c => !string.IsNullOrWhiteSpace(c.Cnpj));

        RuleFor(c => c.Telefone)
            .Must(TelefoneValidation.ValidarTelefone).WithMessage("O campo {PropertyName} fornecido é inválido")
            .When(c => !string.IsNullOrWhiteSpace(c.Telefone));

        RuleFor(c => c.Email)
            .EmailAddress().WithMessage("O campo {PropertyName} fornecido é inválido")
            .Length(5, 100).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres")
            .When(c => !string.IsNullOrWhiteSpace(c.Email));
    }
}