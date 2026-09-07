namespace DMail.Dominio.Modulos.ModuloDMail;

public interface IClienteDeEmail
{
    Task EnviarAsync(string remetente, string senha, string servidorSmtp, int portaSmtp, string destinatario, string assunto, string mensagem, CancellationToken cancellationToken);
}
