using EntregaSegura.Domain.Entities.Enums;

namespace EntregaSegura.Domain.Entities;

public sealed class Funcionario : EntityBase
{
    private readonly IList<Entrega> _entregas;

    public Funcionario(CargoFuncionario cargo, DateTime dataAdmissao, DateTime? dataDemissao, int condominioId, int pessoaId)
    {
        Cargo = cargo;
        DataAdmissao = dataAdmissao;
        DataDemissao = dataDemissao;
        CondominioId = condominioId;
        PessoaId = pessoaId;

        _entregas = new List<Entrega>();
    }

    public DateTime DataAdmissao { get; private set; }
    public DateTime? DataDemissao { get; private set; }
    public CargoFuncionario Cargo { get; private set; }

    public int CondominioId { get; private set; }
    public Condominio Condominio { get; private set; }

    public int PessoaId { get; private set; }
    public Pessoa Pessoa { get; private set; }

    public IReadOnlyCollection<Entrega> Entregas => _entregas.ToList();
}