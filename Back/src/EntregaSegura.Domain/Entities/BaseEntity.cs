namespace EntregaSegura.Domain.Entities;

public abstract class BaseEntity
{
    protected BaseEntity()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
    public bool Excluido { get; set; } = false;
    private DateTime _dataCriacao;
    public DateTime DataCriacao
    {
        get => _dataCriacao.ToLocalTime();
        set => _dataCriacao = value.ToLocalTime();
    }
    private DateTime _dataAtualizacao;
    public DateTime DataAtualizacao
    {
        get => _dataAtualizacao.ToLocalTime();
        set => _dataAtualizacao = value.ToLocalTime();
    }
    private DateTime? _dataExclusao;
    public DateTime? DataExclusao
    {
        get => _dataExclusao?.ToLocalTime();
        set => _dataExclusao = value?.ToLocalTime();
    }

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