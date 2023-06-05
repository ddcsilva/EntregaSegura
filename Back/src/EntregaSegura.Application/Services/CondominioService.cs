using AutoMapper;
using EntregaSegura.Application.DTOs;
using EntregaSegura.Application.Interfaces;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces;
using EntregaSegura.Domain.Validations;
using EntregaSegura.Infra.Data.UnitOfWork;

namespace EntregaSegura.Application.Services;

public class CondominioService : BaseService, ICondominioService
{
    private readonly ICondominioRepository _condominioRepository;
    private readonly IMapper _mapper;

    public CondominioService(
        ICondominioRepository condominioRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        INotificadorErros notificadorErros) : base(unitOfWork, notificadorErros)
    {
        _condominioRepository = condominioRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CondominioDTO>> ObterTodosCondominiosAsync()
    {
        var condominios = await _condominioRepository.ObterTodosCondominiosAsync();
        return _mapper.Map<IEnumerable<CondominioDTO>>(condominios); 
    }

    public async Task<CondominioDTO> ObterCondominioPorIdAsync(int id)
    {
        var condominio = await _condominioRepository.ObterCondominioPorIdAsync(id);
        return _mapper.Map<CondominioDTO>(condominio);
    }

    public async Task AdicionarAsync(CondominioDTO condominioDTO)
    {
        var condominio = _mapper.Map<Condominio>(condominioDTO);

        if (!ExecutarValidacao(new CondominioValidator(), condominio))
        {
            return;
        }

        if (_condominioRepository.BuscarAsync(c => c.Cnpj == condominio.Cnpj).Result.Any())
        {
            Notificar("Já existe um condomínio com este CNPJ.");
        }

        if (_condominioRepository.BuscarAsync(c => c.Nome == condominio.Nome).Result.Any())
        {
            Notificar("Já existe um condomínio com este nome.");
        }

        if (_condominioRepository.BuscarAsync(c => c.Email == condominio.Email).Result.Any())
        {
            Notificar("Já existe um condomínio com este e-mail.");
        }

        if (TemNotificacoes()) 
        {
            return;
        }

        _condominioRepository.Adicionar(condominio);
        var resultadoOperacao = await CommitAsync();

        if (resultadoOperacao == 0)
        {
            Notificar("Ocorreu um erro ao salvar o condomínio.");
        }
    }

    public async Task AtualizarAsync(CondominioDTO condominioDTO)
    {
        var condominio = _mapper.Map<Condominio>(condominioDTO);

        if (!ExecutarValidacao(new CondominioValidator(), condominio)) 
        {
            return;
        }

        if (_condominioRepository.BuscarAsync(c => c.Cnpj == condominio.Cnpj && c.Id != condominio.Id).Result.Any())
        {
            Notificar("Já existe um condomínio com este CNPJ.");
        }

        if (_condominioRepository.BuscarAsync(c => c.Nome == condominio.Nome && c.Id != condominio.Id).Result.Any())
        {
            Notificar("Já existe um condomínio com este Nome.");
        }

        if (_condominioRepository.BuscarAsync(c => c.Email == condominio.Email && c.Id != condominio.Id).Result.Any())
        {
            Notificar("Já existe um condomínio com este E-mail.");
        }

        if (TemNotificacoes()) 
        {
            return;
        }

        _condominioRepository.Atualizar(condominio);
        var resultadoOperacao = await CommitAsync();

        if (resultadoOperacao == 0)
        {
            Notificar("Ocorreu um erro ao atualizar o condomínio.");
        }
    }

    public async Task RemoverAsync(int id)
    {
        var condominio = _condominioRepository.ObterCondominioPorIdAsync(id).Result;

        if (condominio == null)
        {
            Notificar("Condomínio não encontrado.");
            return;
        }

        _condominioRepository.Remover(condominio);

        try
        {
            var resultadoOperacao = await CommitAsync();

            if (resultadoOperacao == 0)
            {
                Notificar("Ocorreu um erro ao remover o condomínio.");
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
