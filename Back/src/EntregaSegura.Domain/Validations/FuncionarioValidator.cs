using FluentValidation;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Validations.Helpers;

namespace EntregaSegura.Domain.Validations;

public class FuncionarioValidator : AbstractValidator<Funcionario>
{
    public FuncionarioValidator()
    {
        RuleFor(m => m.Nome)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(m => m.Cpf)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Must(CPFValidation.ValidarCPF).WithMessage("O campo {PropertyName} fornecido é inválido");

        RuleFor(c => c.Telefone)
           .NotEmpty().WithMessage("O campo {PropertyName} deve ser fornecido")
           .Must(TelefoneValidation.ValidarTelefone).WithMessage("O campo {PropertyName} fornecido é inválido");

        RuleFor(m => m.Email)
            .NotEmpty().WithMessage("O campo {PropertyName} deve ser fornecido")
            .EmailAddress().WithMessage("O campo {PropertyName} fornecido é inválido")
            .When(c => !string.IsNullOrWhiteSpace(c.Email));

        RuleFor(c => c.Cargo)
            .IsInEnum().WithMessage("O campo {PropertyName} precisa ser fornecido");

        RuleFor(c => c.DataAdmissao)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .LessThanOrEqualTo(DateTime.Today).WithMessage("O campo {PropertyName} precisa ser menor ou igual à data atual");

        RuleFor(c => c.DataDemissao)
            .GreaterThanOrEqualTo(c => c.DataAdmissao).WithMessage("O campo {PropertyName} precisa ser maior ou igual à data de admissão")
            .LessThanOrEqualTo(DateTime.Today).WithMessage("O campo {PropertyName} precisa ser menor ou igual à data atual")
            .When(c => c.DataDemissao.HasValue);
    }
}