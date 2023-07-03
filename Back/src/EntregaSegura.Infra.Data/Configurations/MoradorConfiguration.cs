using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Infra.Data.Configurations;

public class MoradorConfiguration : IEntityTypeConfiguration<Morador>
{
    public void Configure(EntityTypeBuilder<Morador> builder)
    {
        builder.ToTable("TB_MORADORES");

        builder.HasKey(m => m.Id)
            .HasName("PK_MORADORES");

        builder.Property(m => m.Id)
            .HasColumnName("MOR_ID")
            .HasColumnOrder(1)
            .ValueGeneratedOnAdd()
            .HasComment("Chave primária do morador");

        builder.Property(m => m.Ramal)
            .HasColumnName("MOR_RAMAL")
            .HasColumnOrder(2)
            .IsRequired()
            .HasColumnType("int")
            .HasComment("Ramal do morador");

        builder.Property(m => m.UnidadeId)
            .HasColumnName("MOR_UNIDADE_ID")
            .HasColumnOrder(3)
            .IsRequired()
            .HasComment("Chave estrangeira da unidade do morador");

        builder.Property(m => m.PessoaId)
            .HasColumnName("MOR_PESSOA_ID")
            .HasColumnOrder(4)
            .IsRequired()
            .HasComment("Chave estrangeira da pessoa do morador");

        builder.Property(m => m.DataCriacao)
            .HasColumnName("MOR_DATA_CRIACAO")
            .HasColumnOrder(5)
            .IsRequired()
            .HasColumnType("datetime")
            .HasDefaultValueSql("GETDATE()")
            .HasComment("Data de criação do morador")
            .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);

        builder.Property(m => m.DataAtualizacao)
            .HasColumnName("MOR_DATA_ATUALIZACAO")
            .HasColumnOrder(6)
            .IsRequired()
            .HasColumnType("datetime")
            .HasDefaultValueSql("GETDATE()")
            .HasComment("Data da última atualização do morador")
            .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Save);

        builder.HasOne(m => m.Unidade)
            .WithMany(u => u.Moradores)
            .HasForeignKey(m => m.UnidadeId)
            .HasConstraintName("FK_MORADORES_UNIDADES")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.Pessoa)
            .WithOne()
            .HasForeignKey<Morador>(m => m.PessoaId)
            .HasConstraintName("FK_MORADORES_PESSOA")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(m => m.Entregas)
            .WithOne(e => e.Morador)
            .HasForeignKey(e => e.MoradorId)
            .HasConstraintName("FK_MORADORES_ENTREGAS")
            .OnDelete(DeleteBehavior.Restrict);
    }
}