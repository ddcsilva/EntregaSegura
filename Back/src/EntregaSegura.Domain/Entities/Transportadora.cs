namespace EntregaSegura.Domain.Entities;

/// <summary>
/// Classe que representa uma transportadora.
/// </summary>
public sealed class Transportadora : Empresa
{
    public Transportadora()
    {
        Entregas = new List<Entrega>();
    }

    // Uma transportadora pode realizar várias entregas
    public ICollection<Entrega> Entregas { get; set; }
}