namespace EntregaSegura.Domain.Entities;

public sealed class Morador : Pessoa
{
    private readonly IList<Entrega> _entregas;

    public Morador(
        string nome, 
        string cpf, 
        string telefone, 
        string email, 
        string ramal, 
        string foto, 
        int unidadeId, 
        int userId) : base(nome, cpf, telefone, email, foto, userId)
    {
        Ramal = string.IsNullOrEmpty(ramal) ? null : ramal;
        UnidadeId = unidadeId;

        _entregas = new List<Entrega>();
    }

    public string Ramal { get; private set; }
    
    public int UnidadeId { get; private set; }
    public Unidade Unidade { get; private set; }

    public IReadOnlyCollection<Entrega> Entregas => _entregas.ToArray();

    public void DefinirUsuario(int userId)
    {
        UserId = userId;
    }
}
