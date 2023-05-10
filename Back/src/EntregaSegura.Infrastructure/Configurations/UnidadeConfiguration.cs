using EntregaSegura.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntregaSegura.Infrastructure.Configurations;

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

        builder.Property(u => u.Bloco)
            .HasColumnName("UND_BLOCO")
            .HasColumnOrder(4)
            .HasColumnType("varchar(10)")
            .HasComment("Bloco da unidade");

        builder.Property(u => u.DataCriacao)
            .HasColumnName("UND_DATA_CRIACAO")
            .HasColumnOrder(5)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()")
            .HasComment("Data de criação da unidade");

        builder.Property(u => u.DataAtualizacao)
            .HasColumnName("UND_DATA_ATUALIZACAO")
            .HasColumnOrder(6)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()")
            .HasComment("Data da última atualização da unidade");

        builder.Property(u => u.DataExclusao)
            .HasColumnName("UND_DATA_EXCLUSAO")
            .HasColumnOrder(7)
            .HasColumnType("datetime")
            .HasComment("Data da exclusão da unidade");

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
            .HasDatabaseName("IX_UNIDADES_CONDOMINIO_NUMERO_BLOCO")
            .IsUnique();
    }
}