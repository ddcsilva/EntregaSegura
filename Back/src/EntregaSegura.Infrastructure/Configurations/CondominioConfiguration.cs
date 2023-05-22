using EntregaSegura.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntregaSegura.Infrastructure.Configurations;

public class CondominioConfiguration : IEntityTypeConfiguration<Condominio>
{
    public void Configure(EntityTypeBuilder<Condominio> builder)
    {
        builder.ToTable("TB_CONDOMINIOS");

        builder.HasKey(c => c.Id)
            .HasName("PK_CONDOMINIOS");

        builder.Property(c => c.Id)
            .HasColumnName("CND_ID")
            .HasColumnOrder(1)
            .ValueGeneratedOnAdd()
            .HasComment("Chave primária do condomínio");

        builder.Property(c => c.QuantidadeUnidades)
            .HasColumnName("CND_QTD_UNIDADES")
            .HasColumnOrder(2)
            .IsRequired()
            .HasColumnType("int")
            .HasComment("Quantidade de unidades do condomínio");

        builder.Property(c => c.QuantidadeBlocos)
            .HasColumnName("CND_QTD_BLOCOS")
            .HasColumnOrder(3)
            .IsRequired()
            .HasColumnType("int")
            .HasComment("Quantidade de blocos do condomínio");

        builder.Property(c => c.QuantidadeAndares)
            .HasColumnName("CND_QTD_ANDARES")
            .HasColumnOrder(4)
            .IsRequired()
            .HasColumnType("int")
            .HasComment("Quantidade de andares do condomínio");

        builder.Property(c => c.Nome)
            .HasColumnName("CND_NOME")
            .HasColumnOrder(5)
            .IsRequired()
            .HasColumnType("varchar(100)")
            .HasComment("Nome do condomínio");

        builder.Property(c => c.CNPJ)
            .HasColumnName("CND_CNPJ")
            .HasColumnOrder(6)
            .IsRequired()
            .HasColumnType("varchar(14)")
            .HasComment("CNPJ do condomínio"); ;

        builder.Property(c => c.Telefone)
            .HasColumnName("CND_TELEFONE")
            .HasColumnOrder(7)
            .IsRequired()
            .HasColumnType("varchar(11)")
            .HasComment("Telefone do condomínio");

        builder.Property(c => c.Email)
            .HasColumnName("CND_EMAIL")
            .HasColumnOrder(8)
            .IsRequired()
            .HasColumnType("varchar(100)")
            .HasComment("E-mail do condomínio");

        builder.Property(e => e.Logradouro)
            .HasColumnName("CND_LOGRADOURO")
            .HasColumnOrder(9)
            .IsRequired()
            .HasColumnType("varchar(100)")
            .HasComment("Logradouro do endereço do condomínio");

        builder.Property(e => e.Numero)
            .HasColumnName("CND_NUMERO")
            .HasColumnOrder(10)
            .IsRequired()
            .HasColumnType("varchar(10)")
            .HasComment("Número do endereço do condomínio");

        builder.Property(e => e.Complemento)
            .HasColumnName("CND_COMPLEMENTO")
            .HasColumnOrder(11)
            .HasColumnType("varchar(50)")
            .HasComment("Complemento do endereço do condomínio");

        builder.Property(e => e.CEP)
            .HasColumnName("CND_CEP")
            .HasColumnOrder(12)
            .IsRequired()
            .HasColumnType("varchar(8)")
            .HasComment("CEP do endereço do condomínio");

        builder.Property(e => e.Bairro)
            .HasColumnName("CND_BAIRRO")
            .HasColumnOrder(13)
            .IsRequired()
            .HasColumnType("varchar(50)")
            .HasComment("Bairro do endereço do condomínio");

        builder.Property(e => e.Cidade)
            .HasColumnName("CND_CIDADE")
            .HasColumnOrder(14)
            .IsRequired()
            .HasColumnType("varchar(50)")
            .HasComment("Cidade do endereço do condomínio");

        builder.Property(e => e.Estado)
            .HasColumnName("CND_ESTADO")
            .HasColumnOrder(15)
            .IsRequired()
            .HasColumnType("varchar(2)")
            .HasComment("Estado do endereço do condomínio");

        builder.Property(c => c.DataCriacao)
            .HasColumnName("CND_DATA_CRIACAO")
            .HasColumnOrder(16)
            .IsRequired()
            .HasColumnType("datetime")
            .HasDefaultValueSql("GETDATE()")
            .HasComment("Data de criação do condomínio")
            .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);

        builder.Property(c => c.DataAtualizacao)
            .HasColumnName("CND_DATA_ATUALIZACAO")
            .HasColumnOrder(17)
            .IsRequired()
            .HasColumnType("datetime")
            .HasDefaultValueSql("GETDATE()")
            .HasComment("Data da última atualização do condomínio")
            .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Save);

        // Um condomínio possui várias unidades e uma unidade pertence a um condomínio
        builder.HasMany(c => c.Unidades)
            .WithOne(u => u.Condominio)
            .HasForeignKey(u => u.CondominioId)
            .HasConstraintName("FK_CONDOMINIO_UNIDADE")
            .OnDelete(DeleteBehavior.Restrict);

        // Um condomínio possui vários funcionários e um funcionário pertence a um condomínio
        builder.HasMany(c => c.Funcionarios)
            .WithOne(f => f.Condominio)
            .HasForeignKey(f => f.CondominioId)
            .HasConstraintName("FK_CONDOMINIO_FUNCIONARIO")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(c => c.CNPJ)
            .HasDatabaseName("IX_CONDOMINIOS_CNPJ")
            .IsUnique();

        builder.HasIndex(c => c.Email)
            .HasDatabaseName("IX_CONDOMINIOS_EMAIL")
            .IsUnique();

        builder.HasIndex(c => c.Nome)
            .HasDatabaseName("IX_CONDOMINIOS_NOME")
            .IsUnique();
    }
}
