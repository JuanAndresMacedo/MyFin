using Dominio;

namespace DominioTest;

[TestClass]
public class TipoDeCambioTest
{
    private TipoDeCambio _tipoDeCambioParaTest;
    private Usuario _usuarioParaTest1;
    private Espacio _espacioParaTest1;
    private DateTime _fechaParaTest1;
    private Moneda _monedaParaTest1;
    
    [TestInitialize]
    public void Inicio()
    {
        _fechaParaTest1 = DateTime.Now;
        
        _monedaParaTest1 = new Moneda()
        {
            Id = 1,
            Nombre = "Dolares",
            SimboloMonetario = "U$S"
        };
        
        _tipoDeCambioParaTest = new TipoDeCambio()
        {
            Id = 1,
            UsuarioCreador = _usuarioParaTest1,
            Espacio = _espacioParaTest1,
            Fecha = _fechaParaTest1,
            ValorDeLaMoneda = 39.3f,
            Moneda = _monedaParaTest1
        };
    }
    
    [TestMethod]
    public void ValorDelDolarCorrecto()
    {
        Assert.AreEqual(39.3f, _tipoDeCambioParaTest.ValorDeLaMoneda);
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void ValorDelDolarNegativoIncorrecto()
    {
        _tipoDeCambioParaTest.ValorDeLaMoneda = -1;
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void ValorDelDolarCeroIncorrecto()
    {
        _tipoDeCambioParaTest.ValorDeLaMoneda = 0;
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void ValorDelDolarNuloIncorrecto()
    {
        _tipoDeCambioParaTest.ValorDeLaMoneda = null;
    }
    
    [TestMethod]
    public void UsuarioCorrecto()
    {
        Assert.AreEqual(_usuarioParaTest1, _tipoDeCambioParaTest.UsuarioCreador);
    }
    
    [TestMethod]
    public void EspacioCorrecto()
    {
        Assert.AreEqual(_espacioParaTest1, _tipoDeCambioParaTest.Espacio);
    }
    
    [TestMethod]
    public void FechaCorrecto()
    {
        Assert.AreEqual(_fechaParaTest1, _tipoDeCambioParaTest.Fecha);
    }
    
    [TestMethod]
    public void MonedaCorrecto()
    {
        Assert.AreEqual(_monedaParaTest1, _tipoDeCambioParaTest.Moneda);
    }
}