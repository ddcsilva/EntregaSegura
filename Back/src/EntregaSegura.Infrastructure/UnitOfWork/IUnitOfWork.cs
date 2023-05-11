using EntregaSegura.Infrastructure.Contexts;

namespace EntregaSegura.Infrastructure.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    Task<int> CommitAsync();
    EntregaSeguraContext Context { get; }
}