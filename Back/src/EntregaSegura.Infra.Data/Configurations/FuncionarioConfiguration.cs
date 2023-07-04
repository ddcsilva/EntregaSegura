using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Infra.Data.Configurations;

public class FuncionarioConfiguration : IEntityTypeConfiguration<Funcionario>
{
    public void Configure(EntityTypeBuilder<Funcionario> builder)
    {
        builder.ToTable("TB_FUNCIONARIOS");

        builder.HasKey(f => f.Id)
            .HasName("PK_FUNCIONARIOS");

        builder.Property(f => f.Id)
            .HasColumnName("FUN_ID")
            .HasColumnOrder(1)
            .ValueGeneratedOnAdd()
            .HasComment("Chave primária do funcionário");

        builder.Property(f => f.DataAdmissao)
            .HasColumnName("FUN_DATA_ADMISSAO")
            .HasColumnOrder(2)
            .IsRequired()
            .HasColumnType("datetime")
            .HasComment("Data de admissão do funcionário");

        builder.Property(f => f.DataDemissao)
            .HasColumnName("FUN_DATA_DEMISSAO")
            .HasColumnOrder(3)
            .HasColumnType("datetime")
            .HasComment("Data de demissão do funcionário");

        builder.Property(f => f.Cargo)
            .HasColumnName("FUN_CARGO")
            .HasColumnOrder(4)
            .IsRequired()
            .HasComment("Cargo do funcionário");

        builder.Property(f => f.PessoaId)
            .HasColumnName("FUN_PESSOA_ID")
            .HasColumnOrder(5)
            .IsRequired()
            .HasComment("Chave estrangeira da pessoa");

        builder.Property(f => f.DataCriacao)
            .HasColumnName("FUN_DATA_CRIACAO")
            .HasColumnOrder(6)
            .IsRequired()
            .HasColumnType("datetime")
            .HasDefaultValueSql("GETDATE()")
            .HasComment("Data de criação do funcionário")
            .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);

        builder.Property(f => f.DataAtualizacao)
            .HasColumnName("FUN_DATA_ATUALIZACAO")
            .HasColumnOrder(7)
            .IsRequired()
            .HasColumnType("datetime")
            .HasDefaultValueSql("GETDATE()")
            .HasComment("Data da última atualização do funcionário")
            .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Save);

        builder.Property(f => f.CondominioId)
            .HasColumnName("FUN_CONDOMINIO_ID")
            .HasColumnOrder(8)
            .IsRequired()
            .HasComment("Chave estrangeira do condomínio");

        builder.HasOne(f => f.Condominio)
            .WithMany(c => c.Funcionarios)
            .HasForeignKey(f => f.CondominioId)
            .HasConstraintName("FK_FUNCIONARIO_CONDOMINIO")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(f => f.Entregas)
            .WithOne(u => u.Funcionario)
            .HasForeignKey(u => u.FuncionarioId)
            .HasConstraintName("FK_FUNCIONARIO_ENTREGA")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(f => f.Pessoa)
            .WithOne()
            .HasForeignKey<Funcionario>(f => f.PessoaId)
            .HasConstraintName("FK_FUNCIONARIOS_PESSOA")
            .OnDelete(DeleteBehavior.Restrict);
    }
}