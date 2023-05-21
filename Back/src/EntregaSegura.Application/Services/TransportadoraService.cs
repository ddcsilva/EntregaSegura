using EntregaSegura.Application.Interfaces;
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
                                 INotificadorErros notificadorErros) : base(unitOfWork, notificadorErros)
    {
        _transportadoraRepository = transportadoraRepository;
    }

    public async Task<Transportadora> Adicionar(Transportadora transportadora)
    {
        if(!ExecutarValidacao(new TransportadoraValidator(), transportadora)) return null;

        if(_transportadoraRepository.BuscarAsync(c => c.CNPJ == transportadora.CNPJ).Result.Any())
        {
            Notificar("Já existe uma transportadora com este CNPJ.");
            return null;
        }

        if(_transportadoraRepository.BuscarAsync(c => c.Nome == transportadora.Nome).Result.Any())
        {
            Notificar("Já existe uma transportadora com este nome.");
            return null;
        }

        if(_transportadoraRepository.BuscarAsync(c => c.Email == transportadora.Email).Result.Any())
        {
            Notificar("Já existe uma transportadora com este e-mail.");
            return null;
        }

        await _transportadoraRepository.AdicionarAsync(transportadora);
        var result = await CommitAsync();

        if (result == 0)
        {
            Notificar("Ocorreu um erro ao salvar a transportadora.");
            return null;
        }

        return transportadora;
    }

    public async Task<Transportadora> Atualizar(Transportadora transportadora)
    {
        if(!ExecutarValidacao(new TransportadoraValidator(), transportadora)) return null;

        if(_transportadoraRepository.BuscarAsync(c => c.CNPJ == transportadora.CNPJ && c.Id != transportadora.Id).Result.Any())
        {
            Notificar("Já existe uma transportadora com este CNPJ.");
            return null;
        }

        if(_transportadoraRepository.BuscarAsync(c => c.Nome == transportadora.Nome && c.Id != transportadora.Id).Result.Any())
        {
            Notificar("Já existe uma transportadora com este nome.");
            return null;
        }

        if(_transportadoraRepository.BuscarAsync(c => c.Email == transportadora.Email && c.Id != transportadora.Id).Result.Any())
        {
            Notificar("Já existe uma transportadora com este e-mail.");
            return null;
        }

        _transportadoraRepository.Atualizar(transportadora);
        var result = await CommitAsync();

        if (result == 0)
        {
            Notificar("Ocorreu um erro ao atualizar a transportadora.");
            return null;
        }

        return transportadora;
    }

    public async Task<bool> Remover(int id)
    {
        var transportadora = await _transportadoraRepository.ObterPorIdAsync(id);

        if (transportadora == null)
        {
            Notificar("Transportadora não encontrada.");
            return false;
        }

        _transportadoraRepository.Remover(transportadora);
        var result = await CommitAsync();

        if (result == 0)
        {
            Notificar("Ocorreu um erro ao remover a transportadora.");
            return false;
        }

        return true;
    }

    public async Task<IEnumerable<Transportadora>> ObterTodosAsync()
    {
        return await _transportadoraRepository.ObterTodosAsync();
    }

    public async Task<Transportadora> ObterPorIdAsync(int id)
    {
        return await _transportadoraRepository.ObterPorIdAsync(id);
    }

    public async Task<Transportadora> ObterPorNomeAsync(string nome)
    {
        return await _transportadoraRepository.ObterPorNomeAsync(nome);
    }

    public async Task<Transportadora> ObterTransportadoraComEntregasAsync(int transportadoraId)
    {
        return await _transportadoraRepository.ObterTransportadoraComEntregasAsync(transportadoraId);
    }    

    public void Dispose()
    {
        _unitOfWork?.Dispose();
    }
}