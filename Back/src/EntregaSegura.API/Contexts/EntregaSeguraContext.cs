using EntregaSegura.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntregaSegura.API.Contexts;

public class EntregaSeguraContext : DbContext
{
    public EntregaSeguraContext(DbContextOptions<EntregaSeguraContext> options) : base(options) { }

    public DbSet<Condominio> Condominios => Set<Condominio>();
    public DbSet<Unidade> Unidades => Set<Unidade>();
    public DbSet<Morador> Moradores => Set<Morador>();
    public DbSet<Entrega> Entregas => Set<Entrega>();
    public DbSet<Transportadora> Transportadoras => Set<Transportadora>();
    public DbSet<Funcionario> Funcionarios => Set<Funcionario>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EntregaSeguraContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}