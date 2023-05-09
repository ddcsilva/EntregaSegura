using EntregaSegura.Entities.Models.Enums;

namespace EntregaSegura.Domain.Entities;

/// <summary>
/// Classe que representa um usuário.
/// </summary>
public abstract class Usuario : BaseEntity
{
    public Usuario()
    {
        Status = StatusConta.Aprovada;
    }

    public string? Nome { get; set; }
    public string? CPF { get; set; }
    public string? Email { get; set; }
    public string? Telefone { get; set; }
    public StatusConta Status { get; set; }
}