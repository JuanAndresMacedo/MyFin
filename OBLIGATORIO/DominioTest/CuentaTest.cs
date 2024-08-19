using Dominio;

namespace DominioTest;

[TestClass]
public class CuentaTest
{
    private Cuenta _cuentaMonetariaParaTest;
    private Cuenta _cuentaTarjetaDeCreditoParaTest;

    private DateTime _fechaParaTest;
    private Usuario _usuarioParaTest;
    private Espacio _espacioParaTest;
    private Moneda _monedaParaTest1;
    private Moneda _monedaParaTest2;

    [TestInitialize]
    public void Inicio()
    {
        _fechaParaTest = DateTime.Now;

        _usuarioParaTest = new Usuario()
        {
            Id = 1,
            Nombre = "Juan",
            Correo = "www@xxx.com"
        };

        _espacioParaTest = new Espacio()
        {
            Id = 1,
            Nombre = "Familia",
            Administrador = _usuarioParaTest
        };

        _monedaParaTest1 = new Moneda()
        {
            Id = 1,
            Nombre = "U$S"
        };

        _monedaParaTest2 = new Moneda()
        {
            Id = 2,
            Nombre = "UYU"
        };

        _cuentaMonetariaParaTest = new Monetaria()
        {
            Id = 1,
            Nombre = "Caja de ahorros",
            Moneda = _monedaParaTest1,
            Espacio = _espacioParaTest,
            Propietario = _usuarioParaTest,
            FechaDeCreacion = _fechaParaTest
        };

        _cuentaTarjetaDeCreditoParaTest = new TarjetaDeCredito()
        {
            Id = 2,
            Moneda = _monedaParaTest2,
            Espacio = _espacioParaTest,
            Propietario = _usuarioParaTest,
            FechaDeCreacion = _fechaParaTest
        };
    }

    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void NombreVacioIncorrecto()
    {
        _cuentaMonetariaParaTest.Nombre = "";
    }

    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void NombreNuloIncorrecto()
    {
        _cuentaMonetariaParaTest.Nombre = null;
    }

    [TestMethod]
    public void NombreCorrecto()
    {
        Assert.AreEqual("Caja de ahorros", _cuentaMonetariaParaTest.Nombre);
    }

    [TestMethod]
    public void MonedaEsDolaresCorrecto()
    {
        Assert.AreEqual("U$S", _cuentaMonetariaParaTest.Moneda.Nombre);
    }

    [TestMethod]
    public void MonedaEsPesosUYCorrecto()
    {
        Assert.AreEqual("UYU", _cuentaTarjetaDeCreditoParaTest.Moneda.Nombre);
    }

    [TestMethod]
    public void fechaDeCreacionCorrecto()
    {
        Assert.AreEqual(_fechaParaTest, _cuentaMonetariaParaTest.FechaDeCreacion);
    }

    [TestMethod]
    public void EqualsIgualesCorrecto()
    {
        Assert.AreEqual(true, _cuentaMonetariaParaTest.Equals(_cuentaMonetariaParaTest));
    }

    [TestMethod]
    public void EqualsNoIgualesCorrecto()
    {
        Assert.AreEqual(false, _cuentaMonetariaParaTest.Equals(
            _cuentaTarjetaDeCreditoParaTest));
    }

    [TestMethod]
    public void UsuarioCorrecto()
    {
        Assert.AreEqual(_usuarioParaTest, _cuentaTarjetaDeCreditoParaTest.Propietario);
    }

    [TestMethod]
    public void EspacioCorrecto()
    {
        Assert.AreEqual(_espacioParaTest, _cuentaMonetariaParaTest.Espacio);
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void MonedaVaciaIncorrecto()
    {
        _cuentaMonetariaParaTest.Moneda = null;
    }
}