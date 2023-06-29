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

        builder.Property(m => m.Nome)
            .HasColumnName("MOR_NOME")
            .HasColumnOrder(2)
            .IsRequired()
            .HasColumnType("varchar(100)")
            .HasComment("Nome do morador");

        builder.Property(m => m.Cpf)
            .HasColumnName("MOR_CPF")
            .HasColumnOrder(3)
            .IsRequired()
            .HasColumnType("varchar(11)")
            .HasComment("CPF do morador");

        builder.Property(m => m.Telefone)
            .HasColumnName("MOR_TELEFONE")
            .HasColumnOrder(4)
            .IsRequired()
            .HasColumnType("varchar(11)")
            .HasComment("Telefone do morador");

        builder.Property(m => m.Email)
            .HasColumnName("MOR_EMAIL")
            .HasColumnOrder(5)
            .IsRequired()
            .HasColumnType("varchar(100)")
            .HasComment("Email do morador");

        builder.Property(m => m.Ramal)
            .HasColumnName("MOR_RAMAL")
            .HasColumnOrder(6)
            .HasColumnType("varchar(5)")
            .HasComment("Ramal do morador");

        builder.Property(m => m.Foto)
            .HasColumnName("MOR_FOTO")
            .HasColumnOrder(7)
            .HasColumnType("varchar(100)")
            .HasComment("Foto do morador");

        builder.Property(m => m.UnidadeId)
            .HasColumnName("MOR_UNIDADE_ID")
            .HasColumnOrder(8)
            .IsRequired()
            .HasComment("Chave estrangeira da unidade do morador");

        builder.Property(m => m.DataCriacao)
            .HasColumnName("MOR_DATA_CRIACAO")
            .HasColumnOrder(10)
            .IsRequired()
            .HasColumnType("datetime")
            .HasDefaultValueSql("GETDATE()")
            .HasComment("Data de criação do morador")
            .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);

        builder.Property(m => m.DataAtualizacao)
            .HasColumnName("MOR_DATA_ATUALIZACAO")
            .HasColumnOrder(11)
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

        builder.HasMany(m => m.Entregas)
            .WithOne(e => e.Morador)
            .HasForeignKey(e => e.MoradorId)
            .HasConstraintName("FK_MORADORES_ENTREGAS")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(m => m.Cpf)
            .HasDatabaseName("IX_MORADORES_CPF")
            .IsUnique();

        builder.HasIndex(m => m.UnidadeId)
            .HasDatabaseName("IX_MORADORES_UNIDADE_ID");

        builder.HasIndex(m => m.Email)
            .HasDatabaseName("IX_MORADORES_EMAIL")
            .IsUnique();
    }
}