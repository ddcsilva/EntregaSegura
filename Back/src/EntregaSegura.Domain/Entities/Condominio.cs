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

    public int QuantidadeBlocos { get; private set; }
    public int QuantidadeUnidades { get; private set; }
    public int QuantidadeAndares { get; private set; }
    public string Logradouro { get; private set; }
    public int Numero { get; private set; }
    public string Cep { get; private set; }
    public string Bairro { get; private set; }
    public string Cidade { get; private set; }
    public string Estado { get; private set; }

    // Propriedades de Navegação
    public IReadOnlyCollection<Unidade> Unidades => _unidades.ToArray();
    public IReadOnlyCollection<Funcionario> Funcionarios => _funcionarios.ToArray();
}