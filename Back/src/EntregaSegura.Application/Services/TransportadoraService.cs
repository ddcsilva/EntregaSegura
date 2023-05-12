using EntregaSegura.Application.Interfaces;
using EntregaSegura.Application.Notifications;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces.Repositories;
using EntregaSegura.Domain.Validators;
using EntregaSegura.Infrastructure.UnitOfWork;

namespace EntregaSegura.Application.Services;

public class TransportadoraService : BaseService, ITransportadoraService
{
    private readonly ITransportadoraRepository _transportadoraRepository;

    public TransportadoraService(ITransportadoraRepository transportadoraRepository,
                                 IUnitOfWork unitOfWork,
                                 NotificadorErros notificadorErros) : base(unitOfWork, notificadorErros)
    {
        _transportadoraRepository = transportadoraRepository;
    }

    public async Task Adicionar(Transportadora transportadora)
    {
        if(!ExecutarValidacao(new TransportadoraValidator(), transportadora)) return;

        if(_transportadoraRepository.BuscarAsync(c => c.CNPJ == transportadora.CNPJ).Result.Any())
        {
            Notificar("Já existe uma transportadora com este CNPJ.");
            return;
        }

        if(_transportadoraRepository.BuscarAsync(c => c.Nome == transportadora.Nome).Result.Any())
        {
            Notificar("Já existe uma transportadora com este nome.");
            return;
        }

        _transportadoraRepository.Adicionar(transportadora);
        await CommitAsync();
    }

    public async Task Atualizar(Transportadora transportadora)
    {
        if(!ExecutarValidacao(new TransportadoraValidator(), transportadora)) return;

        if(_transportadoraRepository.BuscarAsync(c => c.CNPJ == transportadora.CNPJ && c.Id != transportadora.Id).Result.Any())
        {
            Notificar("Já existe uma transportadora com este CNPJ.");
            return;
        }

        _transportadoraRepository.Atualizar(transportadora);
        await CommitAsync();
    }

    public async Task Remover(Guid id)
    {
        if(_transportadoraRepository.ObterTransportadoraComEntregasAsync(id).Result.Entregas.Any())
        {
            Notificar("A transportadora possui entregas cadastradas!");
            return;
        }

        _transportadoraRepository.Remover(id);
        await CommitAsync();
    }

    public async Task<IEnumerable<Transportadora>> ObterTodosAsync()
    {
        return await _transportadoraRepository.ObterTodosAsync();
    }

    public async Task<Transportadora> ObterPorIdAsync(Guid id)
    {
        return await _transportadoraRepository.ObterPorIdAsync(id);
    }

    public async Task<Transportadora> ObterPorNomeAsync(string nome)
    {
        return await _transportadoraRepository.ObterPorNomeAsync(nome);
    }

    public async Task<Transportadora> ObterTransportadoraComEntregasAsync(Guid transportadoraId)
    {
        return await _transportadoraRepository.ObterTransportadoraComEntregasAsync(transportadoraId);
    }    

    public void Dispose()
    {
        _unitOfWork?.Dispose();
    }
}