using DMail.Aplicacao.Modulos.ModuloDMail;

namespace DMail.WebApp.Servicos;

public class AgendadorDeDMailBackgroundService(IServiceScopeFactory escopoFactory, ILogger<AgendadorDeDMailBackgroundService> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var temporizador = new PeriodicTimer(TimeSpan.FromSeconds(30));
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var escopo = escopoFactory.CreateScope();
                var servico = escopo.ServiceProvider.GetRequiredService<ServicoDeEntregaDeDMail>();
                await servico.ProcessarPendentesAsync(stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Falha ao processar a fila de D-Mails.");
            }

            if (!await temporizador.WaitForNextTickAsync(stoppingToken))
                break;
        }
    }
}
