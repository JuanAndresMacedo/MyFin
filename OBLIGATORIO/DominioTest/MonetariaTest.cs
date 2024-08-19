using Dominio;

namespace DominioTest;

[TestClass]
public class MonetariaTest
{
    private Monetaria _monetariaParaTest;
    private Usuario _usuarioParaTest;
    private Espacio _espacioParaTest;
    private Moneda _monedaParaTest;

    [TestInitialize]
    public void Inicio()
    {
        _usuarioParaTest = new Usuario()
        {
            Id = 1,
            Correo = "hola@hotmail.com",
        };

        _espacioParaTest = new Espacio()
        {
            Id = 1,
            Administrador = _usuarioParaTest,
            Nombre = "Familia"
        };

        _monedaParaTest = new Moneda()
        {
            Id = 1,
            Nombre = "UYU"
        };

        _monetariaParaTest = new Monetaria()
        {
            Id = 1,
            Nombre = "Debito",
            Propietario = _usuarioParaTest,
            Espacio = _espacioParaTest,
            Moneda = _monedaParaTest,
            Monto = 2000.5f,
        };
    }

    [TestMethod]
    public void MontoCorrecto()
    {
        Assert.AreEqual(2000.5f, _monetariaParaTest.Monto);
    }

    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void MontoMenorACeroIncorrecto()
    {
        _monetariaParaTest.Monto = -1f;
    }

    [TestMethod]
    public void MontoIgualACeroCorrecto()
    {
        _monetariaParaTest.Monto = 0.0f;
        Assert.AreEqual(0f, _monetariaParaTest.Monto);
    }
}