namespace EntregaSegura.Application.Interfaces;

public interface IEmailService
{
    Task<bool> EnviarEmailAsync(string email, string assunto, string mensagem);
}