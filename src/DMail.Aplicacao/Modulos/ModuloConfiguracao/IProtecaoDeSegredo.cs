namespace DMail.Aplicacao.Modulos.ModuloConfiguracao;

public interface IProtecaoDeSegredo
{
    string Proteger(string segredo);
    string Desproteger(string segredoProtegido);
}
