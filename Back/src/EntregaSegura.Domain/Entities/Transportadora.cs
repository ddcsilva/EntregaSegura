namespace EntregaSegura.Domain.Entities;

public sealed class Transportadora : Empresa
{
    public Transportadora()
    {
        Entregas = new List<Entrega>();
    }

    // Uma transportadora pode realizar várias entregas
    public ICollection<Entrega> Entregas { get; set; }
}