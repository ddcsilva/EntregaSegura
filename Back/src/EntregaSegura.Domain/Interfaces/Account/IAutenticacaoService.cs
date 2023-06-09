namespace EntregaSegura.Domain.Interfaces.Account;

public interface IAutenticacaoService
{
    Task<bool> AutenticarAsync(string email, string senha);

    Task<bool> RegistrarAsync(string email, string senha, int moradorId);

    public string GerarSenhaAleatoria();

    Task LogoutAsync();
}