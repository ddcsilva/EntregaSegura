using System.ComponentModel.DataAnnotations;

namespace EntregaSegura.Application.DTOs;

public class FuncionarioDTO
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public DateTime DataAdmissao { get; set; }

    public DateTime? DataDemissao { get; set; }
    public string Cargo { get; set; }
    public int CondominioId { get; set; }
    public string NomeCondominio { get; set; }
    public PessoaDTO Pessoa { get; set; }
}