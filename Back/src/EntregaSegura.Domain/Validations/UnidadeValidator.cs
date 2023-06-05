using EntregaSegura.Domain.Entities;
using FluentValidation;

namespace EntregaSegura.Domain.Validations;

public class UnidadeValidator : AbstractValidator<Unidade>
{
    public UnidadeValidator()
    {
        RuleFor(u => u.Bloco)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(1, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(u => u.Numero)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

        RuleFor(u => u.CondominioId)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
    }
}