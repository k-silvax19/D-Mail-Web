using DMail.Aplicacao.Modulos.ModuloConfiguracao;
using DMail.Dominio.Modulos.ModuloConfiguracao;
using DMail.Dominio.Modulos.ModuloDMail;

namespace DMail.Aplicacao.Modulos.ModuloDMail;

public class ServicoDeEntregaDeDMail(
    IRepositorioDMailAgendado repositorioDMails,
    IRepositorioConfiguracaoDeEmail repositorioConfiguracao,
    IProtecaoDeSegredo protetor,
    IClienteDeEmail clienteDeEmail)
{
    public async Task ProcessarPendentesAsync(CancellationToken cancellationToken = default)
    {
        var configuracao = await repositorioConfiguracao.ObterAsync(cancellationToken);
        if (configuracao is null)
            return;

        var dMails = await repositorioDMails.ObterPendentesParaEnvioAsync(DateTime.UtcNow, cancellationToken);
        foreach (var dmail in dMails)
        {
            try
            {
                await clienteDeEmail.EnviarAsync(configuracao.Remetente, protetor.Desproteger(configuracao.SenhaProtegida), configuracao.ServidorSmtp, configuracao.PortaSmtp, dmail.Destinatario, dmail.Assunto, dmail.Mensagem, cancellationToken);
                dmail.RegistrarEnvio();
            }
            catch (Exception exception) when (exception is not OperationCanceledException)
            {
                dmail.RegistrarFalha(exception.Message);
            }
        }

        if (dMails.Count > 0)
            await repositorioDMails.SalvarAlteracoesAsync(cancellationToken);
    }
}
