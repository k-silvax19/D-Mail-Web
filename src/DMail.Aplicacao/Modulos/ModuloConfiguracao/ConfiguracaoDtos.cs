using System.ComponentModel.DataAnnotations;

namespace DMail.Aplicacao.Modulos.ModuloConfiguracao;

public class ConfigurarRemetenteViewModel
{
    [Required, EmailAddress]
    [Display(Name = "E-mail do remetente")]
    public string Remetente { get; set; } = string.Empty;

    [Required, DataType(DataType.Password)]
    [Display(Name = "Senha de aplicativo")]
    public string SenhaDeAplicativo { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Servidor SMTP")]
    public string ServidorSmtp { get; set; } = "smtp.gmail.com";

    [Range(1, 65535)]
    [Display(Name = "Porta SMTP")]
    public int PortaSmtp { get; set; } = 587;
}

public record ConfiguracaoDeEmailDto(string Remetente, string ServidorSmtp, int PortaSmtp);
