namespace EntregaSegura.Domain.Entities;

public abstract class Pessoa : EntityBase
{
    public Pessoa(string nome, string cpf, string telefone, string email)
    {
        Nome = nome;
        Cpf = cpf;
        Telefone = telefone;
        Email = email;
    }

    public string Nome { get; private set; }
    public string Cpf { get; private set; }
    public string Telefone { get; private set; }
    public string Email { get; private set; }
}