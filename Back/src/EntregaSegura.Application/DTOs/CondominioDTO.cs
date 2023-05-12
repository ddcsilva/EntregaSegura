using System.ComponentModel.DataAnnotations;

namespace EntregaSegura.Application.DTOs;

public class CondominioDTO
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 2)]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(14, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 14)]
    public string CNPJ { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(8, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.")]
    public string CEP { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 2)]
    public string Logradouro { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(10, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.")]
    public string Numero { get; set; }

    [StringLength(100, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.")]
    public string Complemento { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 2)]
    public string Bairro { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 2)]
    public string Cidade { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(2, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 2)]
    public string Estado { get; set; }

    public IEnumerable<UnidadeDTO> Unidades { get; set; }

    public IEnumerable<FuncionarioDTO> Funcionarios { get; set; }
}