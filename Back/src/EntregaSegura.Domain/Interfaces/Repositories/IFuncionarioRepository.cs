using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Domain.Interfaces.Repositories;

public interface IFuncionarioRepository : IRepository<Funcionario>
{
    Task<Funcionario> ObterPorNomeAsync(string nome);
    Task<Funcionario> ObterFuncionarioComEntregasAsync(Guid id);
}