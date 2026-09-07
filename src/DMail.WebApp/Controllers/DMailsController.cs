using DMail.Aplicacao.Modulos.ModuloDMail;
using Microsoft.AspNetCore.Mvc;

namespace DMail.WebApp.Controllers;

public class DMailsController(ServicoDMail servico) : Controller
{
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var dMails = await servico.ObterTodosAsync(cancellationToken);
        return View(dMails);
    }

    [HttpGet]
    public IActionResult Criar() => View(new CriarDMailViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Criar(CriarDMailViewModel modelo, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(modelo);

        try
        {
            await servico.CriarAsync(modelo, cancellationToken);
            TempData["Sucesso"] = "D-Mail agendado. A linha do tempo está definida.";
            return RedirectToAction(nameof(Index));
        }
        catch (ArgumentException exception)
        {
            ModelState.AddModelError(string.Empty, exception.Message);
            return View(modelo);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cancelar(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await servico.CancelarAsync(id, cancellationToken);
            TempData["Sucesso"] = "D-Mail cancelado.";
        }
        catch (Exception exception) when (exception is KeyNotFoundException or InvalidOperationException)
        {
            TempData["Erro"] = exception.Message;
        }

        return RedirectToAction(nameof(Index));
    }
}
