using System.ComponentModel.DataAnnotations;

namespace EntregaSegura.Application.DTOs;

public class EntregaDTO
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 2)]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public DateTime DataEntrega { get; set; }

    public DateTime? DataRetirada { get; set; }

    public Guid FuncionarioId { get; set; }

    public string NomeFuncionario { get; set; }

    public Guid UnidadeId { get; set; }

    public string BlocoUnidade { get; set; }

    public string NumeroUnidade { get; set; }

    public Guid TransportadoraId { get; set; }

    public string NomeTransportadora { get; set; }
}