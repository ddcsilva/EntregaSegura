namespace EntregaSegura.Domain.Entities;

public class Unidade : BaseEntity
{
    public Unidade()
    {
        Moradores = new List<Morador>();
    }

    public int CondominioId { get; set; }
    public int Numero { get; set; }
    public int Andar { get; set; }
    public string Bloco { get; set; }
    
    // Uma unidade pertence a apenas um condomínio
    public Condominio Condominio { get; set; }

    // Uma unidade pode ter vários moradores
    public ICollection<Morador> Moradores { get; set; } 
}