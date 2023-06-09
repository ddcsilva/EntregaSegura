using System.ComponentModel.DataAnnotations;

namespace EntregaSegura.Application.DTOs;

public class PessoaDTO
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 2)]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public string Cpf { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "O Telefone deve conter apenas números.")]
    [StringLength(11, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 10)]
    public string Telefone { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [EmailAddress(ErrorMessage = "O campo {0} é inválido.")]
    public string Email { get; set; }
}