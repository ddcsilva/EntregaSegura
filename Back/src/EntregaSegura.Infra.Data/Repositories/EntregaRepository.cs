using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces;
using EntregaSegura.Infra.Data.Contexts;

namespace EntregaSegura.Infra.Data.Repositories;

public class EntregaRepository : RepositoryBase<Entrega>, IEntregaRepository
{
    public EntregaRepository(EntregaSeguraContext context) : base(context)
    {
    }
}