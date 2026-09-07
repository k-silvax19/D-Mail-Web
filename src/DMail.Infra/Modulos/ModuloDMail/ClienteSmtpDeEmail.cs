using System.Net;
using System.Net.Mail;
using DMail.Dominio.Modulos.ModuloDMail;

namespace DMail.Infra.Modulos.ModuloDMail;

public class ClienteSmtpDeEmail : IClienteDeEmail
{
    public async Task EnviarAsync(string remetente, string senha, string servidorSmtp, int portaSmtp, string destinatario, string assunto, string mensagem, CancellationToken cancellationToken)
    {
        using var cliente = new SmtpClient(servidorSmtp, portaSmtp)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(remetente, senha)
        };
        using var email = new MailMessage(remetente, destinatario, assunto, mensagem)
        {
            IsBodyHtml = false
        };

        cancellationToken.ThrowIfCancellationRequested();
        await cliente.SendMailAsync(email, cancellationToken);
    }
}
