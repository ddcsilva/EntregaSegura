using AutoMapper;
using EntregaSegura.Application.DTOs;
using EntregaSegura.Application.Helpers;
using EntregaSegura.Application.Interfaces;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Entities.Enums;
using EntregaSegura.Domain.Interfaces;
using EntregaSegura.Domain.Validations;

namespace EntregaSegura.Application.Services;

public class MoradorService : BaseService, IMoradorService
{
    private readonly IMoradorRepository _moradorRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IPessoaRepository _pessoaRepository;
    private readonly IUsuarioService _usuarioService;
    private readonly IUnidadeRepository _unidadeRepository;
    private readonly IEntregaRepository _entregaRepository;
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;

    public MoradorService(IMoradorRepository moradorRepository,
                          IUsuarioRepository usuarioRepository,
                          IPessoaRepository pessoaRepository,
                          IUnidadeRepository unidadeRepository,
                          IEntregaRepository entregaRepository,
                          IUsuarioService usuarioService,
                          IEmailService emailService,
                          IMapper mapper,
                          INotificadorErros notificadorErros) : base(notificadorErros)
    {
        _moradorRepository = moradorRepository;
        _usuarioRepository = usuarioRepository;
        _pessoaRepository = pessoaRepository;
        _usuarioService = usuarioService;
        _unidadeRepository = unidadeRepository;
        _entregaRepository = entregaRepository;
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
            Login = morador.Pessoa.Email,
            Senha = Criptografia.CriptografarSenha("123456"),
            Perfil = PerfilUsuario.Morador
        };

        using (_moradorRepository.IniciarTrasacaoAsync())
        {
            _moradorRepository.Adicionar(morador);

            var adicionadoComSucesso = await _moradorRepository.SalvarAlteracoesAsync();

            if (!adicionadoComSucesso)
            {
                Notificar("Ocorreu um erro ao adicionar o morador.");
                await _moradorRepository.DescartarTransacaoAsync();
                return false;
            }

            usuarioDTO.PessoaId = morador.PessoaId;

            var usuarioRegistradoComSucesso = await _usuarioService.CriarContaUsuarioAsync(usuarioDTO);

            if (usuarioRegistradoComSucesso == null)
            {
                Notificar("Ocorreu um erro ao adicionar o morador.");
                await _moradorRepository.DescartarTransacaoAsync();
                return false;
            }

            await _moradorRepository.SalvarTransacaoAsync();
        }

        (string assuntoEmail, string mensagemEmail) = ConstruirEmail(morador);

        var emailEnviadoComSucesso = await _emailService.EnviarEmailAsync(morador.Pessoa.Email, assuntoEmail, mensagemEmail);

        if (!emailEnviadoComSucesso)
        {
            Notificar("Ocorreu um erro ao enviar o e-mail de registro do usuário.");
            return false;
        }

        moradorDTO.Id = morador.Id;

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
        var morador = await _moradorRepository.BuscarPorIdAsync(id, rastrearAlteracoes: true);

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

        var usuario = await _usuarioRepository.ObterUsuarioPorPessoaAsync(morador.PessoaId, rastrearAlteracoes: true);
        var pessoa = await _pessoaRepository.BuscarPorIdAsync(morador.PessoaId, rastrearAlteracoes: true);

        using (_moradorRepository.IniciarTrasacaoAsync())
        {
            _usuarioRepository.Remover(usuario);

            var usuarioRemovidoComSucesso = await _usuarioRepository.SalvarAlteracoesAsync();

            if (!usuarioRemovidoComSucesso)
            {
                Notificar("Ocorreu um erro ao remover o morador.");
                await _moradorRepository.DescartarTransacaoAsync();
                return false;
            }

            _moradorRepository.Remover(morador);

            var moradorRemovidoComSucesso = await _moradorRepository.SalvarAlteracoesAsync();

            if (!moradorRemovidoComSucesso)
            {
                Notificar("Ocorreu um erro ao remover o morador.");
                await _moradorRepository.DescartarTransacaoAsync();
                return false;
            }

            await _moradorRepository.SalvarTransacaoAsync();

            _pessoaRepository.Remover(pessoa);

            var pessoaRemovidaComSucesso = await _pessoaRepository.SalvarAlteracoesAsync();

            if (!pessoaRemovidaComSucesso)
            {
                Notificar("Ocorreu um erro ao remover o morador.");
                await _moradorRepository.DescartarTransacaoAsync();
                return false;
            }
        }

        return true;
    }

    public void Dispose()
    {
        _moradorRepository?.Dispose();
    }

    private (string, string) ConstruirEmail(Morador morador)
    {

        string assuntoEmail = "Bem-vindo ao EntregaSegura!";
        string mensagemEmail = $"Olá {morador.Pessoa.Nome},\n\n" +
                               $"Sua conta no EntregaSegura foi criada com sucesso! " +
                               $"Suas credenciais são:\n\n" +
                               $"Usuário: {morador.Pessoa.Email}\n" +
                               $"Senha: 123456 \n\n" +
                               $"Atenciosamente,\n" +
                               $"Equipe EntregaSegura";

        return (assuntoEmail, mensagemEmail);
    }

    private async Task<bool> ValidarMorador(Morador morador, bool ehAtualizacao = false)
    {
        if (!ExecutarValidacao(new MoradorValidator(), morador) && !ExecutarValidacao(new PessoaValidator(), morador.Pessoa)) return false;

        if (!string.IsNullOrWhiteSpace(morador.Pessoa.Cpf)
            && (await _moradorRepository.BuscarPorCondicaoAsync(c => c.Pessoa.Cpf == morador.Pessoa.Cpf && (ehAtualizacao ? c.Id != morador.Id : true))).Any())
        {
            Notificar("Já existe um morador com este CPF informado.");
            return false;
        }

        if (!string.IsNullOrWhiteSpace(morador.Pessoa.Nome)
            && (await _moradorRepository.BuscarPorCondicaoAsync(c => c.Pessoa.Nome == morador.Pessoa.Nome && (ehAtualizacao ? c.Id != morador.Id : true))).Any())
        {
            Notificar("Já existe um morador com este nome informado.");
            return false;
        }

        if (!string.IsNullOrWhiteSpace(morador.Pessoa.Email)
            && (await _moradorRepository.BuscarPorCondicaoAsync(c => c.Pessoa.Email == morador.Pessoa.Email && (ehAtualizacao ? c.Id != morador.Id : true))).Any())
        {
            Notificar("Já existe um morador com este e-mail informado.");
            return false;
        }

        return true;
    }

    private async Task<bool> TemAssociacoes(int moradorId)
    {
        var possuiEntregas = await _entregaRepository.BuscarPorCondicaoAsync(e => e.MoradorId == moradorId);

        return possuiEntregas.Any();
    }
}