namespace EntregaSegura.Domain.Entities;

public sealed class Morador : BaseEntity
{
    public Morador()
    {
        Entregas = new List<Entrega>();
    }

    public int UnidadeId { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string Ramal { get; set; }
    public string Foto { get; set; }

    public Unidade Unidade { get; set; }
    public ICollection<Entrega> Entregas { get; set; }
}
