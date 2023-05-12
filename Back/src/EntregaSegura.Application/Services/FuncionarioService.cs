using EntregaSegura.Application.Notifications;
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

    public async Task Adicionar(Funcionario funcionario)
    {
        if(!ExecutarValidacao(new FuncionarioValidator(), funcionario)) return;

        if(_funcionarioRepository.BuscarAsync(f => f.CPF == funcionario.CPF).Result.Any())
        {
            Notificar("Já existe um funcionário com este CPF.");
            return;
        }

        if(_funcionarioRepository.BuscarAsync(f => f.Nome == funcionario.Nome).Result.Any())
        {
            Notificar("Já existe um funcionário com este nome.");
            return;
        }

        _funcionarioRepository.Adicionar(funcionario);
        await CommitAsync();
    }

    public async Task Atualizar(Funcionario funcionario)
    {
        if(!ExecutarValidacao(new FuncionarioValidator(), funcionario)) return;

        if(_funcionarioRepository.BuscarAsync(f => f.CPF == funcionario.CPF && f.Id != funcionario.Id).Result.Any())
        {
            Notificar("Já existe um funcionário com este CPF.");
            return;
        }

        _funcionarioRepository.Atualizar(funcionario);
        await CommitAsync();
    }

    public async Task Remover(Guid id)
    {
        if(_funcionarioRepository.ObterFuncionarioComEntregasAsync(id).Result.Entregas.Any())
        {
            Notificar("O funcionário possui entregas cadastradas!");
            return;
        }

        _funcionarioRepository.Remover(id);
        await CommitAsync();
    }

    public async Task<IEnumerable<Funcionario>> ObterTodosAsync()
    {
        return await _funcionarioRepository.ObterTodosAsync();
    }

    public async Task<Funcionario> ObterPorIdAsync(Guid id)
    {
        return await _funcionarioRepository.ObterPorIdAsync(id);
    }

    public async Task<Funcionario> ObterPorNomeAsync(string nome)
    {
        return await _funcionarioRepository.ObterPorNomeAsync(nome);
    }

    public async Task<Funcionario> ObterFuncionarioComEntregasAsync(Guid funcionarioId)
    {
        return await _funcionarioRepository.ObterFuncionarioComEntregasAsync(funcionarioId);
    }

    public void Dispose()
    {
        _unitOfWork?.Dispose();
    }
}