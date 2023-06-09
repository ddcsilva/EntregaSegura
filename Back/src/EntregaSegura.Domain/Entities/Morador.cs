namespace EntregaSegura.Domain.Entities;

public sealed class Morador : BaseEntity
{
    private readonly IList<Entrega> _entregas;

    public Morador(string nome, string cpf, string email, string telefone, string ramal, string foto, int unidadeId)
    {
        Nome = nome;
        Cpf = cpf;
        Email = email;
        Telefone = telefone;
        Ramal = ramal;
        Foto = foto;
        UnidadeId = unidadeId;

        _entregas = new List<Entrega>();
    }

    public string Nome { get; private set; }
    public string Cpf { get; private set; }
    public string Email { get; private set; }
    public string Telefone { get; private set; }
    public string Ramal { get; private set; }
    public string Foto { get; private set; }

    public int UnidadeId { get; private set; }
    public Unidade Unidade { get; private set; }

    public IReadOnlyCollection<Entrega> Entregas => _entregas.ToArray();
}
