using AutoMapper;
using EntregaSegura.Application.DTOs;
using EntregaSegura.Application.Interfaces;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces;
using EntregaSegura.Domain.Validations;

namespace EntregaSegura.Application.Services;

public class MoradorService : BaseService, IMoradorService
{
    private readonly IMoradorRepository _moradorRepository;
    private readonly IEntregaRepository _entregaRepository;
    private readonly IUsuarioService _usuarioService;
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;

    public MoradorService(IMoradorRepository moradorRepository,
                          IEntregaRepository entregaRepository,
                          IUsuarioService usuarioService,
                          IEmailService emailService,
                          IMapper mapper,
                          INotificadorErros notificadorErros) : base(notificadorErros)
    {
        _moradorRepository = moradorRepository;
        _entregaRepository = entregaRepository;
        _usuarioService = usuarioService;
        _emailService = emailService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MoradorDTO>> ObterTodosMoradoresAsync()
    {
        var moradores = await _moradorRepository.BuscarTodosAsync();
        return _mapper.Map<IEnumerable<MoradorDTO>>(moradores);
    }

    public async Task<IEnumerable<MoradorDTO>> ObterTodosMoradoresComUnidadeECondominioAsync()
    {
        var moradores = await _moradorRepository.ObterTodosMoradoresComUnidadeECondominioAsync();
        return _mapper.Map<IEnumerable<MoradorDTO>>(moradores);
    }

    public async Task<MoradorDTO> ObterMoradorPorIdAsync(int id, bool rastrearAlteracoes = false)
    {
        var morador = await _moradorRepository.BuscarPorIdAsync(id, rastrearAlteracoes);
        return _mapper.Map<MoradorDTO>(morador);
    }

    public async Task<MoradorDTO> ObterMoradorPorIdComUnidadeECondominioAsync(int id)
    {
        var morador = await _moradorRepository.ObterMoradorPorIdComUnidadeECondominioAsync(id);
        return _mapper.Map<MoradorDTO>(morador);
    }

    public async Task<bool> AdicionarAsync(MoradorDTO moradorDTO)
    {
        var morador = _mapper.Map<Morador>(moradorDTO);

        if (!await ValidarMorador(morador)) return false;

        var usuarioDTO = new UsuarioDTO
        {
            UserName = morador.Email,
            Email = morador.Email,
            Senha = "123456"
        };

        using (_moradorRepository.IniciarTrasacaoAsync())
        {
            var usuarioRegistradoComSucesso = await _usuarioService.CriarContaUsuarioAsync(usuarioDTO, "MORADOR");

            if (usuarioRegistradoComSucesso == null)
            {
                Notificar("Ocorreu um erro ao adicionar o morador.");
                return false;
            }

            morador.DefinirUsuario(usuarioRegistradoComSucesso.Id);

            _moradorRepository.Adicionar(morador);

            var adicionadoComSucesso = await _moradorRepository.SalvarAlteracoesAsync();

            if (!adicionadoComSucesso)
            {
                Notificar("Ocorreu um erro ao adicionar o morador.");
                await _moradorRepository.DescartarTransacaoAsync();
                return false;
            }

            await _moradorRepository.SalvarTransacaoAsync();

            (string assuntoEmail, string mensagemEmail) = ConstruirEmail(morador, usuarioDTO);

            await _emailService.EnviarEmailAsync(usuarioDTO.Email, assuntoEmail, mensagemEmail);
        }

        moradorDTO.Id = morador.Id;
        moradorDTO.UserId = morador.UserId;

        return true;
    }

    public async Task<bool> AtualizarAsync(MoradorDTO moradorDTO)
    {
        var morador = _mapper.Map<Morador>(moradorDTO);

        if (!await ValidarMorador(morador, ehAtualizacao: true)) return false;

        _moradorRepository.Atualizar(morador);

        var atualizadoComSucesso = await _moradorRepository.SalvarAlteracoesAsync();

        if (!atualizadoComSucesso)
        {
            Notificar("Ocorreu um erro ao atualizar o morador.");
            return false;
        }

        return true;
    }

    public async Task<bool> RemoverAsync(int id)
    {
        var morador = await _moradorRepository.BuscarPorIdAsync(id);

        if (morador == null)
        {
            Notificar("Morador não encontrado.");
            return false;
        }

        if (await TemAssociacoes(id))
        {
            Notificar("Este morador não pode ser removido pois existem registros associados a ele.");
            return false;
        }

        _moradorRepository.Remover(morador);

        var removidoComSucesso = await _moradorRepository.SalvarAlteracoesAsync();

        if (!removidoComSucesso)
        {
            Notificar("Ocorreu um erro ao remover o morador.");
            return false;
        }

        return true;
    }

    public async Task<MoradorDTO> ObterMoradorPeloUsuarioAsync(int usuarioId)
    {
        var morador = await _moradorRepository.BuscarPorCondicaoAsync(m => m.UserId == usuarioId);

        return _mapper.Map<MoradorDTO>(morador.FirstOrDefault());
    }

    public void Dispose()
    {
        _moradorRepository?.Dispose();
    }

    private (string, string) ConstruirEmail(Morador morador, UsuarioDTO usuarioDTO)
    {
        string assuntoEmail = "Bem-vindo ao EntregaSegura!";
        string mensagemEmail = $"Olá {morador.Nome},\n\n" +
                               $"Sua conta no EntregaSegura foi criada com sucesso! " +
                               $"Suas credenciais são:\n\n" +
                               $"Usuário: {usuarioDTO.UserName}\n" +
                               $"Senha: {usuarioDTO.Senha}\n\n" +
                               $"Atenciosamente,\n" +
                               $"Equipe EntregaSegura";
        return (assuntoEmail, mensagemEmail);
    }

    private async Task<bool> ValidarMorador(Morador morador, bool ehAtualizacao = false)
    {
        if (!ExecutarValidacao(new MoradorValidator(), morador)) return false;

        if (!string.IsNullOrWhiteSpace(morador.Cpf)
            && (await _moradorRepository.BuscarPorCondicaoAsync(c => c.Cpf == morador.Cpf && (ehAtualizacao ? c.Id != morador.Id : true))).Any())
        {
            Notificar("Já existe um morador com este CPF informado.");
            return false;
        }

        if (!string.IsNullOrWhiteSpace(morador.Nome)
            && (await _moradorRepository.BuscarPorCondicaoAsync(c => c.Nome == morador.Nome && (ehAtualizacao ? c.Id != morador.Id : true))).Any())
        {
            Notificar("Já existe um morador com este nome informado.");
            return false;
        }

        if (!string.IsNullOrWhiteSpace(morador.Email)
            && (await _moradorRepository.BuscarPorCondicaoAsync(c => c.Email == morador.Email && (ehAtualizacao ? c.Id != morador.Id : true))).Any())
        {
            Notificar("Já existe um morador com este e-mail informado.");
            return false;
        }

        return true;
    }

    private async Task<bool> TemAssociacoes(int moradorId)
    {
        var entregas = await _entregaRepository.BuscarPorCondicaoAsync(e => e.MoradorId == moradorId);

        return entregas.Any();
    }
}