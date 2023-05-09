using EntregaSegura.Entities.Models.Enums;

namespace EntregaSegura.Domain.Entities;

/// <summary>
/// Classe que representa um funcionário.
/// </summary>
public sealed class Funcionario : Usuario
{
    public Funcionario()
    {
        Cargo = CargoFuncionario.Porteiro;
        DataAdmissao = DateTime.Now;
        Entregas = new List<Entrega>();
    }

    public CargoFuncionario Cargo { get; set; }
    public DateTime DataAdmissao { get; set; }
    public DateTime? DataDemissao { get; set; }

    // Um funcionário pode manipular várias entregas
    public ICollection<Entrega> Entregas { get; set; }

    // Um funcionário pertence a apenas um condomínio
    public Guid CondominioId { get; set; }
    public Condominio? Condominio { get; set; }
}