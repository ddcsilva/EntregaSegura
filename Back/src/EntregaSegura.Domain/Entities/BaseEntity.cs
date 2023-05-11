namespace EntregaSegura.Domain.Entities;

public abstract class BaseEntity
{
    protected BaseEntity()
    {
        Id = Guid.NewGuid();
        DataCriacao = DateTime.Now;
        DataAtualizacao = DateTime.Now;
    }

    public Guid Id { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime DataAtualizacao { get; set; }
    public DateTime? DataExclusao { get; set; }
    public bool Excluido { get; set; } = false;

    public bool Equals(BaseEntity outraEntidade)
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

    public override string ToString()
    {
        return $"{GetType().Name} [Id={Id}]";
    }
}