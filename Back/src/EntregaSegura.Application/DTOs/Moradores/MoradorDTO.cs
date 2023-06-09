namespace EntregaSegura.Application.DTOs;

public class MoradorDTO
{
    public int Id { get; set; }

    public string Nome { get; set; }

    public string CPF { get; set; }

    public string Email { get; set; }

    public string Telefone { get; set; }

    public string Ramal { get; set; }

    public int UnidadeId { get; set; }

    public string NomeCondominio { get; set; }

    public string BlocoAndarUnidade { get; set; }
}