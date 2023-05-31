using EntregaSegura.Application.DTOs.Unidades;

namespace EntregaSegura.Application.DTOs.Condominios;

public class CondominioUnidadesDTO : CondominioDTO
{
    public IEnumerable<UnidadeDTO> Unidades { get; set; }
}