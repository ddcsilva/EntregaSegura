namespace EntregaSegura.Domain.Models;

public class NotificacaoErros
{
    public NotificacaoErros(string mensagem)
    {
        Mensagem = mensagem;
    }

    public string Mensagem { get; }
}