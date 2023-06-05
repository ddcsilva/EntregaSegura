namespace EntregaSegura.Domain.Entities;

public abstract class Empresa : BaseEntity
{
    protected Empresa(string nome, string cnpj, string telefone, string email)
    {
        Nome = nome;
        Cnpj = cnpj;
        Telefone = telefone;
        Email = email;
    }

    public string Nome { get; private set; }
    public string Cnpj { get; private set; }
    public string Telefone { get; private set; }
    public string Email { get; private set; }
}