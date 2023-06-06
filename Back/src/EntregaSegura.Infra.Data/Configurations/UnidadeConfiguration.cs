using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Infra.Data.Configurations;

public class UnidadeConfiguration : IEntityTypeConfiguration<Unidade>
{
    public void Configure(EntityTypeBuilder<Unidade> builder)
    {
        builder.ToTable("TB_UNIDADES");

        builder.HasKey(u => u.Id)
            .HasName("PK_UNIDADES");

        builder.Property(u => u.Id)
            .HasColumnName("UND_ID")
            .HasColumnOrder(1)
            .ValueGeneratedOnAdd()
            .HasComment("Chave primária da unidade");

        builder.Property(u => u.CondominioId)
            .HasColumnName("CON_ID")
            .HasColumnOrder(2)
            .HasComment("Chave estrangeira do condomínio");

        builder.Property(u => u.Numero)
            .HasColumnName("UND_NUMERO")
            .HasColumnOrder(3)
            .IsRequired()
            .HasColumnType("varchar(10)")
            .HasComment("Número da unidade");

        builder.Property(u => u.Andar)
            .HasColumnName("UND_ANDAR")
            .HasColumnOrder(4)
            .HasColumnType("varchar(10)")
            .HasComment("Andar da unidade");

        builder.Property(u => u.Bloco)
            .HasColumnName("UND_BLOCO")
            .HasColumnOrder(5)
            .HasColumnType("varchar(10)")
            .HasComment("Bloco da unidade");

        builder.Property(u => u.DataCriacao)
            .HasColumnName("UND_DATA_CRIACAO")
            .HasColumnOrder(6)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()")
            .HasComment("Data de criação da unidade")
            .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);

        builder.Property(u => u.DataAtualizacao)
            .HasColumnName("UND_DATA_ATUALIZACAO")
            .HasColumnOrder(7)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()")
            .HasComment("Data da última atualização da unidade")
            .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Save);

        builder.HasOne(u => u.Condominio)
            .WithMany(c => c.Unidades)
            .HasForeignKey(u => u.CondominioId)
            .HasConstraintName("FK_UNIDADES_CONDOMINIOS")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(u => u.Moradores)
            .WithOne(m => m.Unidade)
            .HasForeignKey(m => m.UnidadeId)
            .HasConstraintName("FK_MORADORES_UNIDADES")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(u => new { u.CondominioId, u.Numero, u.Bloco })
            .HasDatabaseName("IX_UNIDADES_CONDOMINIO_NUMERO_BLOCO");
    }
}