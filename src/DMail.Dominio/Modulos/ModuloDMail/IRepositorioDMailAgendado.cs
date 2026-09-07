namespace DMail.Dominio.Modulos.ModuloDMail;

public interface IRepositorioDMailAgendado
{
    Task AdicionarAsync(DMailAgendado dmail, CancellationToken cancellationToken = default);
    Task<DMailAgendado?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<DMailAgendado>> ObterTodosAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<DMailAgendado>> ObterPendentesParaEnvioAsync(DateTime agoraUtc, CancellationToken cancellationToken = default);
    Task SalvarAlteracoesAsync(CancellationToken cancellationToken = default);
}
