using DMail.Dominio.Modulos.ModuloDMail;

namespace DMail.Testes.Unidade.ModuloDMail;

[TestClass]
public class DMailAgendadoTests
{
    [TestMethod]
    public void Deve_criar_dmail_agendado_para_o_futuro()
    {
        var horario = DateTime.UtcNow.AddHours(2);
        var dmail = new DMailAgendado("lab@future-mail.test", "Teste", "El Psy Kongroo.", horario, RecorrenciaDoDMail.Unica);

        Assert.AreEqual(StatusDoDMail.Agendado, dmail.Status);
        Assert.AreEqual(horario, dmail.DataAgendadaUtc);
    }

    [TestMethod]
    public void Nao_deve_agendar_para_o_passado()
    {
        try
        {
            _ = new DMailAgendado("lab@future-mail.test", "Teste", "Mensagem", DateTime.UtcNow.AddMinutes(-1), RecorrenciaDoDMail.Unica);
            Assert.Fail("Era esperada uma exceção para um agendamento no passado.");
        }
        catch (ArgumentException)
        {
        }
    }

    [TestMethod]
    public void Deve_cancelar_um_dmail_agendado()
    {
        var dmail = new DMailAgendado("lab@future-mail.test", "Teste", "Mensagem", DateTime.UtcNow.AddHours(1), RecorrenciaDoDMail.Unica);

        dmail.Cancelar();

        Assert.AreEqual(StatusDoDMail.Cancelado, dmail.Status);
    }

    [TestMethod]
    public void Deve_manter_agendamento_diario_apos_envio()
    {
        var horario = DateTime.UtcNow.AddMinutes(1);
        var dmail = new DMailAgendado("lab@future-mail.test", "Teste", "Mensagem", horario, RecorrenciaDoDMail.Diaria);

        dmail.RegistrarEnvio();

        Assert.AreEqual(StatusDoDMail.Agendado, dmail.Status);
        Assert.AreEqual(horario.AddDays(1), dmail.DataAgendadaUtc);
    }
}
