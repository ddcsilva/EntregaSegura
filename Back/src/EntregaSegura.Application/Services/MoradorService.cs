using AutoMapper;
using EntregaSegura.Application.DTOs;
using EntregaSegura.Application.Interfaces;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces;
using EntregaSegura.Domain.Interfaces.Account;
using EntregaSegura.Domain.Validations;
using EntregaSegura.Infra.Data.UnitOfWork;
using Microsoft.AspNetCore.Identity;

namespace EntregaSegura.Application.Services;

public class MoradorService : BaseService, IMoradorService
{
    private readonly IMoradorRepository _moradorRepository;
    private readonly IAutenticacaoService _autenticacaoService;
    private readonly IMapper _mapper;

    public MoradorService(IMoradorRepository moradorRepository,
                          IAutenticacaoService autenticacaoService,
                          IUnitOfWork unitOfWork,
                          IMapper mapper,
                          INotificadorErros notificadorErros) : base(unitOfWork, notificadorErros)

    {
        _moradorRepository = moradorRepository;
        _autenticacaoService = autenticacaoService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MoradorDTO>> ObterTodosMoradoresAsync()
    {
        var moradores = await _moradorRepository.ObterTodosMoradoresAsync();
        return _mapper.Map<IEnumerable<MoradorDTO>>(moradores);
    }

    public async Task<MoradorDTO> ObterMoradorPorIdAsync(int id)
    {
        var morador = await _moradorRepository.ObterMoradorPorIdAsync(id);
        return _mapper.Map<MoradorDTO>(morador);
    }

    public async Task AdicionarAsync(MoradorDTO moradorDTO)
    {
        var morador = _mapper.Map<Morador>(moradorDTO);

        if (!ExecutarValidacao(new MoradorValidator(), morador))
        {
            return;
        }

        var cpfExistente = _moradorRepository.BuscarAsync(m => m.Cpf == morador.Cpf).Result.Any();
        if (cpfExistente)
        {
            Notificar("Já existe um morador com este CPF informado.");
        }

        var nomeExistente = _moradorRepository.BuscarAsync(m => m.Nome == morador.Nome).Result.Any();
        if (nomeExistente)
        {
            Notificar("Já existe um morador com este nome informado.");
        }

        var emailExistente = _moradorRepository.BuscarAsync(m => m.Email == morador.Email).Result.Any();
        if (emailExistente)
        {
            Notificar("Já existe um morador com este e-mail informado.");
        }

        if (TemNotificacoes())
        {
            return;
        }

        _moradorRepository.Adicionar(morador);
        var resultadoOperacao = await _unitOfWork.CommitAsync();

        if (resultadoOperacao <= 0)
        {
            Notificar("Erro ao tentar adicionar morador.");
            return;
        }

        moradorDTO.Id = morador.Id;

        var senhaAleatoria = _autenticacaoService.GerarSenhaAleatoria();
        var resultadoRegistro = await _autenticacaoService.RegistrarAsync(morador.Email, senhaAleatoria, morador.Id);

        if (!resultadoRegistro)
        {
            Notificar("Erro ao tentar adicionar usuário para o morador.");
        }
    }

    public async Task AtualizarAsync(MoradorDTO moradorDTO)
    {
        var morador = _mapper.Map<Morador>(moradorDTO);

        if (!ExecutarValidacao(new MoradorValidator(), morador))
        {
            return;
        }

        var cpfExistente = _moradorRepository.BuscarAsync(m => m.Cpf == morador.Cpf && m.Id != morador.Id).Result.Any();
        if (cpfExistente)
        {
            Notificar("Já existe um morador com este CPF informado.");
        }

        var nomeExistente = _moradorRepository.BuscarAsync(m => m.Nome == morador.Nome && m.Id != morador.Id).Result.Any();
        if (nomeExistente)
        {
            Notificar("Já existe um morador com este nome informado.");
        }

        var emailExistente = _moradorRepository.BuscarAsync(m => m.Email == morador.Email && m.Id != morador.Id).Result.Any();
        if (emailExistente)
        {
            Notificar("Já existe um morador com este e-mail informado.");
        }

        if (TemNotificacoes())
        {
            return;
        }

        _moradorRepository.Atualizar(morador);
        var resultadoOperacao = await _unitOfWork.CommitAsync();

        if (resultadoOperacao <= 0)
        {
            Notificar("Erro ao tentar atualizar morador.");
            return;
        }
    }

    public async Task RemoverAsync(int id)
    {
        var morador = _moradorRepository.ObterMoradorPorIdAsync(id).Result;

        if (morador == null)
        {
            Notificar("Morador não encontrado.");
            return;
        }

        _moradorRepository.Remover(morador);

        try
        {
            var resultadoOperacao = await _unitOfWork.CommitAsync();

            if (resultadoOperacao <= 0)
            {
                Notificar("Erro ao tentar remover morador.");
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