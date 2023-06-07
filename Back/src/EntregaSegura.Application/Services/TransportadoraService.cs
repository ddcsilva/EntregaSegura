using AutoMapper;
using EntregaSegura.Application.DTOs;
using EntregaSegura.Application.Interfaces;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces;
using EntregaSegura.Domain.Validations;
using EntregaSegura.Infra.Data.UnitOfWork;

namespace EntregaSegura.Application.Services;

public class TransportadoraService : BaseService, ITransportadoraService
{
    private readonly ITransportadoraRepository _transportadoraRepository;
    private readonly IMapper _mapper;

    public TransportadoraService(ITransportadoraRepository transportadoraRepository,
                                 IUnitOfWork unitOfWork,
                                 IMapper mapper,
                                 INotificadorErros notificadorErros) : base(unitOfWork, notificadorErros)
    {
        _transportadoraRepository = transportadoraRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TransportadoraDTO>> ObterTodasTransportadorasAsync()
    {
        var transportadoras = await _transportadoraRepository.ObterTodasTransportadorasAsync();
        return _mapper.Map<IEnumerable<TransportadoraDTO>>(transportadoras);
    }

    public async Task<TransportadoraDTO> ObterTransportadoraPorIdAsync(int id)
    {
        var transportadora = await _transportadoraRepository.ObterTransportadoraPorIdAsync(id);
        return _mapper.Map<TransportadoraDTO>(transportadora);
    }

    public async Task AdicionarAsync(TransportadoraDTO transportadoraDTO)
    {
        var transportadora = _mapper.Map<Transportadora>(transportadoraDTO);

        if (!ExecutarValidacao(new TransportadoraValidator(), transportadora))
        {
            return;
        }

        var transportadoraExistente = !string.IsNullOrEmpty(transportadora.Cnpj) && _transportadoraRepository.BuscarAsync(c => c.Cnpj == transportadora.Cnpj).Result.Any();
        if (transportadoraExistente)
        {
            Notificar("Já existe uma transportadora com este CNPJ.");
        }

        var transportadoraMesmoNome = !string.IsNullOrEmpty(transportadora.Nome) && _transportadoraRepository.BuscarAsync(c => c.Nome == transportadora.Nome).Result.Any();
        if (transportadoraMesmoNome)
        {
            Notificar("Já existe uma transportadora com este nome.");
        }

        var transportadoraMesmoEmail = !string.IsNullOrEmpty(transportadora.Email) && _transportadoraRepository.BuscarAsync(c => c.Email == transportadora.Email).Result.Any();
        if (transportadoraMesmoEmail)
        {
            Notificar("Já existe uma transportadora com este e-mail.");
        }

        if (TemNotificacoes())
        {
            return;
        }

        _transportadoraRepository.Adicionar(transportadora);
        var resultadoOperacao = await _unitOfWork.CommitAsync();

        if (resultadoOperacao == 0)
        {
            Notificar("Ocorreu um erro ao salvar a transportadora.");
            return;
        }

        transportadoraDTO.Id = transportadora.Id;
    }

    public async Task AtualizarAsync(TransportadoraDTO transportadoraDTO)
    {
        var transportadora = _mapper.Map<Transportadora>(transportadoraDTO);

        if (!ExecutarValidacao(new TransportadoraValidator(), transportadora))
        {
            return;
        }
            
        var transportadoraExistente = _transportadoraRepository.BuscarAsync(c => c.Cnpj == transportadora.Cnpj && c.Id != transportadora.Id).Result.Any();
        if (transportadoraExistente)
        {
            Notificar("Já existe uma transportadora com este CNPJ.");
        }

        var transportadoraMesmoNome = _transportadoraRepository.BuscarAsync(c => c.Nome == transportadora.Nome && c.Id != transportadora.Id).Result.Any();
        if (transportadoraMesmoNome)
        {
            Notificar("Já existe uma transportadora com este nome.");
        }

        var transportadoraMesmoEmail = _transportadoraRepository.BuscarAsync(c => c.Email == transportadora.Email && c.Id != transportadora.Id).Result.Any();
        if (transportadoraMesmoEmail)
        {
            Notificar("Já existe uma transportadora com este e-mail.");
        }

        if (TemNotificacoes())
        {
            return;
        }

        _transportadoraRepository.Atualizar(transportadora);
        var resultadoOperacao = await CommitAsync();

        if (resultadoOperacao == 0)
        {
            Notificar("Ocorreu um erro ao atualizar a transportadora.");
        }
    }

    public async Task RemoverAsync(int id)
    {
        var transportadora = _transportadoraRepository.ObterTransportadoraPorIdAsync(id).Result;

        if (transportadora == null)
        {
            Notificar("Transportadora não encontrada.");
            return;
        }

        _transportadoraRepository.Remover(transportadora);

        try
        {
            var resultadoOperacao = await CommitAsync();

            if (resultadoOperacao == 0)
            {
                Notificar("Ocorreu um erro ao remover a transportadora.");
            }
        }
        catch (Exception)
        {
            Notificar("Ocorreu um erro inesperado. Favor, contate o administrador.");
        }
    }

    public void Dispose()
    {
        _unitOfWork?.Dispose();
    }
}