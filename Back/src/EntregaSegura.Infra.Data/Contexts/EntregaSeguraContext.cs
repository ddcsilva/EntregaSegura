using Microsoft.EntityFrameworkCore;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Infra.Data.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using EntregaSegura.Domain.Identity;

namespace EntregaSegura.Infra.Data.Contexts;

public class EntregaSeguraContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
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
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(b =>
        {
            b.ToTable("TB_USERS");
        });

        modelBuilder.Entity<IdentityUserClaim<int>>(b =>
        {
            b.ToTable("TB_USERCLAIMS");
        });

        modelBuilder.Entity<IdentityUserLogin<int>>(b =>
        {
            b.ToTable("TB_USERLOGINS");
        });

        modelBuilder.Entity<IdentityUserToken<int>>(b =>
        {
            b.ToTable("TB_USERTOKENS");
        });

        modelBuilder.Entity<Role>(b =>
        {
            b.ToTable("TB_ROLES");
        });

        modelBuilder.Entity<IdentityRoleClaim<int>>(b =>
        {
            b.ToTable("TB_ROLECLAIMS");
        });

        modelBuilder.Entity<UserRole>(b =>
        {
            b.ToTable("TB_USERROLES");

            b.HasKey(ur => new { ur.UserId, ur.RoleId });

            b.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            b.HasOne(ur => ur.User)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        });

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EntregaSeguraContext).Assembly);

        modelBuilder.SeedData();
    }
}