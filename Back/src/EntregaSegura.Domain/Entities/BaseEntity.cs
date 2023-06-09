namespace EntregaSegura.Domain.Entities;

public abstract class BaseEntity
{
    protected BaseEntity()
    {
        DataCriacao = DateTime.Now;
        DataAtualizacao = DateTime.Now;
    }

    public int Id { get; protected set; }
    public DateTime DataCriacao { get; private set; }
    public DateTime DataAtualizacao { get; private set; }
}
