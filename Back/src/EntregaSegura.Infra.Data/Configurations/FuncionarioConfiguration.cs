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

        builder.Property(f => f.Nome)
            .HasColumnName("FUN_NOME")
            .HasColumnOrder(2)
            .IsRequired()
            .HasColumnType("varchar(100)")
            .HasComment("Nome do funcionário");

        builder.Property(f => f.Cpf)
            .HasColumnName("FUN_CPF")
            .HasColumnOrder(3)
            .IsRequired()
            .HasColumnType("varchar(11)")
            .HasComment("CPF do funcionário");

        builder.Property(f => f.Telefone)
            .HasColumnName("FUN_TELEFONE")
            .HasColumnOrder(4)
            .IsRequired()
            .HasColumnType("varchar(11)")
            .HasComment("Telefone do funcionário");

        builder.Property(f => f.Email)
            .HasColumnName("FUN_EMAIL")
            .HasColumnOrder(5)
            .IsRequired()
            .HasColumnType("varchar(100)")
            .HasComment("Email do funcionário");

        builder.Property(f => f.Cargo)
            .HasColumnName("FUN_CARGO")
            .HasColumnOrder(6)
            .IsRequired()
            .HasComment("Cargo do funcionário");

        builder.Property(m => m.Foto)
            .HasColumnName("MOR_FOTO")
            .HasColumnOrder(7)
            .HasColumnType("varchar(100)")
            .HasComment("Foto do morador");

        builder.Property(f => f.UserId)
            .HasColumnName("FUN_USER_ID")
            .HasColumnOrder(8)
            .IsRequired()
            .HasComment("Chave estrangeira do usuário");

        builder.Property(f => f.DataAdmissao)
            .HasColumnName("FUN_DATA_ADMISSAO")
            .HasColumnOrder(9)
            .IsRequired()
            .HasColumnType("datetime")
            .HasComment("Data de admissão do funcionário");

        builder.Property(f => f.DataDemissao)
            .HasColumnName("FUN_DATA_DEMISSAO")
            .HasColumnOrder(10)
            .HasColumnType("datetime")
            .HasComment("Data de demissão do funcionário");

        builder.Property(f => f.DataCriacao)
            .HasColumnName("FUN_DATA_CRIACAO")
            .HasColumnOrder(11)
            .IsRequired()
            .HasColumnType("datetime")
            .HasDefaultValueSql("GETDATE()")
            .HasComment("Data de criação do funcionário")
            .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);

        builder.Property(f => f.DataAtualizacao)
            .HasColumnName("FUN_DATA_ATUALIZACAO")
            .HasColumnOrder(12)
            .IsRequired()
            .HasColumnType("datetime")
            .HasDefaultValueSql("GETDATE()")
            .HasComment("Data da última atualização do funcionário")
            .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Save);

        builder.Property(f => f.CondominioId)
            .HasColumnName("FUN_CONDOMINIO_ID")
            .HasColumnOrder(13)
            .IsRequired()
            .HasComment("Chave estrangeira do condomínio");

        builder.HasOne(f => f.Condominio)
            .WithMany(c => c.Funcionarios)
            .HasForeignKey(f => f.CondominioId)
            .HasConstraintName("FK_FUNCIONARIO_CONDOMINIO")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(f => f.User)
            .WithOne()
            .HasForeignKey<Funcionario>(f => f.UserId)
            .HasConstraintName("FK_FUNCIONARIOS_USERS")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(f => f.Entregas)
            .WithOne(u => u.Funcionario)
            .HasForeignKey(u => u.FuncionarioId)
            .HasConstraintName("FK_FUNCIONARIO_ENTREGA")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(f => f.Cpf)
            .HasDatabaseName("IX_FUNCIONARIO_CPF")
            .IsUnique();

        builder.HasIndex(f => f.Email)
            .HasDatabaseName("IX_FUNCIONARIO_EMAIL")
            .IsUnique();
    }
}