namespace EntregaSegura.Domain.Entities;

public abstract class EntityBase
{
    protected EntityBase()
    {
        DataCriacao = DateTime.Now;
        DataAtualizacao = DateTime.Now;
    }

    public int Id { get; protected set; }
    public DateTime DataCriacao { get; private set; }
    public DateTime DataAtualizacao { get; private set; }
}
