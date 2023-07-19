namespace EntregaSegura.Domain.Entities;

public sealed class Morador : EntityBase
{
    private readonly IList<Entrega> _entregas;

    public Morador(int ramal, int unidadeId, int pessoaId)
    {
        Ramal = ramal;
        UnidadeId = unidadeId;
        PessoaId = pessoaId;

        _entregas = new List<Entrega>();
    }

    public int Ramal { get; private set; }
    public int UnidadeId { get; private set; }
    public int PessoaId { get; private set; }

    // Propriedades de Navegação
    public Unidade Unidade { get; private set; }
    public Pessoa Pessoa { get; private set; }
    public IReadOnlyCollection<Entrega> Entregas => _entregas.ToArray();
}
