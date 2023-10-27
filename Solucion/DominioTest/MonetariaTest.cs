using Dominio;

namespace DominioTest;

[TestClass] 
public class MonetariaTest
{
    private Monetaria _monetariaParaTest;
    private Usuario _usuarioParaTest;
    private Espacio _espacioParaTest;

    [TestInitialize]
    public void Inicio()
    {
        _usuarioParaTest = new Usuario()
        {
            Correo = "hola@hotmail.com",
        };

        _espacioParaTest = new Espacio()
        {
            Administrador = _usuarioParaTest,
            Nombre = "Familia"
        };

        _monetariaParaTest = new Monetaria()
        {
            Nombre = "Debito",
            Propietario = _usuarioParaTest,
            Espacio = _espacioParaTest,
            Moneda = "US$",
            Monto = 1,
        };
    }
    
    [TestMethod]
    public void MontoCorrecto()
    {
        _monetariaParaTest.Monto = 2000.5f;
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
    
    [TestMethod]
    public void DevolverTipoDeCuentaMonetariaCorrecto()
    {
        Assert.AreEqual("monetaria", _monetariaParaTest.DevolverTipoDeCuenta());
    }
    
    [TestMethod]
    public void ActualizarCuentaMonetariaCorrecto()
    {
        Monetaria monetariaActualizada = new Monetaria()
        {
            Monto = 1000,
            Moneda = "UYU",
            Nombre = "Nombre editado"
        };

        _monetariaParaTest.ActualizarCuenta(monetariaActualizada);
        
        Assert.AreEqual(1000, _monetariaParaTest.Monto);
        Assert.AreEqual("UYU", _monetariaParaTest.Moneda);
        Assert.AreEqual("Nombre editado", _monetariaParaTest.Nombre);
    }
    
    [TestMethod]
    public void IngresarIngresoCorrecto()
    {
        Monetaria monetariaRecibeIngreso = new Monetaria()
        {
            Monto = 1000,
            Moneda = "UYU",
            Nombre = "Nombre editado"
        };

        monetariaRecibeIngreso.SumarIngreso(20);
        Assert.AreEqual(1020, monetariaRecibeIngreso.Monto);
    }
    
    [TestMethod]
    public void IngresarCostoCorrecto()
    {
        Monetaria monetariaRecibeIngreso = new Monetaria()
        {
            Monto = 1000,
            Moneda = "UYU",
            Nombre = "Nombre editado"
        };

        monetariaRecibeIngreso.DescontarCosto(20);
        Assert.AreEqual(980, monetariaRecibeIngreso.Monto);
    }
}