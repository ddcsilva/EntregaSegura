namespace EntregaSegura.Domain.Entities;

public sealed class Transportadora : Empresa
{
    private readonly IList<Entrega> _entregas;

    public Transportadora(
        string nome, 
        string cnpj, 
        string telefone, 
        string email) : base(nome, cnpj, telefone, email)
    {
        _entregas = new List<Entrega>();
    }

    public IReadOnlyCollection<Entrega> Entregas => _entregas.ToArray();
}