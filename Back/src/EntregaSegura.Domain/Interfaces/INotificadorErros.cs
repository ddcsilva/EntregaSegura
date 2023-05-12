using EntregaSegura.Domain.Models;

namespace EntregaSegura.Domain.Entities;

public interface INotificadorErros
{
    void Handle(NotificacaoErros notificacao);
    List<NotificacaoErros> ObterNotificacoes();
    bool TemNotificacao();
}