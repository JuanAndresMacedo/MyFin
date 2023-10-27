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

    [TestInitialize]
    public void Inicio()
    {
        _cuentaMonetariaParaTest = new Monetaria();
        _cuentaTarjetaDeCreditoParaTest = new TarjetaDeCredito();
        _fechaParaTest = DateTime.Now;
        _usuarioParaTest = new Usuario();
        _espacioParaTest = new Espacio();
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
        _cuentaMonetariaParaTest.Nombre = "Caja de ahorros";
        Assert.AreEqual("Caja de ahorros", _cuentaMonetariaParaTest.Nombre);
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void MonedaNoEsPesosUruguayosODolaresIncorrecto()
    {
        _cuentaTarjetaDeCreditoParaTest.Moneda = "Soles";
    }
    
    [TestMethod]
    public void MonedaEsDolaresCorrecto()
    {
        _cuentaMonetariaParaTest.Moneda = "US$";
        Assert.AreEqual("US$", _cuentaMonetariaParaTest.Moneda);
    }
    
    [TestMethod]
    public void MonedaEsPesosUYCorrecto()
    {
        _cuentaTarjetaDeCreditoParaTest.Moneda = "UYU";
        Assert.AreEqual("UYU", _cuentaTarjetaDeCreditoParaTest.Moneda);
    }
    
    [TestMethod]
    public void fechaDeCreacionCorrecto()
    {
        _cuentaMonetariaParaTest.FechaDeCreacion = _fechaParaTest;
        Assert.AreEqual(_fechaParaTest, _cuentaMonetariaParaTest.FechaDeCreacion);
    }
    
    [TestMethod]
    public void EqualsIgualesCorrecto()
    {
        _espacioParaTest.Nombre = "Familia";
        _espacioParaTest.Administrador = _usuarioParaTest;
        _usuarioParaTest.Correo = "www@xxx.com";
        _cuentaMonetariaParaTest.Nombre = "Ahorros";
        _cuentaMonetariaParaTest.Propietario = _usuarioParaTest;
        _cuentaMonetariaParaTest.Espacio = _espacioParaTest;
        Assert.AreEqual(true, _cuentaMonetariaParaTest.Equals(_cuentaMonetariaParaTest));
    }
    
    [TestMethod]
    public void EqualsNoIgualesCorrecto()
    {
        _espacioParaTest.Nombre = "Familia";
        _espacioParaTest.Administrador = _usuarioParaTest;
        _usuarioParaTest.Correo = "www@xxx.com";
        _cuentaTarjetaDeCreditoParaTest.Nombre = "Tarjeta c";
        _cuentaTarjetaDeCreditoParaTest.Propietario = _usuarioParaTest;
        _cuentaMonetariaParaTest.Espacio = _espacioParaTest;
        Assert.AreEqual(false, _cuentaMonetariaParaTest.Equals(
            _cuentaTarjetaDeCreditoParaTest));
    }
    
    [TestMethod]
    public void UsuarioCorrecto()
    {
        _cuentaTarjetaDeCreditoParaTest.Propietario = _usuarioParaTest;
        Assert.AreEqual(_usuarioParaTest, _cuentaTarjetaDeCreditoParaTest.Propietario);
    }
    
    [TestMethod]
    public void EspacioCorrecto()
    {
        _espacioParaTest.Nombre = "Familia";
        _espacioParaTest.Administrador = _usuarioParaTest;
        _cuentaMonetariaParaTest.Espacio = _espacioParaTest;
        
        Assert.AreEqual(_espacioParaTest, _cuentaMonetariaParaTest.Espacio);
    }
}