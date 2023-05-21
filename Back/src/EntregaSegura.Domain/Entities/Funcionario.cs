using EntregaSegura.Domain.Entities.Enums;

namespace EntregaSegura.Domain.Entities;

public sealed class Funcionario : BaseEntity
{
    public Funcionario()
    {
        Entregas = new List<Entrega>();
        DataAdmissao = DateTime.Now;
    }

    public int CondominioId { get; set; }
    public string Nome { get; set; }
    public string CPF { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public CargoFuncionario Cargo { get; set; }
    public DateTime DataAdmissao { get; set; }
    public DateTime? DataDemissao { get; set; }

    // Um funcionário pertence a apenas um condomínio
    public Condominio Condominio { get; set; }

    // Um funcionário pode manipular várias entregas
    public ICollection<Entrega> Entregas { get; set; }
}