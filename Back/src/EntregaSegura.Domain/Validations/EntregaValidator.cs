using FluentValidation;
using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Domain.Validations;

public class EntregaValidator : AbstractValidator<Entrega>
{
    public EntregaValidator()
    {
        RuleFor(e => e.DataRecebimento)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

        RuleFor(e => e.DataRetirada)
            .GreaterThanOrEqualTo(c => c.DataRecebimento).WithMessage("O campo {PropertyName} precisa ser maior ou igual à data de recebimento")
            .When(c => c.DataRetirada.HasValue);

        RuleFor(e => e.Descricao)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(e => e.Status)
            .IsInEnum().WithMessage("O campo {PropertyName} precisa ser fornecido");

        RuleFor(e => e.TransportadoraId)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

        RuleFor(e => e.MoradorId)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

        RuleFor(e => e.FuncionarioId)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
    }
}