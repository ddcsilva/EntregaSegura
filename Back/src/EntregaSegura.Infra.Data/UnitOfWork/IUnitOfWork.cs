using EntregaSegura.Infra.Data.Contexts;

namespace EntregaSegura.Infra.Data.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    Task<int> CommitAsync();
    EntregaSeguraContext Context { get; }
}