using DMail.Dominio.Modulos.ModuloConfiguracao;

namespace DMail.Aplicacao.Modulos.ModuloConfiguracao;

public class ServicoConfiguracaoDeEmail(IRepositorioConfiguracaoDeEmail repositorio, IProtecaoDeSegredo protetor)
{
    public async Task<ConfiguracaoDeEmailDto?> ObterAsync(CancellationToken cancellationToken = default)
    {
        var configuracao = await repositorio.ObterAsync(cancellationToken);
        return configuracao is null ? null : new(configuracao.Remetente, configuracao.ServidorSmtp, configuracao.PortaSmtp);
    }

    public async Task SalvarAsync(ConfigurarRemetenteViewModel modelo, CancellationToken cancellationToken = default)
    {
        var segredoProtegido = protetor.Proteger(modelo.SenhaDeAplicativo);
        var configuracao = await repositorio.ObterAsync(cancellationToken);
        if (configuracao is null)
        {
            configuracao = new ConfiguracaoDeEmail(modelo.Remetente, segredoProtegido, modelo.ServidorSmtp, modelo.PortaSmtp);
            await repositorio.AdicionarAsync(configuracao, cancellationToken);
        }
        else
            configuracao.Atualizar(modelo.Remetente, segredoProtegido, modelo.ServidorSmtp, modelo.PortaSmtp);

        await repositorio.SalvarAlteracoesAsync(cancellationToken);
    }
}
