using EntregaSegura.Domain.Entities.Enums;

namespace EntregaSegura.Domain.Entities;

public sealed class Funcionario : EntityBase
{
    private readonly IList<Entrega> _entregas;

    public Funcionario(string nome, string cpf, string email, string telefone, CargoFuncionario cargo)
    {
        Nome = nome;
        Cpf = cpf;
        Email = email;
        Telefone = telefone;
        Cargo = cargo;
        DataAdmissao = DateTime.Now;

        _entregas = new List<Entrega>();
    }

    public string Nome { get; private set; }
    public string Cpf { get; private set; }
    public string Email { get; private set; }
    public string Telefone { get; private set; }
    public CargoFuncionario Cargo { get; private set; }
    public DateTime DataAdmissao { get; private set; }
    public DateTime? DataDemissao { get; private set; }

    public int CondominioId { get; private set; }
    public Condominio Condominio { get; private set; }

    public IReadOnlyCollection<Entrega> Entregas => _entregas.ToList();
}