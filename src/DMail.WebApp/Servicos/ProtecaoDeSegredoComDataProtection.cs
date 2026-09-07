using DMail.Aplicacao.Modulos.ModuloConfiguracao;
using Microsoft.AspNetCore.DataProtection;

namespace DMail.WebApp.Servicos;

public class ProtecaoDeSegredoComDataProtection(IDataProtectionProvider provider) : IProtecaoDeSegredo
{
    private readonly IDataProtector _protetor = provider.CreateProtector("DMail.SenhaDeAplicativo.v1");

    public string Proteger(string segredo) => _protetor.Protect(segredo);
    public string Desproteger(string segredoProtegido) => _protetor.Unprotect(segredoProtegido);
}
