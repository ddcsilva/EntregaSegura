using System.ComponentModel.DataAnnotations;

namespace EntregaSegura.Application.DTOs;

public class UnidadeDTO
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public int Bloco { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public int Andar { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public int Numero { get; set; }

    public string DescricaoUnidade { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public int CondominioId { get; set; }

    public string NomeCondominio { get; set; }

    public int QuantidadeMoradores { get; set; }
}