
using DMail.Aplicacao.Modulos.ModuloConfiguracao;
using DMail.Aplicacao.Modulos.ModuloDMail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DMail.Aplicacao;

public static class InjecaoDeDependencia
{
    public static void AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddScoped<ServicoDMail>();
        services.AddScoped<ServicoConfiguracaoDeEmail>();
        services.AddScoped<ServicoDeEntregaDeDMail>();
    }
}