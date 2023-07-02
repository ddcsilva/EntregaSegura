using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Infra.Data.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("TB_USUARIOS");

        builder.HasKey(u => u.Id)
            .HasName("PK_USUARIOS");

        builder.Property(u => u.Id)
            .HasColumnName("USR_ID")
            .HasColumnOrder(1)
            .ValueGeneratedOnAdd()
            .HasComment("Chave primária do usuário");

        builder.Property(u => u.Nome)
            .HasColumnName("USR_NOME")
            .HasColumnOrder(2)
            .IsRequired()
            .HasColumnType("varchar(100)")
            .HasComment("Nome do usuário");

        builder.Property(u => u.Login)
            .HasColumnName("USR_LOGIN")
            .HasColumnOrder(3)
            .IsRequired()
            .HasColumnType("varchar(50)")
            .HasComment("Login do usuário");

        builder.Property(u => u.Senha)
            .HasColumnName("USR_SENHA")
            .HasColumnOrder(4)
            .IsRequired()
            .HasColumnType("varchar(50)")
            .HasComment("Senha do usuário");

        builder.Property(u => u.Email)
            .HasColumnName("USR_EMAIL")
            .HasColumnOrder(5)
            .IsRequired()
            .HasColumnType("varchar(100)")
            .HasComment("E-mail do usuário");

        builder.Property(u => u.Token)
            .HasColumnName("USR_TOKEN")
            .HasColumnOrder(6)
            .HasColumnType("varchar(100)")
            .HasComment("Token do usuário");

        builder.Property(u => u.Perfil)
            .HasColumnName("USR_PERFIL")
            .HasColumnOrder(7)
            .IsRequired()
            .HasComment("Perfil do usuário");

        builder.Property(u => u.DataCriacao)
            .HasColumnName("USR_DTCRIACAO")
            .HasColumnOrder(8)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()")
            .HasComment("Data de criação do usuário");

        builder.Property(u => u.DataAtualizacao)
            .HasColumnName("USR_DTATUALIZACAO")
            .HasColumnOrder(9)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()")
            .HasComment("Data de atualização do usuário");

        builder.HasIndex(u => u.Login)
            .HasDatabaseName("IX_USR_LOGIN")
            .IsUnique();

        builder.HasIndex(u => u.Email)
            .HasDatabaseName("IX_USR_EMAIL")
            .IsUnique();
    }
}