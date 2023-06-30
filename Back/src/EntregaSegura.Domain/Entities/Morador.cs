namespace EntregaSegura.Domain.Entities;

public sealed class Morador : Pessoa
{
    private readonly IList<Entrega> _entregas;

    public Morador(
        string nome, 
        string cpf, 
        string telefone, 
        string email, 
        int ramal,
        int unidadeId) : base(nome, cpf, telefone, email)
    {
        Ramal = ramal;
        UnidadeId = unidadeId;

        _entregas = new List<Entrega>();
    }

    public int Ramal { get; private set; }
    
    public int UnidadeId { get; private set; }
    public Unidade Unidade { get; private set; }

    public IReadOnlyCollection<Entrega> Entregas => _entregas.ToArray();
}
