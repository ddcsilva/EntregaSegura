using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Models;

namespace EntregaSegura.Application.Notifications;

public class NotificadorErros : INotificadorErros
{
    private readonly List<NotificacaoErros> _notificacoes;

    public NotificadorErros()
    {
        _notificacoes = new List<NotificacaoErros>();
    }

    public void Handle(NotificacaoErros notificacao)
    {
        _notificacoes.Add(notificacao);
    }

    public List<NotificacaoErros> ObterNotificacoes()
    {
        return _notificacoes;
    }
    
    public bool TemNotificacao()
    {
        return _notificacoes.Any();
    }
}