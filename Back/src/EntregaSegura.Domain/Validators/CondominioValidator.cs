
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Validators.Helpers;
using FluentValidation;

namespace EntregaSegura.Domain.Validators;

public class CondominioValidator : AbstractValidator<Condominio>
{
    public CondominioValidator()
    {
        RuleFor(c => c.Nome)
            .NotEmpty().WithMessage("O campo {PropertyName} deve ser fornecido")
            .Length(2, 100).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(c => c.CNPJ)
            .NotEmpty().WithMessage("O campo {PropertyName} deve ser fornecido")
            .Must(CNPJValidation.ValidarCNPJ).WithMessage("O campo {PropertyName} fornecido é inválido")
            .When(c => c.CNPJ != null);

        RuleFor(c => c.Telefone)
            .NotEmpty().WithMessage("O campo {PropertyName} deve ser fornecido")
            .Must(TelefoneValidation.ValidarTelefone).WithMessage("O campo {PropertyName} fornecido é inválido")
            .When(c => c.Telefone != null);

        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("O campo {PropertyName} deve ser fornecido")
            .MaximumLength(100).WithMessage("O campo {PropertyName} deve ter no máximo {MaxLength} caracteres")
            .EmailAddress().WithMessage("O campo {PropertyName} fornecido é inválido")
            .When(c => c.Email != null);

        RuleFor(c => c.Logradouro)
            .NotEmpty().WithMessage("O campo {PropertyName} deve ser fornecido")
            .Length(2, 100).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(c => c.Numero)
            .NotEmpty().WithMessage("O campo {PropertyName} deve ser fornecido")
            .MaximumLength(10).WithMessage("O campo {PropertyName} deve ter no máximo {MaxLength} caracteres");

        RuleFor(c => c.Bairro)
            .NotEmpty().WithMessage("O campo {PropertyName} deve ser fornecido")
            .Length(2, 50).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(c => c.Cidade)
            .NotEmpty().WithMessage("O campo {PropertyName} deve ser fornecido")
            .Length(2, 50).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(c => c.Estado)
            .NotEmpty().WithMessage("O campo {PropertyName} deve ser fornecido")
            .Must(EstadoValidation.ValidarEstado)
            .WithMessage("O campo {PropertyName} fornecido não é um estado válido");

        RuleFor(c => c.CEP)
            .NotEmpty().WithMessage("O campo {PropertyName} deve ser fornecido")
            .Matches("^[0-9]*$").WithMessage("O campo {PropertyName} deve conter apenas números.")
            .Length(8).WithMessage("O campo {PropertyName} deve ter exatamente {TotalLength} caracteres");
    }
}