using EntregaSegura.Application.Interfaces;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces.Repositories;
using EntregaSegura.Domain.Validators;
using EntregaSegura.Infrastructure.UnitOfWork;

namespace EntregaSegura.Application.Services;

public class CondominioService : BaseService, ICondominioService
{
    private readonly ICondominioRepository _condominioRepository;

    public CondominioService(ICondominioRepository condominioRepository,
                             IUnitOfWork unitOfWork,
                             INotificadorErros notificadorErros) : base(unitOfWork, notificadorErros)
    {
        _condominioRepository = condominioRepository;
    }

    public async Task<Condominio> Adicionar(Condominio condominio)
    {
        if(!ExecutarValidacao(new CondominioValidator(), condominio)) return null;

        if(_condominioRepository.BuscarAsync(c => c.CNPJ == condominio.CNPJ).Result.Any())
        {
            Notificar("Já existe um condomínio com este CNPJ.");
        }

        if(_condominioRepository.BuscarAsync(c => c.Nome == condominio.Nome).Result.Any())
        {
            Notificar("Já existe um condomínio com este nome.");
        }

        if(_condominioRepository.BuscarAsync(c => c.Email == condominio.Email).Result.Any())
        {
            Notificar("Já existe um condomínio com este e-mail.");
        }

        if (TemNotificacoes()) return null;

        _condominioRepository.Adicionar(condominio);
        var result = await CommitAsync();

        if (result == 0)
        {
            Notificar("Ocorreu um erro ao salvar o condomínio.");
            return null;
        }

        return condominio;
    }

    public async Task<Condominio> Atualizar(Condominio condominio)
    {
        if(!ExecutarValidacao(new CondominioValidator(), condominio)) return null;

        if(_condominioRepository.BuscarAsync(c => c.CNPJ == condominio.CNPJ && c.Id != condominio.Id).Result.Any())
        {
            Notificar("Já existe um condomínio com este CNPJ.");
            return null;
        }

        if(_condominioRepository.BuscarAsync(c => c.Nome == condominio.Nome && c.Id != condominio.Id).Result.Any())
        {
            Notificar("Já existe um condomínio com este Nome.");
            return null;
        }

        if(_condominioRepository.BuscarAsync(c => c.Email == condominio.Email && c.Id != condominio.Id).Result.Any())
        {
            Notificar("Já existe um condomínio com este E-mail.");
            return null;
        }

        _condominioRepository.Atualizar(condominio);
        var result = await CommitAsync();

        if (result == 0)
        {
            Notificar("Ocorreu um erro ao atualizar o condomínio.");
            return null;
        }

        return condominio;
    }

    public async Task <bool> Remover(int id)
    {
        var condominio = await _condominioRepository.ObterPorIdAsync(id);

        if (condominio == null)
        {
            Notificar("Condomínio não encontrado.");
            return false;
        }

        _condominioRepository.Remover(condominio);

        try
        {
            var result = await CommitAsync();

            if (result == 0)
            {
                Notificar("Ocorreu um erro ao remover o condomínio.");
                return false;
            }
        }
        catch (Exception ex)
        {
            Notificar(ex.Message);
            return false;
        }

        return true;
    }

    public async Task<IEnumerable<Condominio>> ObterTodosAsync()
    {
        return await _condominioRepository.ObterTodosAsync();
    }

    public async Task<IEnumerable<Condominio>> ObterTodosCondominiosPeloNomeAsync(string nome)
    {
        return await _condominioRepository.ObterTodosCondominiosPeloNomeAsync(nome);
    }

    public async Task<Condominio> ObterPorIdAsync(int id)
    {
        return await _condominioRepository.ObterPorIdAsync(id);
    }

    public async Task<Condominio> ObterCondominioComFuncionariosAsync(int condominioId)
    {
        return await _condominioRepository.ObterCondominioComFuncionariosAsync(condominioId);
    }

    public async Task<Condominio> ObterCondominioComUnidadesAsync(int condominioId)
    {
        return await _condominioRepository.ObterCondominioComUnidadesAsync(condominioId);
    }

    public async Task<Condominio> ObterCondominioComUnidadesEFuncionariosAsync(int condominioId)
    {
        return await _condominioRepository.ObterCondominioComUnidadesEFuncionariosAsync(condominioId);
    }

    public void Dispose()
    {
        _unitOfWork?.Dispose();
    }
}
