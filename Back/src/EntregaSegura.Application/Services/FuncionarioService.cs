using EntregaSegura.Application.Services;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces.Repositories;
using EntregaSegura.Domain.Validators;
using EntregaSegura.Infrastructure.UnitOfWork;

namespace EntregaSegura.Application.Interfaces;

public class FuncionarioService : BaseService, IFuncionarioService
{
    private readonly IFuncionarioRepository _funcionarioRepository;

    public FuncionarioService(IFuncionarioRepository funcionarioRepository,
                              IUnitOfWork unitOfWork,
                              INotificadorErros notificadorErros) : base(unitOfWork, notificadorErros)
    {
        _funcionarioRepository = funcionarioRepository;
    }

    public async Task<Funcionario> Adicionar(Funcionario funcionario)
    {
        if(!ExecutarValidacao(new FuncionarioValidator(), funcionario)) return null;

        if(_funcionarioRepository.BuscarAsync(f => f.CPF == funcionario.CPF).Result.Any())
        {
            Notificar("Já existe um funcionário com este CPF.");
            return null;
        }

        await _funcionarioRepository.AdicionarAsync(funcionario);
        var result = await CommitAsync();

        if (result == 0)
        {
            Notificar("Ocorreu um erro ao salvar o funcionário.");
            return null;
        }

        return funcionario;
    }

    public async Task<Funcionario> Atualizar(Funcionario funcionario)
    {
        if(!ExecutarValidacao(new FuncionarioValidator(), funcionario)) return null;

        if(_funcionarioRepository.BuscarAsync(f => f.CPF == funcionario.CPF && f.Id != funcionario.Id).Result.Any())
        {
            Notificar("Já existe um funcionário com este CPF.");
            return null;
        }

        _funcionarioRepository.Atualizar(funcionario);
        var result = await CommitAsync();

        if (result == 0)
        {
            Notificar("Ocorreu um erro ao atualizar o funcionário.");
            return null;
        }

        return funcionario;
    }

    public async Task<bool> Remover(int id)
    {
        var funcionario = await _funcionarioRepository.ObterPorIdAsync(id);

        if (funcionario == null)
        {
            Notificar("Funcionário não encontrado.");
            return false;
        }

        _funcionarioRepository.Remover(funcionario);
        var result = await CommitAsync();

        if (result == 0)
        {
            Notificar("Ocorreu um erro ao remover o funcionário.");
            return false;
        }

        return true;
    }

    public async Task<IEnumerable<Funcionario>> ObterTodosAsync()
    {
        return await _funcionarioRepository.ObterTodosAsync();
    }

    public async Task<Funcionario> ObterPorIdAsync(int id)
    {
        return await _funcionarioRepository.ObterPorIdAsync(id);
    }

    public async Task<Funcionario> ObterPorNomeAsync(string nome)
    {
        return await _funcionarioRepository.ObterPorNomeAsync(nome);
    }

    public async Task<Funcionario> ObterFuncionarioComEntregasAsync(int funcionarioId)
    {
        return await _funcionarioRepository.ObterFuncionarioComEntregasAsync(funcionarioId);
    }

    public void Dispose()
    {
        _unitOfWork?.Dispose();
    }
}