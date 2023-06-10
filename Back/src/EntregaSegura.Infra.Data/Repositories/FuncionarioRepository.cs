using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces;
using EntregaSegura.Infra.Data.Contexts;

namespace EntregaSegura.Infra.Data.Repositories;

public class FuncionarioRepository : RepositoryBase<Funcionario>, IFuncionarioRepository
{
    public FuncionarioRepository(EntregaSeguraContext context) : base(context)
    {
    }
}