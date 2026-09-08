using DMail.Aplicacao.Modulos.ModuloConfiguracao;
using Microsoft.AspNetCore.Mvc;

namespace DMail.WebApp.Controllers;

public class ConfiguracaoController(ServicoConfiguracaoDeEmail servico) : Controller
{
    private const string CaminhoRemetente = "~/Modulos/Configuracao/Views/Remetente.cshtml";

    [HttpGet]
    public async Task<IActionResult> Remetente(CancellationToken cancellationToken)
    {
        var configuracao = await servico.ObterAsync(cancellationToken);
        return View(CaminhoRemetente, new ConfigurarRemetenteViewModel
        {
            Remetente = configuracao?.Remetente ?? string.Empty,
            ServidorSmtp = configuracao?.ServidorSmtp ?? "smtp.gmail.com",
            PortaSmtp = configuracao?.PortaSmtp ?? 587
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Remetente(ConfigurarRemetenteViewModel modelo, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(CaminhoRemetente, modelo);

        try
        {
            await servico.SalvarAsync(modelo, cancellationToken);
            TempData["Sucesso"] = "Remetente configurado. Os próximos D-Mails poderão ser enviados.";
            return RedirectToAction("Index", "DMails");
        }
        catch (ArgumentException exception)
        {
            ModelState.AddModelError(string.Empty, exception.Message);
            return View(CaminhoRemetente, modelo);
        }
    }
}
