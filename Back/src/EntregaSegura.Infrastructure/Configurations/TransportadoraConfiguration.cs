using EntregaSegura.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntregaSegura.Infrastructure.Configurations;

public class TransportadoraConfiguration : IEntityTypeConfiguration<Transportadora>
{
    public void Configure(EntityTypeBuilder<Transportadora> builder)
    {
        builder.ToTable("TB_TRANSPORTADORAS");

        builder.HasKey(e => e.Id)
            .HasName("PK_TRANSPORTADORAS");

        builder.Property(e => e.Id)
            .HasColumnName("TRA_ID")
            .HasColumnOrder(1)
            .ValueGeneratedOnAdd()
            .HasComment("Chave primária da transportadora");

        builder.Property(e => e.Nome)
            .HasColumnName("TRA_NOME")
            .HasColumnOrder(2)
            .IsRequired()
            .HasColumnType("varchar(100)")
            .HasComment("Nome da transportadora");

        builder.Property(e => e.CNPJ)
            .HasColumnName("TRA_CNPJ")
            .HasColumnOrder(3)
            .HasColumnType("varchar(14)")
            .HasComment("CNPJ da transportadora");

        builder.Property(e => e.Telefone)
            .HasColumnName("TRA_TELEFONE")
            .HasColumnOrder(4)
            .HasColumnType("varchar(11)")
            .HasComment("Telefone da transportadora");

        builder.Property(e => e.Email)
            .HasColumnName("TRA_EMAIL")
            .HasColumnOrder(5)
            .HasColumnType("varchar(100)")
            .HasComment("E-mail da transportadora");

        builder.Property(e => e.DataCriacao)
            .HasColumnName("TRA_DATA_CRIACAO")
            .HasColumnOrder(6)
            .IsRequired()
            .HasColumnType("datetime")
            .HasDefaultValueSql("GETDATE()")
            .HasComment("Data de criação da transportadora")
            .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);

        builder.Property(e => e.DataAtualizacao)
            .HasColumnName("TRA_DATA_ATUALIZACAO")
            .HasColumnOrder(7)
            .IsRequired()
            .HasColumnType("datetime")
            .HasDefaultValueSql("GETDATE()")
            .HasComment("Data da última atualização da transportadora")
            .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Save);

        // Uma transportadora possui várias entregas e uma entrega possui uma transportadora
        builder.HasMany(t => t.Entregas)
            .WithOne(e => e.Transportadora)
            .HasForeignKey(e => e.TransportadoraId)
            .HasConstraintName("FK_ENTREGA_TRANSPORTADORA")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(e => e.CNPJ)
            .HasDatabaseName("IX_TRANSPORTADORAS_CNPJ")
            .IsUnique();

        builder.HasIndex(e => e.Email)
            .HasDatabaseName("IX_TRANSPORTADORAS_EMAIL")
            .IsUnique();

        builder.HasIndex(e => e.Nome)
            .HasDatabaseName("IX_TRANSPORTADORAS_NOME")
            .IsUnique();

        builder.HasIndex(e => e.Telefone)
            .HasDatabaseName("IX_TRANSPORTADORAS_TELEFONE")
            .IsUnique();
    }
}