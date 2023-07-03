using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Infra.Data.Configurations;

public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
{
    public void Configure(EntityTypeBuilder<Pessoa> builder)
    {
        builder.ToTable("TB_PESSOAS");

        builder.HasKey(p => p.Id)
            .HasName("PK_PESSOAS");

        builder.Property(p => p.Id)
            .HasColumnName("PES_ID")
            .HasColumnOrder(1)
            .ValueGeneratedOnAdd()
            .HasComment("Chave primária da pessoa");

        builder.Property(p => p.Nome)
            .HasColumnName("PES_NOME")
            .HasColumnOrder(2)
            .IsRequired()
            .HasColumnType("varchar(100)")
            .HasComment("Nome da pessoa");

        builder.Property(p => p.Cpf)
            .HasColumnName("PES_CPF")
            .HasColumnOrder(3)
            .IsRequired()
            .HasColumnType("varchar(11)")
            .HasComment("CPF da pessoa");

        builder.Property(p => p.Telefone)
            .HasColumnName("PES_TELEFONE")
            .HasColumnOrder(4)
            .IsRequired()
            .HasColumnType("varchar(11)")
            .HasComment("Telefone da pessoa");

        builder.Property(p => p.Email)
            .HasColumnName("PES_EMAIL")
            .HasColumnOrder(5)
            .IsRequired()
            .HasColumnType("varchar(100)")
            .HasComment("E-mail da pessoa");

        builder.Property(p => p.DataCriacao)
            .HasColumnName("PES_DATA_CRIACAO")
            .HasColumnOrder(6)
            .IsRequired()
            .HasColumnType("datetime")
            .HasDefaultValueSql("GETDATE()")
            .HasComment("Data de criação da pessoa")
            .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);

        builder.Property(p => p.DataAtualizacao)
            .HasColumnName("PES_DATA_ATUALIZACAO")
            .HasColumnOrder(7)
            .IsRequired()
            .HasColumnType("datetime")
            .HasDefaultValueSql("GETDATE()")
            .HasComment("Data da última atualização da pessoa")
            .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Save);

        builder.HasIndex(p => p.Cpf)
            .HasDatabaseName("IX_PESSOAS_CPF")
            .IsUnique();

        builder.HasIndex(p => p.Telefone)
            .HasDatabaseName("IX_PESSOAS_TELEFONE")
            .IsUnique();

        builder.HasIndex(p => p.Email)
            .HasDatabaseName("IX_PESSOAS_EMAIL")
            .IsUnique();
    }
}