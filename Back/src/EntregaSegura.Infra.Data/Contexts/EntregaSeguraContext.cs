using Microsoft.EntityFrameworkCore;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Infra.Data.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using EntregaSegura.Infra.Data.Identity;

namespace EntregaSegura.Infra.Data.Contexts;

public class EntregaSeguraContext : IdentityDbContext<ApplicationUser>
{
    public EntregaSeguraContext(DbContextOptions options) : base(options) { }

    public DbSet<Condominio> Condominios => Set<Condominio>();
    public DbSet<Entrega> Entregas => Set<Entrega>();
    public DbSet<Funcionario> Funcionarios => Set<Funcionario>();
    public DbSet<Morador> Moradores => Set<Morador>();
    public DbSet<Transportadora> Transportadoras => Set<Transportadora>();
    public DbSet<Unidade> Unidades => Set<Unidade>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EntregaSeguraContext).Assembly);

        modelBuilder.SeedData();

        base.OnModelCreating(modelBuilder);
    }
}