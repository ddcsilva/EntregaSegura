namespace EntregaSegura.Domain.Entities;

public abstract class Empresa : EntityBase
{
    protected Empresa(string nome, string cnpj, string telefone, string email)
    {
        Nome = nome;
        Cnpj = string.IsNullOrEmpty(cnpj) ? null : cnpj;
        Telefone = string.IsNullOrEmpty(telefone) ? null : telefone;
        Email = string.IsNullOrEmpty(email) ? null : email;
    }

    public string Nome { get; private set; }
    public string Cnpj { get; private set; }
    public string Telefone { get; private set; }
    public string Email { get; private set; }
}