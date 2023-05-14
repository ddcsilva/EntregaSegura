using EntregaSegura.Application.Interfaces;
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
                          INotificadorErros notificadorErros) : base(unitOfWork, notificadorErros)
    {
        _unidadeRepository = unidadeRepository;
    }

    public async Task<Unidade> Adicionar(Unidade unidade)
    {
        if (!ExecutarValidacao(new UnidadeValidator(), unidade)) return null;

        if (_unidadeRepository.BuscarAsync(u => u.Numero == unidade.Numero).Result.Any())
        {
            Notificar("Já existe uma unidade com este número.");
            return null;
        }

        await _unidadeRepository.AdicionarAsync(unidade);
        await CommitAsync();

        return unidade;
    }

    public async Task<Unidade> Atualizar(Unidade unidade)
    {
        if (!ExecutarValidacao(new UnidadeValidator(), unidade)) return null;

        if (_unidadeRepository.BuscarAsync(u => u.Numero == unidade.Numero && u.Id != unidade.Id).Result.Any())
        {
            Notificar("Já existe uma unidade com este número.");
            return null;
        }

        _unidadeRepository.Atualizar(unidade);
        var result = await CommitAsync();

        if (result == 0)
        {
            Notificar("Ocorreu um erro ao salvar a unidade.");
            return null;
        }

        return unidade;
    }

    public async Task<bool> Remover(Guid id)
    {
        var unidade = await _unidadeRepository.ObterPorIdAsync(id);

        if (unidade == null)
        {
            Notificar("Unidade não encontrada.");
            return false;
        }

        _unidadeRepository.Remover(unidade);
        var result = await CommitAsync();

        if (result == 0)
        {
            Notificar("Ocorreu um erro ao remover a unidade.");
            return false;
        }

        return true;
    }

    public async Task<IEnumerable<Unidade>> ObterTodosAsync()
    {
        return await _unidadeRepository.ObterTodosAsync();
    }

    public async Task<Unidade> ObterPorIdAsync(Guid id)
    {
        return await _unidadeRepository.ObterPorIdAsync(id);
    }

    public async Task<IEnumerable<Unidade>> ObterUnidadesComCondominioAsync()
    {
        return await _unidadeRepository.ObterUnidadesComCondominioAsync();
    }

    public async Task<Unidade> ObterUnidadePorIdComCondominioEMoradoresAsync(Guid id)
    {
        return await _unidadeRepository.ObterUnidadePorIdComCondominioEMoradoresAsync(id);
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