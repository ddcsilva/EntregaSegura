using FluentValidation;
using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Domain.Validations;

public class UnidadeValidator : AbstractValidator<Unidade>
{
    public UnidadeValidator()
    {
        RuleFor(u => u.Bloco)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .GreaterThanOrEqualTo(1).WithMessage("O campo {PropertyName} não pode ser menor que 1")
            .LessThanOrEqualTo(20).WithMessage("O campo {PropertyName} não pode ser maior que 20");

        RuleFor(u => u.Numero)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .GreaterThanOrEqualTo(1).WithMessage("O campo {PropertyName} não pode ser menor que 1")
            .LessThanOrEqualTo(10).WithMessage("O campo {PropertyName} não pode ser maior que 10");

        RuleFor(u => u.Andar)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .GreaterThanOrEqualTo(1).WithMessage("O campo {PropertyName} não pode ser menor que 1")
            .LessThanOrEqualTo(40).WithMessage("O campo {PropertyName} não pode ser maior que 40");

        RuleFor(u => u.CondominioId)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
    }
}
