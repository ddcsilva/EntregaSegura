using System.Net;
using System.Net.Mail;
using EntregaSegura.Application.Interfaces;

namespace EntregaSegura.Application.Services;

public class EmailService : IEmailService
{
    public async Task EnviarEmailAsync(string email, string assunto, string mensagem)
    {
        using (var client = new SmtpClient("smtp.gmail.com", 587))
        {
            client.Credentials = new NetworkCredential("sistema.entrega.segura@gmail.com", "hkkiwmqzvkkmzggx");
            client.EnableSsl = true;

            await client.SendMailAsync("sistema.entrega.segura@gmail.com", email, assunto, mensagem);
        }
    }
}