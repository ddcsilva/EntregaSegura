using AutoMapper;
using EntregaSegura.Application.DTOs;
using EntregaSegura.Application.Interfaces;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces;
using EntregaSegura.Domain.Interfaces.Account;
using EntregaSegura.Domain.Validations;

namespace EntregaSegura.Application.Services;

public class MoradorService : BaseService, IMoradorService
{
    private readonly IMoradorRepository _moradorRepository;
    // private readonly IAutenticacaoService _autenticacaoService;
    private readonly IMapper _mapper;

    public MoradorService(
        IMoradorRepository moradorRepository,
        // IAutenticacaoService autenticacaoService,
        IMapper mapper,
        INotificadorErros notificadorErros) : base(notificadorErros)
    {
        _moradorRepository = moradorRepository;
        // _autenticacaoService = autenticacaoService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MoradorDTO>> ObterTodosMoradoresAsync()
    {
        var moradores = await _moradorRepository.BuscarTodosAsync();
        return _mapper.Map<IEnumerable<MoradorDTO>>(moradores);
    }

    public async Task<MoradorDTO> ObterMoradorPorIdAsync(int id)
    {
        var morador = await _moradorRepository.BuscarPorIdAsync(id, rastrearAlteracoes: true);
        return _mapper.Map<MoradorDTO>(morador);
    }

    public async Task<bool> AdicionarAsync(MoradorDTO moradorDTO)
    {
        var morador = _mapper.Map<Morador>(moradorDTO);

        if (!await ValidarMorador(morador)) return false;

        _moradorRepository.Adicionar(morador);
        
        var adicionadoComSucesso = await _moradorRepository.SalvarAlteracoesAsync();

        if (!adicionadoComSucesso)
        {
            Notificar("Ocorreu um erro ao adicionar o morador.");
            return false;
        }

        moradorDTO.Id = morador.Id;

        // var senhaAleatoria = _autenticacaoService.GerarSenhaAleatoria();
        // var usuarioRegistradoComSucesso = await _autenticacaoService.RegistrarAsync(moradorDTO.Email, senhaAleatoria, morador.Id);

        // if (!usuarioRegistradoComSucesso)
        // {
        //     Notificar("Erro ao tentar adicionar usuário para o morador.");
        // }

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

        _moradorRepository.Remover(morador);

        var removidoComSucesso = await _moradorRepository.SalvarAlteracoesAsync();

        if (!removidoComSucesso)
        {
            Notificar("Ocorreu um erro ao remover o morador.");
            return false;
        }

        return true;
    }

    public void Dispose()
    {
        _moradorRepository?.Dispose();
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
}