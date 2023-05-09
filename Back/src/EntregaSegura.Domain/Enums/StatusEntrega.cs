namespace EntregaSegura.Entities.Models.Enums;

/// <summary>
/// Enum que representa o status de uma entrega.
/// </summary>
public enum StatusEntrega
{
    Recebida = 1,
    Notificada = 2,
    AguardandoRetirada = 3,
    Retirada = 4,
    EntregaAtrasada = 5
}