using EntregaSegura.Application.Interfaces;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces.Repositories;
using EntregaSegura.Domain.Validators;
using EntregaSegura.Infrastructure.UnitOfWork;

namespace EntregaSegura.Application.Services;

public class CondominioService : BaseService, ICondominioService
{
    private readonly ICondominioRepository _condominioRepository;

    public CondominioService(IUnitOfWork unitOfWork, ICondominioRepository condominioRepository) : base(unitOfWork)
    {
        _condominioRepository = condominioRepository;
    }

    public async Task Adicionar(Condominio condominio)
    {
        if(!ExecutarValidacao(new CondominioValidator(), condominio)) return;

        _condominioRepository.Adicionar(condominio);
        await CommitAsync();
    }

    public async Task Atualizar(Condominio condominio)
    {
        if(!ExecutarValidacao(new CondominioValidator(), condominio)) return;

        _condominioRepository.Atualizar(condominio);
        await CommitAsync();
    }

    public async Task Remover(Guid id)
    {
        _condominioRepository.Remover(id);
        await CommitAsync();
    }
}
