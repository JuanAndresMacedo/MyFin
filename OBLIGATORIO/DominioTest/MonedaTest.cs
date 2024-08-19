using Dominio;

namespace DominioTest;

[TestClass]
public class MonedaTest
{
    private Moneda _monedaParaTest1;
    private Moneda _monedaParaTest2;

    [TestInitialize]
    public void Inicio()
    {
        _monedaParaTest1 = new Moneda()
        {
            Id = 1,
            Nombre = "Pesos Uruguayos",
            SimboloMonetario = "UYU"
        };

        _monedaParaTest2 = new Moneda()
        {
            Id = 2,
            Nombre = "Dolares",
            SimboloMonetario = "U$S"
        };
    }

    [TestMethod]
    public void SonMonedasIgualesCorrecto()
    {
        _monedaParaTest2.Id = 1;
        Assert.IsTrue(_monedaParaTest1.Equals(_monedaParaTest2));
    }

    [TestMethod]
    public void SonMonedasDiferentesCorrecto()
    {
        Assert.IsFalse(_monedaParaTest1.Equals(_monedaParaTest2));
    }

    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void NombreMonedaVacioIncorrecto()
    {
        _monedaParaTest1.Nombre = "";
    }

    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void NombreMonedaNuloIncorrecto()
    {
        _monedaParaTest1.Nombre = null;
    }

    [TestMethod]
    public void NombreMonedaEsPesosUruguayosCorrecto()
    {
        Assert.AreEqual("Pesos Uruguayos", _monedaParaTest1.Nombre);
    }
    
    [TestMethod]
    public void SimboloMonetarioDeMonedaEsUYUCorrecto()
    {
        Assert.AreEqual("UYU", _monedaParaTest1.SimboloMonetario);
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void SimboloMonetarioEsVacioIncorrecto()
    {
        _monedaParaTest1.SimboloMonetario = "";
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void SimboloMonetarioEsNuloIncorrecto()
    {
        _monedaParaTest1.SimboloMonetario = null;
    }
}