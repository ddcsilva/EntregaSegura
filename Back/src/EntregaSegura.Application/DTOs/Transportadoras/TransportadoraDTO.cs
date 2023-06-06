using System.ComponentModel.DataAnnotations;

namespace EntregaSegura.Application.DTOs;

public class TransportadoraDTO
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 2)]
    public string Nome { get; set; }

    public string Cnpj { get; set; }

    public string Telefone { get; set; }

    public string Email { get; set; }
}