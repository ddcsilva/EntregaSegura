using FluentValidation;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Validations.Helpers;

namespace EntregaSegura.Domain.Validations;

public class MoradorValidator : AbstractValidator<Morador>
{
    public MoradorValidator()
    {
        RuleFor(c => c.Ramal)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .GreaterThanOrEqualTo(1).WithMessage("O campo {PropertyName} não pode ser menor que 1")
            .LessThanOrEqualTo(9999).WithMessage("O campo {PropertyName} não pode ser maior que 9999");

        RuleFor(m => m.UnidadeId)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
    }
}