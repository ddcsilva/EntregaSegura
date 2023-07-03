using System.ComponentModel.DataAnnotations;

namespace EntregaSegura.Application.DTOs;

public class MoradorDTO
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public int Ramal { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public int UnidadeId { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public int CondominioId { get; set; }

    public string NomeCondominio { get; set; }
    public string DescricaoUnidade { get; set; }
    public PessoaDTO Pessoa { get; set; }
}