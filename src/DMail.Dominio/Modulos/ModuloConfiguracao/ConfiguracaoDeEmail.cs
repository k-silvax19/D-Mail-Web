using DMail.Dominio.Compartilhado;

namespace DMail.Dominio.Modulos.ModuloConfiguracao;

public class ConfiguracaoDeEmail : EntidadeBase
{
    private ConfiguracaoDeEmail()
    {
    }

    public ConfiguracaoDeEmail(string remetente, string senhaProtegida, string servidorSmtp, int portaSmtp)
    {
        Atualizar(remetente, senhaProtegida, servidorSmtp, portaSmtp);
    }

    public string Remetente { get; private set; } = string.Empty;
    public string SenhaProtegida { get; private set; } = string.Empty;
    public string ServidorSmtp { get; private set; } = string.Empty;
    public int PortaSmtp { get; private set; }

    public void Atualizar(string remetente, string senhaProtegida, string servidorSmtp, int portaSmtp)
    {
        if (!System.Net.Mail.MailAddress.TryCreate(remetente, out _))
            throw new ArgumentException("Informe um e-mail de remetente válido.", nameof(remetente));
        if (string.IsNullOrWhiteSpace(senhaProtegida))
            throw new ArgumentException("Informe uma senha de aplicativo.", nameof(senhaProtegida));
        if (string.IsNullOrWhiteSpace(servidorSmtp))
            throw new ArgumentException("Informe o servidor SMTP.", nameof(servidorSmtp));
        if (portaSmtp is < 1 or > 65535)
            throw new ArgumentOutOfRangeException(nameof(portaSmtp));

        Remetente = remetente.Trim();
        SenhaProtegida = senhaProtegida;
        ServidorSmtp = servidorSmtp.Trim();
        PortaSmtp = portaSmtp;
    }
}
