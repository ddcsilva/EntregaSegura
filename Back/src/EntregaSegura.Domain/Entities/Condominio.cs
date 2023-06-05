namespace EntregaSegura.Domain.Entities;

public sealed class Condominio : Empresa
{
    private readonly IList<Unidade> _unidades;
    private readonly IList<Funcionario> _funcionarios;

    public Condominio(
        string nome, 
        string cnpj, 
        string telefone, 
        string email,
        int quantidadeBlocos,
        int quantidadeUnidades,
        int quantidadeAndares,
        string logradouro,
        int numero,
        string cep,
        string bairro,
        string cidade,
        string estado) : base(nome, cnpj, telefone, email)
    {
        QuantidadeBlocos = quantidadeBlocos;
        QuantidadeUnidades = quantidadeUnidades;
        QuantidadeAndares = quantidadeAndares;
        Logradouro = logradouro;
        Numero = numero;
        Cep = cep;
        Bairro = bairro;
        Cidade = cidade;
        Estado = estado;

        _unidades = new List<Unidade>();
        _funcionarios = new List<Funcionario>();
    }

    public int QuantidadeBlocos { get; set; }
    public int QuantidadeUnidades { get; set; }
    public int QuantidadeAndares { get; set; }
    public string Logradouro { get; set; }
    public int Numero { get; set; }
    public string Cep { get; set; }
    public string Bairro { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }

    public ICollection<Unidade> Unidades { get; set; }
    public ICollection<Funcionario> Funcionarios { get; set; }
}