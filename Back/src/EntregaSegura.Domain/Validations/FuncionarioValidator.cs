using FluentValidation;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Validations.Helpers;

namespace EntregaSegura.Domain.Validations;

public class FuncionarioValidator : AbstractValidator<Funcionario>
{
    public FuncionarioValidator()
    {
        RuleFor(c => c.DataAdmissao.Date)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .LessThanOrEqualTo(DateTime.Today).WithMessage("O campo {PropertyName} precisa ser menor ou igual à data atual");

        RuleFor(c => c.DataDemissao)
            .GreaterThanOrEqualTo(c => c.DataAdmissao).WithMessage("O campo {PropertyName} precisa ser maior ou igual à data de admissão")
            .LessThanOrEqualTo(DateTime.Today).WithMessage("O campo {PropertyName} precisa ser menor ou igual à data atual")
            .When(c => c.DataDemissao.HasValue);
    }
}