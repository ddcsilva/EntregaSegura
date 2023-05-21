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
    public DateTime? DataExclusao { get; private set; }
    public bool Excluido { get; private set; } = false;

    public void Atualizar()
    {
        DataAtualizacao = DateTime.Now;
    }

    public void Excluir()
    {
        Excluido = true;
        DataExclusao = DateTime.Now;
    }

    public void DefinirId(int id)
    {
        Id = id;
    }
}
