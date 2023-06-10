using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces;
using EntregaSegura.Infra.Data.Contexts;

namespace EntregaSegura.Infra.Data.Repositories;

public class CondominioRepository : RepositoryBase<Condominio>, ICondominioRepository
{
    public CondominioRepository(EntregaSeguraContext context) : base(context)
    {
    }
}