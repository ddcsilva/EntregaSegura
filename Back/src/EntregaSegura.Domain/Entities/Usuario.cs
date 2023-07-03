using EntregaSegura.Domain.Entities.Enums;

namespace EntregaSegura.Domain.Entities;

public sealed class Usuario : EntityBase
{
    public Usuario(string login, string senha, string token, string foto, PerfilUsuario perfil, int pessoaId)
    {
        Login = login;
        Senha = senha;
        Token = token;
        Foto = foto;
        Perfil = perfil;
        PessoaId = pessoaId;
    }
    
    public string Login { get; private set; }
    public string Senha { get; private set; }
    public string Token { get; private set; }
    public string Foto { get; private set; }
    public PerfilUsuario Perfil { get; private set; }

    public int PessoaId { get; private set; }
    public Pessoa Pessoa { get; private set; }
}