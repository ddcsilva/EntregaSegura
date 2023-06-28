using AutoMapper;
using EntregaSegura.Application.DTOs;
using EntregaSegura.Application.Interfaces;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces;
using EntregaSegura.Domain.Validations;

namespace EntregaSegura.Application.Services;

public class FuncionarioService : BaseService, IFuncionarioService
{
    private readonly IFuncionarioRepository _funcionarioRepository;
    private readonly IUsuarioService _usuarioService;
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;

    public FuncionarioService(IFuncionarioRepository funcionarioRepository,
                              IUsuarioService usuarioService,
                              IEmailService emailService,
                              IMapper mapper,
                              INotificadorErros notificadorErros) : base(notificadorErros)
    {
        _funcionarioRepository = funcionarioRepository;
        _usuarioService = usuarioService;
        _emailService = emailService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<FuncionarioDTO>> ObterTodosFuncionariosAsync()
    {
        var funcionarios = await _funcionarioRepository.BuscarTodosAsync();
        return _mapper.Map<IEnumerable<FuncionarioDTO>>(funcionarios);
    }

    public async Task<FuncionarioDTO> ObterFuncionarioPorIdAsync(int id, bool rastrearAlteracoes = false)
    {
        var funcionario = await _funcionarioRepository.BuscarPorIdAsync(id, rastrearAlteracoes);
        return _mapper.Map<FuncionarioDTO>(funcionario);
    }

    public async Task<bool> AdicionarAsync(FuncionarioDTO funcionarioDTO)
    {
        var funcionario = _mapper.Map<Funcionario>(funcionarioDTO);

        if (!await ValidarFuncionario(funcionario)) return false;

        var usuarioDTO = new UsuarioDTO
        {
            UserName = funcionario.Email,
            Email = funcionario.Email,
            Senha = "123456"
        };

        using (_funcionarioRepository.IniciarTrasacaoAsync())
        {
            var usuarioRegistradoComSucesso = await _usuarioService.CriarContaUsuarioAsync(usuarioDTO, "FUNCIONARIO");

            if (usuarioRegistradoComSucesso == null)
            {
                Notificar("Ocorreu um erro ao adicionar o funcionário.");
                return false;
            }

            funcionario.DefinirUsuario(usuarioRegistradoComSucesso.Id);

            _funcionarioRepository.Adicionar(funcionario);

            var adicionadoComSucesso = await _funcionarioRepository.SalvarAlteracoesAsync();

            if (!adicionadoComSucesso)
            {
                Notificar("Ocorreu um erro ao adicionar o funcionário.");
                await _funcionarioRepository.DescartarTransacaoAsync();
                return false;
            }

            await _funcionarioRepository.SalvarTransacaoAsync();

            (string assuntoEmail, string mensagemEmail) = ConstruirEmail(funcionario, usuarioDTO);

            await _emailService.EnviarEmailAsync(funcionarioDTO.Email, assuntoEmail, mensagemEmail);
        }

        funcionarioDTO.Id = funcionario.Id;
        funcionarioDTO.UserId = funcionario.UserId;

        return true;
    }

    public async Task<bool> AtualizarAsync(FuncionarioDTO funcionarioDTO)
    {
        var funcionario = _mapper.Map<Funcionario>(funcionarioDTO);

        if (!await ValidarFuncionario(funcionario)) return false;

        _funcionarioRepository.Atualizar(funcionario);

        var atualizadoComSucesso = await _funcionarioRepository.SalvarAlteracoesAsync();

        if (!atualizadoComSucesso)
        {
            Notificar("Ocorreu um erro ao atualizar o funcionário.");
            return false;
        }

        return true;
    }

    public async Task<bool> RemoverAsync(int id)
    {
        var funcionario = await _funcionarioRepository.BuscarPorIdAsync(id);

        if (funcionario == null)
        {
            Notificar("Funcionário não encontrado.");
            return false;
        }

        _funcionarioRepository.Remover(funcionario);

        var removidoComSucesso = await _funcionarioRepository.SalvarAlteracoesAsync();

        if (!removidoComSucesso)
        {
            Notificar("Ocorreu um erro ao remover o funcionário.");
            return false;
        }

        return true;
    }

    public async Task<IEnumerable<FuncionarioDTO>> ObterTodosFuncionariosECondominiosAsync()
    {
        var funcionarios = await _funcionarioRepository.ObterTodosFuncionariosECondominiosAsync();
        return _mapper.Map<IEnumerable<FuncionarioDTO>>(funcionarios);
    }

    public async Task<FuncionarioDTO> ObterFuncionarioPeloUsuarioAsync(int usuarioId)
    {
        var funcionario = await _funcionarioRepository.BuscarPorCondicaoAsync(m => m.UserId == usuarioId);
        return _mapper.Map<FuncionarioDTO>(funcionario.FirstOrDefault());
    }

    public void Dispose()
    {
        _funcionarioRepository?.Dispose();
    }

    private (string, string) ConstruirEmail(Funcionario funcionario, UsuarioDTO usuarioDTO)
    {
        string assuntoEmail = "Bem-vindo ao EntregaSegura!";
        string mensagemEmail = $"Olá {funcionario.Nome},\n\n" +
                               $"Sua conta no EntregaSegura foi criada com sucesso! " +
                               $"Suas credenciais são:\n\n" +
                               $"Usuário: {usuarioDTO.UserName}\n" +
                               $"Senha: {usuarioDTO.Senha}\n\n" +
                               $"Atenciosamente,\n" +
                               $"Equipe EntregaSegura";
        return (assuntoEmail, mensagemEmail);
    }

    private async Task<bool> ValidarFuncionario(Funcionario funcionario, bool ehAtualizacao = false)
    {
        if (!ExecutarValidacao(new FuncionarioValidator(), funcionario)) return false;

        if (!string.IsNullOrWhiteSpace(funcionario.Cpf)
            && (await _funcionarioRepository.BuscarPorCondicaoAsync(c => c.Cpf == funcionario.Cpf && (ehAtualizacao ? c.Id != funcionario.Id : true))).Any())
        {
            Notificar("Já existe um funcionário com este CPF informado.");
            return false;
        }

        if (!string.IsNullOrWhiteSpace(funcionario.Nome)
            && (await _funcionarioRepository.BuscarPorCondicaoAsync(c => c.Nome == funcionario.Nome && (ehAtualizacao ? c.Id != funcionario.Id : true))).Any())
        {
            Notificar("Já existe um funcionário com este nome informado.");
            return false;
        }

        if (!string.IsNullOrWhiteSpace(funcionario.Email)
            && (await _funcionarioRepository.BuscarPorCondicaoAsync(c => c.Email == funcionario.Email && (ehAtualizacao ? c.Id != funcionario.Id : true))).Any())
        {
            Notificar("Já existe um funcionário com este e-mail informado.");
            return false;
        }

        return true;
    }
}