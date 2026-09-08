using DMail.Dominio.Modulos.ModuloConfiguracao;
using DMail.Dominio.Modulos.ModuloDMail;
using DMail.Infra.Modulos.ModuloConfiguracao;
using DMail.Infra.Modulos.ModuloDMail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DMail.Infra;

public static class InjecaoDeDependencia
{
    public static void AddInfraRepositories(
        this IServiceCollection services,
        IConfiguration configuration

    )
    {
        services.AddScoped<IRepositorioDMailAgendado, RepositorioDMailEmOrm>();
        services.AddScoped<IRepositorioConfiguracaoDeEmail, RepositorioConfiguracaoDeEmailEmOrm>();
        services.AddScoped<IClienteDeEmail, ClienteSmtpDeEmail>();
    }
}