using Microsoft.EntityFrameworkCore;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Infra.Data.Extensions;

namespace EntregaSegura.Infra.Data.Contexts;

public class EntregaSeguraContext : DbContext
{
    public EntregaSeguraContext(DbContextOptions options) : base(options) { }

    public DbSet<Condominio> Condominios => Set<Condominio>();
    public DbSet<Unidade> Unidades => Set<Unidade>();
    public DbSet<Pessoa> Pessoas => Set<Pessoa>();
    public DbSet<Morador> Moradores => Set<Morador>();
    public DbSet<Funcionario> Funcionarios => Set<Funcionario>();
    public DbSet<Transportadora> Transportadoras => Set<Transportadora>();
    public DbSet<Entrega> Entregas => Set<Entrega>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EntregaSeguraContext).Assembly);

        modelBuilder.SeedData();
    }
}