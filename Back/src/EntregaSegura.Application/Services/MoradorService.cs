using EntregaSegura.Application.Interfaces;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces.Repositories;
using EntregaSegura.Domain.Validators;
using EntregaSegura.Infrastructure.UnitOfWork;

namespace EntregaSegura.Application.Services;

public class MoradorService : BaseService, IMoradorService
{
    private readonly IMoradorRepository _moradorRepository;

    public MoradorService(IMoradorRepository moradorRepository,
                          IUnitOfWork unitOfWork,
                          INotificadorErros notificadorErros) : base(unitOfWork, notificadorErros)
    {
        _moradorRepository = moradorRepository;
    }

    public async Task Adicionar(Morador morador)
    {
        if (!ExecutarValidacao(new MoradorValidator(), morador)) return;

        if (_moradorRepository.BuscarAsync(m => m.CPF == morador.CPF).Result.Any())
        {
            Notificar("Já existe um morador com este CPF.");
            return;
        }

        if (_moradorRepository.BuscarAsync(m => m.Nome == morador.Nome).Result.Any())
        {
            Notificar("Já existe um morador com este nome.");
            return;
        }

        await _moradorRepository.AdicionarAsync(morador);
        await CommitAsync();
    }

    public async Task Atualizar(Morador morador)
    {
        if (!ExecutarValidacao(new MoradorValidator(), morador)) return;

        if (_moradorRepository.BuscarAsync(m => m.CPF == morador.CPF && m.Id != morador.Id).Result.Any())
        {
            Notificar("Já existe um morador com este CPF.");
            return;
        }

        _moradorRepository.Atualizar(morador);
        await CommitAsync();
    }

    public async Task Remover(Guid id)
    {
        if (_moradorRepository.ObterMoradorComEntregasAsync(id).Result.Entregas.Any())
        {
            Notificar("O morador possui entregas cadastradas!");
            return;
        }

        await _moradorRepository.Remover(id);
        await CommitAsync();
    }

    public async Task<IEnumerable<Morador>> ObterTodosAsync()
    {
        return await _moradorRepository.ObterTodosAsync();
    }

    public async Task<Morador> ObterPorIdAsync(Guid id)
    {
        return await _moradorRepository.ObterPorIdAsync(id);
    }

    public async Task<Morador> ObterPorNomeAsync(string nome)
    {
        return await _moradorRepository.ObterPorNomeAsync(nome);
    }

    public async Task<Morador> ObterMoradorComEntregasAsync(Guid moradorId)
    {
        return await _moradorRepository.ObterMoradorComEntregasAsync(moradorId);
    }

    public async Task<IEnumerable<Morador>> ObterMoradoresPorUnidadeAsync(Guid unidadeId)
    {
        return await _moradorRepository.ObterMoradoresPorUnidadeAsync(unidadeId);
    }

    public async Task<Morador> ObterMoradorComUnidadeAsync(Guid moradorId)
    {
        return await _moradorRepository.ObterMoradorComUnidadeAsync(moradorId);
    }

    public void Dispose()
    {
        _unitOfWork?.Dispose();
    }
}