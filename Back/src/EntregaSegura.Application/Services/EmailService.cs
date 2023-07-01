using System.Net;
using System.Net.Mail;
using EntregaSegura.Application.Interfaces;
using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Application.Services;

public class EmailService : BaseService, IEmailService
{
    public EmailService(INotificadorErros notificadorErros) : base(notificadorErros) { }

    public async Task<bool> EnviarEmailAsync(string email, string assunto, string mensagem)
    {
        using (var client = new SmtpClient("smtp.gmail.com", 587))
        {
            try
            {
                client.Credentials = new NetworkCredential("sistema.entrega.segura@gmail.com", "hkkiwmqzvkkmzggx");
                client.EnableSsl = true;

                await client.SendMailAsync("sistema.entrega.segura@gmail.com", email, assunto, mensagem);

                return true;
            }
            catch (Exception)
            {
                Notificar("Não foi possível enviar o email.");
                return false;
            }
        }
    }
}