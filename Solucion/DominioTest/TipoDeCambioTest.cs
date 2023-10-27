using Dominio;

namespace DominioTest;

[TestClass]
public class TipoDeCambioTest
{
    private TipoDeCambio _tipoDeCambioParaTest;
    private Usuario _usuarioParaTest1;
    private Espacio _espacioParaTest1;
    private DateTime _fechaParaTest1;
    
    [TestInitialize]
    public void Inicio()
    {
        _fechaParaTest1 = DateTime.Now;
        
        _tipoDeCambioParaTest = new TipoDeCambio()
        {
            UsuarioCreador = _usuarioParaTest1,
            Espacio = _espacioParaTest1,
            Fecha = _fechaParaTest1,
            ValorDelDolar = 39.3f
        };
    }
    
    [TestMethod]
    public void ValorDelDolarCorrecto()
    {
        Assert.AreEqual(39.3f, _tipoDeCambioParaTest.ValorDelDolar);
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void ValorDelDolarNegativoIncorrecto()
    {
        _tipoDeCambioParaTest.ValorDelDolar = -1;
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void ValorDelDolarCeroIncorrecto()
    {
        _tipoDeCambioParaTest.ValorDelDolar = 0;
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void ValorDelDolarNuloIncorrecto()
    {
        _tipoDeCambioParaTest.ValorDelDolar = null;
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
}