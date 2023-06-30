using EntregaSegura.Domain.Entities.Enums;

namespace EntregaSegura.Domain.Entities;

public sealed class Funcionario : Pessoa
{
    private readonly IList<Entrega> _entregas;

    public Funcionario(
        string nome, 
        string cpf, 
        string telefone, 
        string email,
        CargoFuncionario cargo, 
        DateTime dataAdmissao, 
        int condominioId) : base(nome, cpf, telefone, email)
    {
        Cargo = cargo;
        DataAdmissao = dataAdmissao;
        CondominioId = condominioId;

        _entregas = new List<Entrega>();
    }

    public CargoFuncionario Cargo { get; private set; }
    public DateTime DataAdmissao { get; private set; }
    public DateTime? DataDemissao { get; private set; }

    public int CondominioId { get; private set; }
    public Condominio Condominio { get; private set; }

    public IReadOnlyCollection<Entrega> Entregas => _entregas.ToList();
}