namespace EntregaSegura.Domain.Entities;

public sealed class Morador : BaseEntity
{
    public Morador()
    {
        Entregas = new List<Entrega>();
    }

    public Guid UnidadeId { get; set; }
    public string Nome { get; set; }
    public string CPF { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string Ramal { get; set; }
    public string Foto { get; set; }

    // Um morador pertence a apenas uma unidade
    public Unidade Unidade { get; set; }

    // Um morador pode receber várias entregas
    public ICollection<Entrega> Entregas { get; set; }
}
