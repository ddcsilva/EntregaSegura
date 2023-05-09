namespace EntregaSegura.Domain.Entities;

/// <summary>
/// Classe que representa uma unidade.
/// </summary>
public sealed class Unidade : BaseEntity
{
    public Unidade()
    {
        Moradores = new List<Morador>();
    }

    public string? Numero { get; set; }
    public string? Bloco { get; set; }
    public Guid CondominioId { get; set; }
    
    // Uma unidade pertence a apenas um condomínio
    public Condominio? Condominio { get; set; }

    // Uma unidade pode ter vários moradores
    public ICollection<Morador> Moradores { get; set; } 
}