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
    private readonly IEntregaRepository _entregaRepository;
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;

    public FuncionarioService(IFuncionarioRepository funcionarioRepository,
                              IEntregaRepository entregaRepository,
                              IEmailService emailService,
                              IMapper mapper,
                              INotificadorErros notificadorErros) : base(notificadorErros)
    {
        _funcionarioRepository = funcionarioRepository;
        _entregaRepository = entregaRepository;
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

            await _funcionarioRepository.SalvarTransacaoAsync();
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
        var funcionario = await _funcionarioRepository.BuscarPorIdAsync(id);

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

    public void Dispose()
    {
        _funcionarioRepository?.Dispose();
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

        if (!string.IsNullOrWhiteSpace(funcionario.Pessoa.Nome)
            && (await _funcionarioRepository.BuscarPorCondicaoAsync(c => c.Pessoa.Nome == funcionario.Pessoa.Nome && (ehAtualizacao ? c.Id != funcionario.Id : true))).Any())
        {
            Notificar("Já existe um funcionário com este nome informado.");
            return false;
        }

        if (!string.IsNullOrWhiteSpace(funcionario.Pessoa.Nome)
            && (await _funcionarioRepository.BuscarPorCondicaoAsync(c => c.Pessoa.Email == funcionario.Pessoa.Email && (ehAtualizacao ? c.Id != funcionario.Id : true))).Any())
        {
            Notificar("Já existe um funcionário com este e-mail informado.");
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