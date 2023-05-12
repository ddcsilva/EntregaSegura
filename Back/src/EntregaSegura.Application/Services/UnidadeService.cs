using EntregaSegura.Application.Interfaces;
using EntregaSegura.Application.Notifications;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces.Repositories;
using EntregaSegura.Domain.Validators;
using EntregaSegura.Infrastructure.UnitOfWork;

namespace EntregaSegura.Application.Services;

public class UnidadeService : BaseService, IUnidadeService
{
    private readonly IUnidadeRepository _unidadeRepository;

    public UnidadeService(IUnidadeRepository unidadeRepository,
                          IUnitOfWork unitOfWork,
                          NotificadorErros notificadorErros) : base(unitOfWork, notificadorErros)
    {
        _unidadeRepository = unidadeRepository;
    }

    public async Task Adicionar(Unidade unidade)
    {
        if (!ExecutarValidacao(new UnidadeValidator(), unidade)) return;

        if (_unidadeRepository.BuscarAsync(u => u.Numero == unidade.Numero).Result.Any())
        {
            Notificar("Já existe uma unidade com este número.");
            return;
        }

        _unidadeRepository.Adicionar(unidade);
        await CommitAsync();
    }

    public async Task Atualizar(Unidade unidade)
    {
        if (!ExecutarValidacao(new UnidadeValidator(), unidade)) return;

        if (_unidadeRepository.BuscarAsync(u => u.Numero == unidade.Numero && u.Id != unidade.Id).Result.Any())
        {
            Notificar("Já existe uma unidade com este número.");
            return;
        }

        _unidadeRepository.Atualizar(unidade);
        await CommitAsync();
    }

    public async Task Remover(Guid id)
    {
        var unidade = await _unidadeRepository.ObterUnidadeComMoradoresAsync(id);

        if (unidade.Moradores.Any())
        {
            Notificar("A unidade possui moradores cadastrados.");
            return;
        }

        _unidadeRepository.Remover(id);
        await CommitAsync();
    }

    public async Task<IEnumerable<Unidade>> ObterTodosAsync()
    {
        return await _unidadeRepository.ObterTodosAsync();
    }

    public async Task<Unidade> ObterPorIdAsync(Guid id)
    {
        return await _unidadeRepository.ObterPorIdAsync(id);
    }

    public async Task<Unidade> ObterPorUnidadePorCondominioBlocoNumeroAsync(Guid condominioId, string bloco, string numero)
    {
        return await _unidadeRepository.ObterPorUnidadePorCondominioBlocoNumeroAsync(condominioId, bloco, numero);
    }

    public async Task<Unidade> ObterUnidadeComMoradoresPorCondominioBlocoNumeroAsync(Guid condominioId, string bloco, string numero)
    {
        return await _unidadeRepository.ObterUnidadeComMoradoresPorCondominioBlocoNumeroAsync(condominioId, bloco, numero);
    }

    public async Task<Unidade> ObterUnidadeComEntregasPorCondominioBlocoNumeroAsync(Guid condominioId, string bloco, string numero)
    {
        return await _unidadeRepository.ObterUnidadeComEntregasPorCondominioBlocoNumeroAsync(condominioId, bloco, numero);
    }

    public void Dispose()
    {
        _unitOfWork?.Dispose();
    }
}