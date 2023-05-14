using System.ComponentModel.DataAnnotations;

namespace EntregaSegura.Application.DTOs.Condominios;

public class CondominioFuncionariosDTO
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 2)]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(14, ErrorMessage = "O campo deve ter {1} caracteres.")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "O CEP deve conter apenas números.")]
    public string CNPJ { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(11, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 10)]
    [RegularExpression("^[0-9]*$", ErrorMessage = "O CEP deve conter apenas números.")]
    public string Telefone { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 2)]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 2)]
    public string Logradouro { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(10, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.")]
    public string Numero { get; set; }

    [StringLength(50, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.")]
    public string Complemento { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(8, ErrorMessage = "O campo deve ter {1} caracteres.")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "O CEP deve conter apenas números.")]
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

    public IEnumerable<FuncionarioDTO> Funcionarios { get; set; }
}