using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces;
using EntregaSegura.Infra.Data.Contexts;

namespace EntregaSegura.Infra.Data.Repositories;

public class MoradorRepository : RepositoryBase<Morador>, IMoradorRepository
{
    public MoradorRepository(EntregaSeguraContext context) : base(context)
    {
    }
}