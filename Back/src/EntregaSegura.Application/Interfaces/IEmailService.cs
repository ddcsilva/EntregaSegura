namespace EntregaSegura.Application.Interfaces;

public interface IEmailService
{
    Task EnviarEmailAsync(string email, string assunto, string mensagem);
}