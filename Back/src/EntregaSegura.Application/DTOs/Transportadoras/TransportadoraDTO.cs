using System.ComponentModel.DataAnnotations;

namespace EntregaSegura.Application.DTOs.Transportadoras;

public class TransportadoraDTO
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 2)]
    public string Nome { get; set; }

    [StringLength(14, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 14)]
    public string CNPJ { get; set; }

    [StringLength(100, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 2)]
    public string Email { get; set; }

    [StringLength(11, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 11)]
    public string Telefone { get; set; }
}