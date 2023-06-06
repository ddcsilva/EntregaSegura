
using FluentValidation;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Validations.Helpers;

namespace EntregaSegura.Domain.Validations;

public class CondominioValidator : AbstractValidator<Condominio>
{
    public CondominioValidator()
    {
        RuleFor(c => c.Nome)
            .NotEmpty().WithMessage("O campo {PropertyName} deve ser fornecido")
            .Length(2, 100).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(c => c.Cnpj)
            .NotEmpty().WithMessage("O campo {PropertyName} deve ser fornecido")
            .Must(CNPJValidation.ValidarCNPJ).WithMessage("O campo {PropertyName} fornecido é inválido");

        RuleFor(c => c.Telefone)
            .NotEmpty().WithMessage("O campo {PropertyName} deve ser fornecido")
            .Must(TelefoneValidation.ValidarTelefone).WithMessage("O campo {PropertyName} fornecido é inválido");

        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("O campo {PropertyName} deve ser fornecido")
            .MaximumLength(100).WithMessage("O campo {PropertyName} deve ter no máximo {MaxLength} caracteres")
            .EmailAddress().WithMessage("O campo {PropertyName} fornecido é inválido");

        RuleFor(c => c.Cep)
            .NotEmpty().WithMessage("O campo {PropertyName} deve ser fornecido")
            .Matches("^[0-9]{8}$").WithMessage("O campo {PropertyName} deve estar no formato correto: 00000-000");

        RuleFor(c => c.Logradouro)
            .NotEmpty().WithMessage("O campo {PropertyName} deve ser fornecido")
            .Length(2, 100).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(c => c.Numero)
            .GreaterThan(0).WithMessage("O campo {PropertyName} deve ser maior do que zero")
            .LessThanOrEqualTo(9999).WithMessage("O campo {PropertyName} deve ser menor ou igual a 9999");

        RuleFor(c => c.Bairro)
            .NotEmpty().WithMessage("O campo {PropertyName} deve ser fornecido")
            .Length(2, 50).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(c => c.Cidade)
            .NotEmpty().WithMessage("O campo {PropertyName} deve ser fornecido")
            .Length(2, 50).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(c => c.Estado)
            .NotEmpty().WithMessage("O campo {PropertyName} deve ser fornecido")
            .Must(EstadoValidation.ValidarEstado).WithMessage("O campo {PropertyName} fornecido não é um estado válido");

        RuleFor(c => c.QuantidadeBlocos)
            .InclusiveBetween(1, 20).WithMessage("O campo {PropertyName} deve estar entre {MinValue} e {MaxValue}");

        RuleFor(c => c.QuantidadeUnidades)
            .InclusiveBetween(1, 8).WithMessage("O campo {PropertyName} deve estar entre {MinValue} e {MaxValue}");

        RuleFor(c => c.QuantidadeAndares)
            .InclusiveBetween(1, 40).WithMessage("O campo {PropertyName} deve estar entre {MinValue} e {MaxValue}");
    }
}