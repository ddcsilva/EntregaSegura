namespace EntregaSegura.Domain.Entities;

public sealed class Unidade : EntityBase
{
    private readonly IList<Morador> _moradores;

    public Unidade(int numero, int andar, int bloco, int condominioId)
    {
        Numero = numero;
        Andar = andar;
        Bloco = bloco;
        CondominioId = condominioId;
        
        _moradores = new List<Morador>();
    }

    public int Numero { get; private set; }
    public int Andar { get; private set; }
    public int Bloco { get; private set; }
    
    public int CondominioId { get; private set; }
    public Condominio Condominio { get; private set; }

    public IReadOnlyCollection<Morador> Moradores => _moradores.ToArray();
}