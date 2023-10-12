using AutoMapper;
using EntregaSegura.Application.DTOs;
using EntregaSegura.Application.Interfaces;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Entities.Enums;
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

    public async Task<IEnumerable<EntregaDTO>> ObterTodasEntregasComMoradoresEUnidadesEFuncionariosETransportadorasAsync(string emailUsuario = null, string perfilUsuario = null)
    {
        var entregas = await _entregaRepository.ObterTodasEntregasComMoradoresEUnidadesEFuncionariosETransportadorasAsync(emailUsuario, perfilUsuario);
        return _mapper.Map<IEnumerable<EntregaDTO>>(entregas);
    }

    public async Task<EntregaDTO> ObterEntregaPorIdAsync(int id, bool rastrearAlteracoes = false)
    {
        var entrega = await _entregaRepository.BuscarPorIdAsync(id, rastrearAlteracoes);
        return _mapper.Map<EntregaDTO>(entrega);
    }

    public async Task<EntregaDTO> ObterEntregaComMoradorEUnidadeEFuncionarioETransportadoraPorIdAsync(int id, bool rastrearAlteracoes = false)
    {
        var entrega = await _entregaRepository.ObterEntregaComMoradorEUnidadeEFuncionarioETransportadoraPorIdAsync(id, rastrearAlteracoes);
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

    public async Task<bool> ConfirmarRetiradaAsync(int id)
    {
        var entrega = await _entregaRepository.BuscarPorIdAsync(id);

        if (entrega == null)
        {
            Notificar("Não foi possível encontrar a entrega informada.");
            return false;
        }

        entrega.AtualizarParaRetirada();

        _entregaRepository.Atualizar(entrega);

        var atualizadoComSucesso = await _entregaRepository.SalvarAlteracoesAsync();

        if (!atualizadoComSucesso)
        {
            Notificar("Ocorreu um erro ao confirmar a retirada da entrega.");
            return false;
        }

        return true;
    }

    public async Task<bool> NotificarEntregaAsync(int id)
    {
        var entrega = await _entregaRepository.ObterEntregaComMoradorEUnidadeEFuncionarioETransportadoraPorIdAsync(id);

        if (entrega == null)
        {
            Notificar("Não foi possível encontrar a entrega informada.");
            return false;
        }

        var morador = await _moradorRepository.ObterMoradorPorIdComUnidadeECondominioAsync(entrega.MoradorId);

        if (morador == null)
        {
            Notificar("Não foi possível encontrar o morador associado a entrega informada.");
            return false;
        }

        (string assuntoEmail, string mensagemEmail) = ConstruirEmail(entrega);

        var emailEnviadoComSucesso = await _emailService.EnviarEmailAsync(morador.Pessoa.Email, assuntoEmail, mensagemEmail);

        if (!emailEnviadoComSucesso)
        {
            Notificar("Ocorreu um erro ao enviar o e-mail de notificação da entrega.");
            return false;
        }

        entrega.AtualizarParaNotificada();

        _entregaRepository.Atualizar(entrega);

        var atualizadoComSucesso = await _entregaRepository.SalvarAlteracoesAsync();

        if (!atualizadoComSucesso)
        {
            Notificar("Ocorreu um erro ao notificar a entrega.");
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

        if (PossuiRestricoes(entrega))
        {
            Notificar("Não é possível remover a entrega pois já foi notificada ou retirada.");
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

    private (string, string) ConstruirEmail(Entrega entrega)
    {
        string assuntoEmail = "Entrega pendente de retirada";
        string mensagemEmail = $"Olá {entrega.Morador.Pessoa.Nome},\n\n" +
                               $"Você possui uma entrega pendente de retirada!\n\n" +
                               $"A entrega foi realizada pela transportadora {entrega.Transportadora.Nome} no dia {entrega.DataRecebimento} \n\n" +
                               $"Pedimos, por gentileza, que retire sua entrega o quanto antes.\n\n" +
                               $"Atenciosamente,\n" +
                               $"Equipe Entrega Segura";

        return (assuntoEmail, mensagemEmail);
    }

    private bool ValidarEntrega(Entrega entrega, bool ehAtualizacao = false)
    {
        if (!ExecutarValidacao(new EntregaValidator(), entrega)) return false;

        if (entrega.DataRecebimento.Date > DateTime.Today.Date && ehAtualizacao)
        {
            Notificar("A data de recebimento não pode ser maior que a data atual.");
            return false;
        }

        return true;
    }

    private bool PossuiRestricoes(Entrega entrega)
    {
        if (entrega.Status != StatusEntrega.Recebida || entrega.Status != StatusEntrega.Notificada)
        {
            return true;
        }

        return false;
    }
}