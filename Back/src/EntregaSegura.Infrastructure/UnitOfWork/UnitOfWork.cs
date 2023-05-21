using EntregaSegura.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly EntregaSeguraContext _context;
    private readonly ILogger<UnitOfWork> _logger;

    public UnitOfWork(EntregaSeguraContext context, ILogger<UnitOfWork> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<int> CommitAsync()
    {
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                foreach (var entry in _context.ChangeTracker.Entries<BaseEntity>())
                {
                    entry.Entity.Atualizar();
                }

                var linhasAfetadas = await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return linhasAfetadas;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, $"Ocorreu um erro ao salvar as alterações no banco de dados: {ex.Message}");

                return 0;
            }
        }
    }

    public EntregaSeguraContext Context => _context;

    public void Dispose()
    {
        _context.Dispose();
    }
}
