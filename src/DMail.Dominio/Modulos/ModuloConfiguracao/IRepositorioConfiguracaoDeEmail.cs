namespace DMail.Dominio.Modulos.ModuloConfiguracao;

public interface IRepositorioConfiguracaoDeEmail
{
    Task<ConfiguracaoDeEmail?> ObterAsync(CancellationToken cancellationToken = default);
    Task AdicionarAsync(ConfiguracaoDeEmail configuracao, CancellationToken cancellationToken = default);
    Task SalvarAlteracoesAsync(CancellationToken cancellationToken = default);
}
