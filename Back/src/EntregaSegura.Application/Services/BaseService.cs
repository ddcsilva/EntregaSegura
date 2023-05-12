using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Models;
using EntregaSegura.Infrastructure.UnitOfWork;
using FluentValidation;
using FluentValidation.Results;

namespace EntregaSegura.Application.Services;

public abstract class BaseService
{
    protected readonly IUnitOfWork _unitOfWork;
    private readonly INotificadorErros _notificadorErros;

    protected BaseService(IUnitOfWork unitOfWork, INotificadorErros notificadorErros)
    {
        _unitOfWork = unitOfWork;
        _notificadorErros = notificadorErros;
    }

    protected void Notificar(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            Notificar(error.ErrorMessage);
        }
    }

    protected void Notificar(string mensagem)
    {
        _notificadorErros.Handle(new NotificacaoErros(mensagem));
    }

    protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : BaseEntity
    {
        var validator = validacao.Validate(entidade);

        if (validator.IsValid) return true;

        Notificar(validator);

        return false;
    }

    public async Task CommitAsync()
    {
        await _unitOfWork.CommitAsync();
    }
}