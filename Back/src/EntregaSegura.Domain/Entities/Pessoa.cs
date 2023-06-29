namespace EntregaSegura.Domain.Entities;

public abstract class Pessoa : EntityBase
{
    protected Pessoa(string nome, string cpf, string telefone, string email, string foto)
    {
        Nome = nome;
        Cpf = cpf;
        Telefone = telefone;
        Email = email;
        Foto = string.IsNullOrEmpty(foto) ? null : foto;
    }

    public string Nome { get; protected set; }
    public string Cpf { get; protected set; }
    public string Telefone { get; protected set; }
    public string Email { get; protected set; }
    public string Foto { get; protected set; }
}