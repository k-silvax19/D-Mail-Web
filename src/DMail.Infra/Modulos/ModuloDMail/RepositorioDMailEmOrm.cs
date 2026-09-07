using DMail.Dominio.Modulos.ModuloDMail;
using DMail.Infra.Compartilhado.Orm;
using Microsoft.EntityFrameworkCore;

namespace DMail.Infra.Modulos.ModuloDMail;

public class RepositorioDMailEmOrm(DMailDbContext contexto) : IRepositorioDMailAgendado
{
    public Task AdicionarAsync(DMailAgendado dmail, CancellationToken cancellationToken = default) =>
        contexto.DMails.AddAsync(dmail, cancellationToken).AsTask();

    public Task<DMailAgendado?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        contexto.DMails.FirstOrDefaultAsync(d => d.Id == id, cancellationToken);

    public async Task<IReadOnlyList<DMailAgendado>> ObterTodosAsync(CancellationToken cancellationToken = default) =>
        await contexto.DMails.OrderByDescending(d => d.DataAgendadaUtc).ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<DMailAgendado>> ObterPendentesParaEnvioAsync(DateTime agoraUtc, CancellationToken cancellationToken = default) =>
        await contexto.DMails
            .Where(d => d.Status == StatusDoDMail.Agendado && d.DataAgendadaUtc <= agoraUtc)
            .OrderBy(d => d.DataAgendadaUtc)
            .ToListAsync(cancellationToken);

    public Task SalvarAlteracoesAsync(CancellationToken cancellationToken = default) =>
        contexto.SaveChangesAsync(cancellationToken);
}
