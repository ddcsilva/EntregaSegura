using System.ComponentModel.DataAnnotations;

namespace EntregaSegura.Application.DTOs;

public class EntregaDTO
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 2)]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public DateTime DataRecebimento { get; set; }

    public DateTime? DataRetirada { get; set; }

    public string Status { get; set; }

    public int TransportadoraId { get; set; }

    public string NomeTransportadora { get; set; }

    public int MoradorId { get; set; }

    public string NomeMorador { get; set; }
    public string EmailMorador { get; set; }

    public int FuncionarioId { get; set; }

    public string NomeFuncionario { get; set; }

    public string DescricaoUnidade { get; set; }
}