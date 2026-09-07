using DMail.Dominio.Modulos.ModuloDMail;

namespace DMail.Aplicacao.Modulos.ModuloDMail;

public class ServicoDMail(IRepositorioDMailAgendado repositorio)
{
    public async Task CriarAsync(CriarDMailViewModel modelo, CancellationToken cancellationToken = default)
    {
        var dataUtc = DateTime.SpecifyKind(modelo.DataAgendada, DateTimeKind.Local).ToUniversalTime();
        var dmail = new DMailAgendado(modelo.Destinatario, modelo.Assunto, modelo.Mensagem, dataUtc, modelo.Recorrencia);
        await repositorio.AdicionarAsync(dmail, cancellationToken);
        await repositorio.SalvarAlteracoesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<DMailResumoDto>> ObterTodosAsync(CancellationToken cancellationToken = default)
    {
        var dMails = await repositorio.ObterTodosAsync(cancellationToken);
        return dMails.Select(d => new DMailResumoDto(d.Id, d.Destinatario, d.Assunto, d.DataAgendadaUtc, d.Recorrencia, d.Status)).ToList();
    }

    public async Task CancelarAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var dmail = await repositorio.ObterPorIdAsync(id, cancellationToken)
            ?? throw new KeyNotFoundException("D-Mail não encontrado.");

        dmail.Cancelar();
        await repositorio.SalvarAlteracoesAsync(cancellationToken);
    }
}
