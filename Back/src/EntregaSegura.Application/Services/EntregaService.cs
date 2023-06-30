using AutoMapper;
using EntregaSegura.Application.DTOs;
using EntregaSegura.Application.Interfaces;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces;
using EntregaSegura.Domain.Validations;

namespace EntregaSegura.Application.Services;

public class EntregaService : BaseService, IEntregaService
{
    private readonly IEntregaRepository _entregaRepository;
    private readonly IFuncionarioRepository _funcionarioRepository;
    private readonly IMoradorRepository _moradorRepository;
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;

    public EntregaService(IEntregaRepository entregaRepository,
                          IFuncionarioRepository funcionarioRepository,
                          IMoradorRepository moradorRepository,
                          IEmailService emailService,
                          IMapper mapper,
                          INotificadorErros notificadorErros) : base(notificadorErros)
    {
        _entregaRepository = entregaRepository;
        _funcionarioRepository = funcionarioRepository;
        _moradorRepository = moradorRepository;
        _emailService = emailService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EntregaDTO>> ObterTodasEntregasAsync()
    {
        var entregas = await _entregaRepository.BuscarTodosAsync();
        return _mapper.Map<IEnumerable<EntregaDTO>>(entregas);
    }

    public async Task<IEnumerable<EntregaDTO>> ObterTodasEntregasComMoradoresEUnidadesEFuncionariosETransportadorasAsync()
    {
        var entregas = await _entregaRepository.ObterTodasEntregasComMoradoresEUnidadesEFuncionariosETransportadorasAsync();
        return _mapper.Map<IEnumerable<EntregaDTO>>(entregas);
    }

    public async Task<EntregaDTO> ObterEntregaPorIdAsync(int id, bool rastrearAlteracoes = false)
    {
        var entrega = await _entregaRepository.BuscarPorIdAsync(id, rastrearAlteracoes);
        return _mapper.Map<EntregaDTO>(entrega);
    }

    public async Task<bool> AdicionarAsync(EntregaDTO entregaDTO)
    {
        var entrega = _mapper.Map<Entrega>(entregaDTO);
        
        if (!ValidarEntrega(entrega)) return false;

        _entregaRepository.Adicionar(entrega);

        var adicionadoComSucesso = await _entregaRepository.SalvarAlteracoesAsync();

        if (!adicionadoComSucesso)
        {
            Notificar("Ocorreu um erro ao adicionar a entrega.");
            return false;
        }

        return true;
    }

    public async Task<bool> AtualizarAsync(EntregaDTO entregaDTO)
    {
        var entrega = _mapper.Map<Entrega>(entregaDTO);

        if (!ValidarEntrega(entrega, ehAtualizacao: true)) return false;

        _entregaRepository.Atualizar(entrega);

        var atualizadoComSucesso = await _entregaRepository.SalvarAlteracoesAsync();

        if (!atualizadoComSucesso)
        {
            Notificar("Ocorreu um erro ao atualizar a entrega.");
            return false;
        }

        return true;
    }

    public async Task<bool> RemoverAsync(int id)
    {
        var entrega = await _entregaRepository.BuscarPorIdAsync(id);

        if (entrega == null)
        {
            Notificar("Não foi possível encontrar a entrega informada.");
            return false;
        }

        if (await TemAssociacoes(id))
        {
            Notificar("Este condomínio não pode ser removido pois existem registros associados a ele.");
            return false;
        }

        _entregaRepository.Remover(entrega);

        var removidoComSucesso = await _entregaRepository.SalvarAlteracoesAsync();

        if (!removidoComSucesso)
        {
            Notificar("Ocorreu um erro ao remover a entrega.");
            return false;
        }

        return true;
    }

    public void Dispose()
    {
        _entregaRepository?.Dispose();
        _funcionarioRepository?.Dispose();
        _moradorRepository?.Dispose();
    }

    public Task<IEnumerable<Entrega>> ObterEntregasComStatusAguardandoRetiradaPorMoradorAsync(int moradorId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Entrega>> ObterEntregasComStatusRetiradaPorMoradorAsync(int moradorId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Entrega>> ObterEntregasPorFuncionarioAsync(int funcionarioId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Entrega>> ObterEntregasPorMoradorAsync(int moradorId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Entrega>> ObterEntregasPorTransportadoraAsync(int transportadoraId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Entrega>> ObterEntregasRecebidasPorFuncionarioAsync(int funcionarioId)
    {
        throw new NotImplementedException();
    }

    private bool ValidarEntrega(Entrega entrega, bool ehAtualizacao = false)
    {
        if (!ExecutarValidacao(new EntregaValidator(), entrega)) return false;

        return true;
    }

    private async Task<bool> TemAssociacoes(int condominioId)
    {
        var temMoradores = await _moradorRepository.BuscarPorCondicaoAsync(m => m.Id == condominioId);
        var temFuncionarios = await _funcionarioRepository.BuscarPorCondicaoAsync(f => f.Id == condominioId);

        return temMoradores.Any() || temFuncionarios.Any();
    }
}