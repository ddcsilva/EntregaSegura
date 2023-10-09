using AutoMapper;
using EntregaSegura.Application.DTOs;
using EntregaSegura.Application.Helpers;
using EntregaSegura.Application.Interfaces;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Entities.Enums;
using EntregaSegura.Domain.Interfaces;
using EntregaSegura.Domain.Validations;

namespace EntregaSegura.Application.Services;

public class FuncionarioService : BaseService, IFuncionarioService
{
    private readonly IFuncionarioRepository _funcionarioRepository;
    private readonly IMoradorRepository _moradorRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IPessoaRepository _pessoaRepository;
    private readonly IEntregaRepository _entregaRepository;
    private readonly IUsuarioService _usuarioService;
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;

    public FuncionarioService(IFuncionarioRepository funcionarioRepository,
                              IMoradorRepository moradorRepository,
                              IUsuarioRepository usuarioRepository,
                              IPessoaRepository pessoaRepository,
                              IEntregaRepository entregaRepository,
                              IUsuarioService usuarioService,
                              IEmailService emailService,
                              IMapper mapper,
                              INotificadorErros notificadorErros) : base(notificadorErros)
    {
        _funcionarioRepository = funcionarioRepository;
        _moradorRepository = moradorRepository;
        _usuarioRepository = usuarioRepository;
        _pessoaRepository = pessoaRepository;
        _entregaRepository = entregaRepository;
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

    public async Task<FuncionarioDTO> ObterFuncionarioPorIdECondominioAsync(int id)
    {
        var funcionario = await _funcionarioRepository.ObterFuncionarioPorIdECondominioAsync(id);
        return _mapper.Map<FuncionarioDTO>(funcionario);
    }

    public async Task<bool> AdicionarAsync(FuncionarioDTO funcionarioDTO)
    {
        var funcionario = _mapper.Map<Funcionario>(funcionarioDTO);

        if (!await ValidarFuncionario(funcionario)) return false;

        var usuarioDTO = new UsuarioDTO
        {
            Login = funcionario.Pessoa.Email,
            Senha = Criptografia.CriptografarSenha("123456"),
            Perfil = PerfilUsuario.Morador
        };

        using (_funcionarioRepository.IniciarTrasacaoAsync())
        {
            _funcionarioRepository.Adicionar(funcionario);

            var adicionadoComSucesso = await _funcionarioRepository.SalvarAlteracoesAsync();

            if (!adicionadoComSucesso)
            {
                Notificar("Ocorreu um erro ao adicionar o funcionário.");
                await _funcionarioRepository.DescartarTransacaoAsync();
                return false;
            }

            usuarioDTO.PessoaId = funcionario.PessoaId;

            var usuarioRegistradoComSucesso = await _usuarioService.CriarContaUsuarioAsync(usuarioDTO);

            if (usuarioRegistradoComSucesso == null)
            {
                Notificar("Ocorreu um erro ao adicionar o funcionário.");
                await _moradorRepository.DescartarTransacaoAsync();
                return false;
            }

            await _funcionarioRepository.SalvarTransacaoAsync();
        }

        (string assuntoEmail, string mensagemEmail) = ConstruirEmail(funcionario);

        var emailEnviadoComSucesso = await _emailService.EnviarEmailAsync(funcionario.Pessoa.Email, assuntoEmail, mensagemEmail);

        if (!emailEnviadoComSucesso)
        {
            Notificar("Ocorreu um erro ao enviar o e-mail de registro do usuário.");
            return false;
        }

        funcionarioDTO.Id = funcionario.Id;

        return true;
    }

    public async Task<bool> AtualizarAsync(FuncionarioDTO funcionarioDTO)
    {
        var funcionario = _mapper.Map<Funcionario>(funcionarioDTO);

        if (!await ValidarFuncionario(funcionario, ehAtualizacao: true)) return false;

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
        var funcionario = await _funcionarioRepository.BuscarPorIdAsync(id, rastrearAlteracoes: true);

        if (funcionario == null)
        {
            Notificar("Funcionário não encontrado.");
            return false;
        }

        if (await TemAssociacoes(id))
        {
            Notificar("Este funcionário não pode ser removido pois existem registros associados a ele.");
            return false;
        }

        var usuario = await _usuarioRepository.ObterUsuarioPorPessoaAsync(funcionario.PessoaId, rastrearAlteracoes: true);
        var pessoa = await _pessoaRepository.BuscarPorIdAsync(funcionario.PessoaId, rastrearAlteracoes: true);

        using (_funcionarioRepository.IniciarTrasacaoAsync())
        {
            _usuarioRepository.Remover(usuario);

            var usuarioRemovidoComSucesso = await _usuarioRepository.SalvarAlteracoesAsync();

            if (!usuarioRemovidoComSucesso)
            {
                Notificar("Ocorreu um erro ao remover o funcionário.");
                await _funcionarioRepository.DescartarTransacaoAsync();
                return false;
            }

            _funcionarioRepository.Remover(funcionario);

            var funcionarioRemovidoComSucesso = await _funcionarioRepository.SalvarAlteracoesAsync();

            if (!funcionarioRemovidoComSucesso)
            {
                Notificar("Ocorreu um erro ao remover o funcionário.");
                await _funcionarioRepository.DescartarTransacaoAsync();
                return false;
            }

            await _funcionarioRepository.SalvarTransacaoAsync();

            _pessoaRepository.Remover(pessoa);

            var pessoaRemovidaComSucesso = await _pessoaRepository.SalvarAlteracoesAsync();

            if (!pessoaRemovidaComSucesso)
            {
                Notificar("Ocorreu um erro ao remover o funcionário.");
                await _moradorRepository.DescartarTransacaoAsync();
                return false;
            }
        }

        return true;
    }

    public async Task<IEnumerable<FuncionarioDTO>> ObterTodosFuncionariosECondominiosAsync()
    {
        var funcionarios = await _funcionarioRepository.ObterTodosFuncionariosECondominiosAsync();
        return _mapper.Map<IEnumerable<FuncionarioDTO>>(funcionarios);
    }

    public void Dispose()
    {
        _funcionarioRepository?.Dispose();
    }

    private (string, string) ConstruirEmail(Funcionario funcionario)
    {

        string assuntoEmail = "Bem-vindo ao EntregaSegura!";
        string mensagemEmail = $"Olá {funcionario.Pessoa.Nome},\n\n" +
                               $"Sua conta no EntregaSegura foi criada com sucesso! " +
                               $"Suas credenciais são:\n\n" +
                               $"Usuário: {funcionario.Pessoa.Email}\n" +
                               $"Senha: 123456 \n\n" +
                               $"Atenciosamente,\n" +
                               $"Equipe EntregaSegura";

        return (assuntoEmail, mensagemEmail);
    }

    private async Task<bool> ValidarFuncionario(Funcionario funcionario, bool ehAtualizacao = false)
    {
        if (!ExecutarValidacao(new FuncionarioValidator(), funcionario)) return false;

        if (!string.IsNullOrWhiteSpace(funcionario.Pessoa.Cpf)
            && (await _funcionarioRepository.BuscarPorCondicaoAsync(c => c.Pessoa.Cpf == funcionario.Pessoa.Cpf && (ehAtualizacao ? c.Id != funcionario.Id : true))).Any())
        {
            Notificar("Já existe um funcionário com este CPF informado.");
            return false;
        }
        else
        {
            if ((await _moradorRepository.BuscarPorCondicaoAsync(c => c.Pessoa.Cpf == funcionario.Pessoa.Cpf)).Any())
            {
                Notificar("Não é permitido cadastrar um funcionário com o CPF de um morador.");
                return false;
            }
        }

        if (!string.IsNullOrWhiteSpace(funcionario.Pessoa.Nome)
            && (await _funcionarioRepository.BuscarPorCondicaoAsync(c => c.Pessoa.Nome == funcionario.Pessoa.Nome && (ehAtualizacao ? c.Id != funcionario.Id : true))).Any())
        {
            Notificar("Já existe um funcionário com este nome informado.");
            return false;
        }

        if (!string.IsNullOrWhiteSpace(funcionario.Pessoa.Email)
            && (await _funcionarioRepository.BuscarPorCondicaoAsync(c => c.Pessoa.Email == funcionario.Pessoa.Email && (ehAtualizacao ? c.Id != funcionario.Id : true))).Any())
        {
            Notificar("Já existe um funcionário com este e-mail informado.");
            return false;
        }
        else
        {
            if ((await _moradorRepository.BuscarPorCondicaoAsync(c => c.Pessoa.Email == funcionario.Pessoa.Email)).Any())
            {
                Notificar("Não é permitido cadastrar um funcionário com o e-mail de um morador.");
                return false;
            }
        }

        if (!string.IsNullOrWhiteSpace(funcionario.Pessoa.Telefone)
            && (await _funcionarioRepository.BuscarPorCondicaoAsync(c => c.Pessoa.Telefone == funcionario.Pessoa.Telefone && (ehAtualizacao ? c.Id != funcionario.Id : true))).Any())
        {
            Notificar("Já existe um funcionário com este telefone informado.");
            return false;
        }
        else
        {
            if ((await _moradorRepository.BuscarPorCondicaoAsync(c => c.Pessoa.Telefone == funcionario.Pessoa.Telefone)).Any())
            {
                Notificar("Não é permitido cadastrar um funcionário com o telefone de um morador.");
                return false;
            }
        }

        return true;
    }

    private async Task<bool> TemAssociacoes(int moradorId)
    {
        var entregas = await _entregaRepository.BuscarPorCondicaoAsync(e => e.MoradorId == moradorId);

        return entregas.Any();
    }
}