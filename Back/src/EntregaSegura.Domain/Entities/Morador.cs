namespace EntregaSegura.Domain.Entities;

/// <summary>
/// Classe que representa um Morador
/// </summary>
public sealed class Morador : Usuario
{
    public Morador()
    {
        Entregas = new List<Entrega>();
    }
    

    // Um morador pertence a apenas uma unidade
    public Guid UnidadeId { get; set; }
    public Unidade? Unidade { get; set; }

    // Um morador pode receber várias entregas
    public ICollection<Entrega> Entregas { get; set; }
}