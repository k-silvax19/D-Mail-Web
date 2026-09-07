using DMail.Dominio.Modulos.ModuloDMail;
using DMail.Dominio.Modulos.ModuloConfiguracao;
using Microsoft.EntityFrameworkCore;

namespace DMail.Infra.Compartilhado.Orm;

public class DMailDbContext(DbContextOptions<DMailDbContext> options) : DbContext(options)
{
    public DbSet<DMailAgendado> DMails => Set<DMailAgendado>();
    public DbSet<ConfiguracaoDeEmail> ConfiguracoesDeEmail => Set<ConfiguracaoDeEmail>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DMailAgendado>(entity =>
        {
            entity.ToTable("DMails");
            entity.HasKey(d => d.Id);
            entity.Property(d => d.Destinatario).HasMaxLength(254).IsRequired();
            entity.Property(d => d.Assunto).HasMaxLength(120).IsRequired();
            entity.Property(d => d.Mensagem).HasMaxLength(5000).IsRequired();
            entity.Property(d => d.Status).HasConversion<string>().HasMaxLength(20).IsRequired();
            entity.Property(d => d.ErroDeEnvio).HasMaxLength(1000);
            entity.HasIndex(d => new { d.Status, d.DataAgendadaUtc });
        });

        modelBuilder.Entity<ConfiguracaoDeEmail>(entity =>
        {
            entity.ToTable("ConfiguracoesDeEmail");
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Remetente).HasMaxLength(254).IsRequired();
            entity.Property(c => c.SenhaProtegida).IsRequired();
            entity.Property(c => c.ServidorSmtp).HasMaxLength(253).IsRequired();
        });
    }
}
