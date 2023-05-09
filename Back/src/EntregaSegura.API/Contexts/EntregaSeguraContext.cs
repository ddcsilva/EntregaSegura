using EntregaSegura.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntregaSegura.API.Contexts;

public class EntregaSeguraContext : DbContext
{
    public EntregaSeguraContext(DbContextOptions<EntregaSeguraContext> options) : base(options) { }

    public DbSet<Condominio> Condominios => Set<Condominio>();
}