namespace EntregaSegura.Domain.Entities;

public sealed class Condominio : Empresa
{
    public Condominio()
    {
        Unidades = new List<Unidade>();
        Funcionarios = new List<Funcionario>();
    }

    public int QuantidadeBlocos { get; set; }
    public int QuantidadeUnidades { get; set; }
    public int QuantidadeAndares { get; set; }
    public string Logradouro { get; set; }
    public string Numero { get; set; }
    public string Complemento { get; set; }
    public string CEP { get; set; }
    public string Bairro { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }

    // Um condomínio possui várias unidades
    public ICollection<Unidade> Unidades { get; set; }

    // Um condomínio possui vários funcionários
    public ICollection<Funcionario> Funcionarios { get; set; }
}