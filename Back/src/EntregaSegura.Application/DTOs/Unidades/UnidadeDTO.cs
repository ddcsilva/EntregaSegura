using System.ComponentModel.DataAnnotations;

namespace EntregaSegura.Application.DTOs.Unidades;

public class UnidadeDTO
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(5, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 1)]
    public string Bloco { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public int Andar { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(5, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 1)]
    public string Numero { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public int CondominioId { get; set; }

    public string NomeCondominio { get; set; }

    public IEnumerable<MoradorDTO> Moradores { get; set; }
}