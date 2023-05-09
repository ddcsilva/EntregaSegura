namespace EntregaSegura.Domain.Entities;

/// <summary>
/// Classe que representa a base de todas as entidades do sistema.
/// </summary>
public abstract class BaseEntity
{
    protected BaseEntity()
    {
        Id = Guid.NewGuid();
        DataCriacao = DateTime.Now;
        DataAtualizacao = DateTime.Now;
    }

    public Guid Id { get; private set; }
    public DateTime DataCriacao { get; private set; }
    public DateTime DataAtualizacao { get; private set; }
    public DateTime? DataExclusao { get; private set; }

    public void AtualizarDataAtualizacao()
    {
        DataAtualizacao = DateTime.Now;
    }

    public void MarcarComoExcluido()
{
    DataExclusao = DateTime.Now;
}

    public bool Equals(BaseEntity? outraEntidade)
    {
        if (outraEntidade == null)
            return false;

        if (ReferenceEquals(this, outraEntidade))
            return true;

        if (GetType() != outraEntidade.GetType())
            return false;

        return Id == outraEntidade.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}