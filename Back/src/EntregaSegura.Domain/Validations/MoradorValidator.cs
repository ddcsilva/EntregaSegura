using FluentValidation;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Validations.Helpers;

namespace EntregaSegura.Domain.Validations;

public class MoradorValidator : AbstractValidator<Morador>
{
    public MoradorValidator()
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

        RuleFor(c => c.Ramal)
            .NotEmpty().WithMessage("O campo {PropertyName} deve ser fornecido")
            .Length(1, 5).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres")
            .When(c => !string.IsNullOrWhiteSpace(c.Ramal));

        RuleFor(c => c.Foto)
            .Length(1, 100).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres")
            .When(c => !string.IsNullOrWhiteSpace(c.Foto));

        RuleFor(m => m.Email)
            .NotEmpty().WithMessage("O campo {PropertyName} deve ser fornecido")
            .EmailAddress().WithMessage("O campo {PropertyName} fornecido é inválido")
            .When(c => !string.IsNullOrWhiteSpace(c.Email));

        RuleFor(m => m.UnidadeId)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
    }
}