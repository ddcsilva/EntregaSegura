using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces;
using EntregaSegura.Infra.Data.Contexts;

namespace EntregaSegura.Infra.Data.Repositories;

public class TransportadoraRepository : RepositoryBase<Transportadora>, ITransportadoraRepository
{
    public TransportadoraRepository(EntregaSeguraContext context) : base(context) { }
}