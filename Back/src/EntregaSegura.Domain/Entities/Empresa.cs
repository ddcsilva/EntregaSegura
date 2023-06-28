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

    public string Nome { get; protected set; }
    public string Cnpj { get; protected set; }
    public string Telefone { get; protected set; }
    public string Email { get; protected set; }
}