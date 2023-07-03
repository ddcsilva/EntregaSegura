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

        builder.Property(u => u.Login)
            .HasColumnName("USR_LOGIN")
            .HasColumnOrder(2)
            .IsRequired()
            .HasColumnType("varchar(100)")
            .HasComment("Login do usuário");

        builder.Property(u => u.Senha)
            .HasColumnName("USR_SENHA")
            .HasColumnOrder(3)
            .IsRequired()
            .HasColumnType("varchar(50)")
            .HasComment("Senha do usuário");

        builder.Property(u => u.Token)
            .HasColumnName("USR_TOKEN")
            .HasColumnOrder(4)
            .HasColumnType("varchar(100)")
            .HasComment("Token de acesso do usuário");

        builder.Property(u => u.Perfil)
            .HasColumnName("USR_PERFIL")
            .HasColumnOrder(5)
            .IsRequired()
            .HasComment("Perfil de acesso do usuário");

        builder.Property(u => u.Foto)
            .HasColumnName("USR_FOTO")
            .HasColumnOrder(6)
            .HasColumnType("varchar(100)")
            .HasComment("Foto do usuário");

        builder.Property(u => u.PessoaId)
            .HasColumnName("USR_PESSOA_ID")
            .HasColumnOrder(7)
            .IsRequired()
            .HasComment("Chave estrangeira da pessoa do usuário");

        builder.Property(u => u.DataCriacao)
            .HasColumnName("USR_DATA_CRIACAO")
            .HasColumnOrder(8)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()")
            .HasComment("Data de criação do usuário");

        builder.Property(u => u.DataAtualizacao)
            .HasColumnName("USR_DATA_ATUALIZACAO")
            .HasColumnOrder(9)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()")
            .HasComment("Data de atualização do usuário");

        builder.HasOne(u => u.Pessoa)
            .WithOne()
            .HasForeignKey<Usuario>(u => u.PessoaId)
            .HasConstraintName("FK_USUARIOS_PESSOAS")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(u => u.Login)
            .HasDatabaseName("IX_USR_LOGIN")
            .IsUnique();
    }
}