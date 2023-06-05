using EntregaSegura.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntregaSegura.Infra.Data.Configurations;

/// <summary>
/// Classe que representa a configuração da entidade Entrega
/// </summary>
public class EntregaConfiguration : IEntityTypeConfiguration<Entrega>
{
    public void Configure(EntityTypeBuilder<Entrega> builder)
    {
        builder.ToTable("TB_ENTREGAS");

        builder.HasKey(e => e.Id)
            .HasName("PK_ENTREGAS");

        builder.Property(e => e.Id)
            .HasColumnName("ETG_ID")
            .HasColumnOrder(1)
            .ValueGeneratedOnAdd()
            .HasComment("Chave primária da entrega");

        builder.Property(e => e.TransportadoraId)
            .HasColumnName("TRP_ID")
            .HasColumnOrder(2)
            .IsRequired()
            .HasComment("Chave estrangeira da transportadora");

        builder.Property(e => e.FuncionarioId)
            .HasColumnName("FUN_ID")
            .HasColumnOrder(3)
            .IsRequired()
            .HasComment("Chave estrangeira do funcionário");

        builder.Property(e => e.MoradorId)
            .HasColumnName("MOR_ID")
            .HasColumnOrder(4)
            .IsRequired()
            .HasComment("Chave estrangeira do morador");

        builder.Property(e => e.DataRecebimento)
            .HasColumnName("ETG_DATA_RECEBIMENTO")
            .HasColumnOrder(5)
            .HasColumnType("datetime")
            .IsRequired()
            .HasComment("Data de recebimento da entrega");

        builder.Property(e => e.DataRetirada)
            .HasColumnName("ETG_DATA_RETIRADA")
            .HasColumnOrder(6)
            .HasColumnType("datetime")
            .HasComment("Data de retirada da entrega");

        builder.Property(e => e.Descricao)
            .HasColumnName("ETG_DESCRICAO")
            .HasColumnOrder(7)
            .HasColumnType("varchar(200)")
            .HasComment("Descrição da entrega");

        builder.Property(e => e.Observacao)
            .HasColumnName("ETG_OBSERVACAO")
            .HasColumnOrder(8)
            .HasColumnType("varchar(200)")
            .HasComment("Observação da entrega");

        builder.Property(e => e.Status)
            .HasColumnName("ETG_STATUS")
            .HasColumnOrder(9)
            .IsRequired()
            .HasComment("Status da entrega");

        builder.Property(e => e.DataCriacao)
            .HasColumnName("ETG_DATA_CRIACAO")
            .HasColumnOrder(10)
            .IsRequired()
            .HasColumnType("datetime")
            .HasDefaultValueSql("GETDATE()")
            .HasComment("Data de criação da entrega")
            .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);

        builder.Property(e => e.DataAtualizacao)
            .HasColumnName("ETG_DATA_ATUALIZACAO")
            .HasColumnOrder(11)
            .IsRequired()
            .HasColumnType("datetime")
            .HasDefaultValueSql("GETDATE()")
            .HasComment("Data da última atualização da entrega")
            .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Save);

        // Uma entrega pertence a apenas uma transportadora e uma transportadora pode ter várias entregas
        builder.HasOne(e => e.Transportadora)
            .WithMany(t => t.Entregas)
            .HasForeignKey(e => e.TransportadoraId)
            .HasConstraintName("FK_ENTREGA_TRANSPORTADORA")
            .OnDelete(DeleteBehavior.Restrict);

        // Uma entrega pertence a apenas um morador e um morador pode ter várias entregas
        builder.HasOne(e => e.Morador)
            .WithMany(m => m.Entregas)
            .HasForeignKey(e => e.MoradorId)
            .HasConstraintName("FK_ENTREGA_MORADOR")
            .OnDelete(DeleteBehavior.Restrict);

        // Uma entrega só pode ser manipulada por um funcionário e um funcionário pode manipular várias entregas
        builder.HasOne(e => e.Funcionario)
            .WithMany(f => f.Entregas)
            .HasForeignKey(e => e.FuncionarioId)
            .HasConstraintName("FK_ENTREGA_FUNCIONARIO")
            .OnDelete(DeleteBehavior.Restrict);
    }
}
