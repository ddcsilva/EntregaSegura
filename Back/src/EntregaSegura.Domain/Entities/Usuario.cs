using EntregaSegura.Domain.Entities.Enums;

namespace EntregaSegura.Domain.Entities;

public sealed class Usuario : EntityBase {

    public Usuario(string nome, string login, string senha, string email, PerfilUsuario perfil)
    {
        Nome = nome;
        Login = login;
        Senha = senha;
        Email = email;
        Perfil = perfil;
    }

    public string Nome { get; private set; }
    public string Login { get; private set; }
    public string Senha { get; private set; }
    public string Email { get; private set; }
    public string Token { get; private set; }
    public PerfilUsuario Perfil { get; private set; }
}