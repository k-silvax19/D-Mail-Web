using System.ComponentModel.DataAnnotations;
using DMail.Dominio.Modulos.ModuloDMail;

namespace DMail.Aplicacao.Modulos.ModuloDMail;

public class CriarDMailViewModel
{
    [Required(ErrorMessage = "Informe o destinatário.")]
    [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
    [Display(Name = "Destinatário")]
    public string Destinatario { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o assunto.")]
    [StringLength(120)]
    public string Assunto { get; set; } = string.Empty;

    [Required(ErrorMessage = "Escreva a mensagem.")]
    [StringLength(5000)]
    public string Mensagem { get; set; } = string.Empty;

    [Required(ErrorMessage = "Escolha a data e a hora de envio.")]
    [Display(Name = "Enviar em")]
    public DateTime DataAgendada { get; set; } = DateTime.Now.AddHours(1);

    [Display(Name = "Recorrência")]
    public RecorrenciaDoDMail Recorrencia { get; set; } = RecorrenciaDoDMail.Unica;
}

public record DMailResumoDto(Guid Id, string Destinatario, string Assunto, DateTime DataAgendadaUtc, RecorrenciaDoDMail Recorrencia, StatusDoDMail Status);
