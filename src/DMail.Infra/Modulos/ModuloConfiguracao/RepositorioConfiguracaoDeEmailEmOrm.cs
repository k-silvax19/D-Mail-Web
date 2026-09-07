using DMail.Dominio.Modulos.ModuloConfiguracao;
using DMail.Infra.Compartilhado.Orm;
using Microsoft.EntityFrameworkCore;

namespace DMail.Infra.Modulos.ModuloConfiguracao;

public class RepositorioConfiguracaoDeEmailEmOrm(DMailDbContext contexto) : IRepositorioConfiguracaoDeEmail
{
    public Task<ConfiguracaoDeEmail?> ObterAsync(CancellationToken cancellationToken = default) =>
        contexto.ConfiguracoesDeEmail.OrderBy(configuracao => configuracao.Id).FirstOrDefaultAsync(cancellationToken);

    public Task AdicionarAsync(ConfiguracaoDeEmail configuracao, CancellationToken cancellationToken = default) =>
        contexto.ConfiguracoesDeEmail.AddAsync(configuracao, cancellationToken).AsTask();

    public Task SalvarAlteracoesAsync(CancellationToken cancellationToken = default) =>
        contexto.SaveChangesAsync(cancellationToken);
}
