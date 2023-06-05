using System.ComponentModel.DataAnnotations;

namespace EntregaSegura.Application.DTOs;

public class CondominioDTO
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 2)]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public string CNPJ { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "O Telefone deve conter apenas números.")]
    [StringLength(11, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 10)]
    public string Telefone { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [EmailAddress(ErrorMessage = "O campo {0} é inválido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 2)]
    public string Logradouro { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [Range(1, int.MaxValue, ErrorMessage = "O campo {0} deve ser maior que 0.")]
    public int Numero { get; set; }

    [StringLength(50, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.")]
    public string Complemento { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public string CEP { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(50, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 2)]
    public string Bairro { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(50, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 2)]
    public string Cidade { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(2, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 2)]
    public string Estado { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [Range(1, 8, ErrorMessage = "O campo {0} deve estar entre {1} e {2}.")]
    public int QuantidadeUnidades { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [Range(1, 40, ErrorMessage = "O campo {0} deve estar entre {1} e {2}.")]
    public int QuantidadeAndares { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [Range(1, 20, ErrorMessage = "O campo {0} deve estar entre {1} e {2}.")]
    public int QuantidadeBlocos { get; set; }
}