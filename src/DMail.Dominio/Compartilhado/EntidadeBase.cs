namespace DMail.Dominio.Compartilhado;

public abstract class EntidadeBase
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
}
