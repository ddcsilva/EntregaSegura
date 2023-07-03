
using FluentValidation;
using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Domain.Validations;

public class UsuarioValidator : AbstractValidator<Usuario>
{
    public UsuarioValidator()
    {
        RuleFor(u => u.Login)
            .NotEmpty().WithMessage("O campo {PropertyName} deve ser fornecido")
            .MaximumLength(100).WithMessage("O campo {PropertyName} deve ter no máximo {MaxLength} caracteres")
            .EmailAddress().WithMessage("O campo {PropertyName} fornecido é inválido");

        RuleFor(u => u.Senha)
            .NotEmpty().WithMessage("O campo {PropertyName} deve ser fornecido")
            .Length(2, 50).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");
    }
}