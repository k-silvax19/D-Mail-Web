using System.Net.Mail;
using DMail.Dominio.Compartilhado;

namespace DMail.Dominio.Modulos.ModuloDMail;

public class DMailAgendado : EntidadeBase
{
    private DMailAgendado()
    {
    }

    public DMailAgendado(string destinatario, string assunto, string mensagem, DateTime dataAgendadaUtc, RecorrenciaDoDMail recorrencia)
    {
        DefinirConteudo(destinatario, assunto, mensagem);
        AgendarPara(dataAgendadaUtc);
        Recorrencia = recorrencia;
        CriadoEmUtc = DateTime.UtcNow;
        Status = StatusDoDMail.Agendado;
    }

    public string Destinatario { get; private set; } = string.Empty;
    public string Assunto { get; private set; } = string.Empty;
    public string Mensagem { get; private set; } = string.Empty;
    public DateTime DataAgendadaUtc { get; private set; }
    public DateTime CriadoEmUtc { get; private set; }
    public RecorrenciaDoDMail Recorrencia { get; private set; }
    public StatusDoDMail Status { get; private set; }
    public string? ErroDeEnvio { get; private set; }

    public void Reagendar(DateTime novaDataUtc)
    {
        GarantirQueEstaAgendado();
        AgendarPara(novaDataUtc);
    }

    public void Cancelar()
    {
        GarantirQueEstaAgendado();
        Status = StatusDoDMail.Cancelado;
    }

    public void RegistrarEnvio()
    {
        GarantirQueEstaAgendado();
        Status = Recorrencia == RecorrenciaDoDMail.Diaria ? StatusDoDMail.Agendado : StatusDoDMail.Enviado;
        if (Recorrencia == RecorrenciaDoDMail.Diaria)
            DataAgendadaUtc = DataAgendadaUtc.AddDays(1);
        ErroDeEnvio = null;
    }

    public void RegistrarFalha(string erro)
    {
        GarantirQueEstaAgendado();
        Status = StatusDoDMail.Falhou;
        ErroDeEnvio = string.IsNullOrWhiteSpace(erro) ? "Falha desconhecida no envio." : erro;
    }

    private void DefinirConteudo(string destinatario, string assunto, string mensagem)
    {
        if (string.IsNullOrWhiteSpace(destinatario) || !MailAddress.TryCreate(destinatario, out _))
            throw new ArgumentException("Informe um endereço de e-mail válido.", nameof(destinatario));

        if (string.IsNullOrWhiteSpace(assunto) || assunto.Length > 120)
            throw new ArgumentException("O assunto deve ter entre 1 e 120 caracteres.", nameof(assunto));

        if (string.IsNullOrWhiteSpace(mensagem) || mensagem.Length > 5000)
            throw new ArgumentException("A mensagem deve ter entre 1 e 5000 caracteres.", nameof(mensagem));

        Destinatario = destinatario.Trim();
        Assunto = assunto.Trim();
        Mensagem = mensagem.Trim();
    }

    private void AgendarPara(DateTime dataUtc)
    {
        var dataNormalizada = dataUtc.Kind == DateTimeKind.Utc ? dataUtc : dataUtc.ToUniversalTime();
        if (dataNormalizada <= DateTime.UtcNow)
            throw new ArgumentException("O envio precisa ser agendado para um instante futuro.", nameof(dataUtc));

        DataAgendadaUtc = dataNormalizada;
    }

    private void GarantirQueEstaAgendado()
    {
        if (Status != StatusDoDMail.Agendado)
            throw new InvalidOperationException("Apenas D-Mails agendados podem ser alterados.");
    }
}
