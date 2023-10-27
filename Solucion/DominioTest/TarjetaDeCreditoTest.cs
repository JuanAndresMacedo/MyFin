using Dominio;

namespace DominioTest;

[TestClass]
public class TarjetaDeCreditoTest
{
    private TarjetaDeCredito _tarjetaDeCreditoParaTest;
    private Usuario _usuarioParaTest;
    private Espacio _espacioParaTest;
    private DateTime _fechaParaTest;

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

        _tarjetaDeCreditoParaTest = new TarjetaDeCredito()
        {
            Nombre = "Debito",
            Propietario = _usuarioParaTest,
            Espacio = _espacioParaTest,
            BancoEmisor = "Santander",
            UltimosCuatroDigitos = "1123",
        };
        
        _fechaParaTest = DateTime.Now;
    }

    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void BancoEmisorVacioIncorrecto()
    {
        _tarjetaDeCreditoParaTest.BancoEmisor = "";
    }

    [TestMethod]
    public void BancoEmisorCorrecto()
    {
        _tarjetaDeCreditoParaTest.BancoEmisor = "Itau";
        Assert.AreEqual("Itau", _tarjetaDeCreditoParaTest.BancoEmisor);
    }

    [TestMethod]
    public void CreditoDisponibleCorrecto()
    {
        _tarjetaDeCreditoParaTest.CreditoDisponible = 2000.5f;
        Assert.AreEqual(2000.5f, _tarjetaDeCreditoParaTest.CreditoDisponible);
    }

    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void CreditoDisponibleMenorACeroIncorrecto()
    {
        _tarjetaDeCreditoParaTest.CreditoDisponible = -0.0001f;
    }

    [TestMethod]
    public void CreditoDisponibleCorrectoEsCero()
    {
        _tarjetaDeCreditoParaTest.CreditoDisponible = 0f;
        Assert.AreEqual(0.0f, _tarjetaDeCreditoParaTest.CreditoDisponible);
    }

    [TestMethod]
    public void DiaDeCierreCorrecto()
    {
        _tarjetaDeCreditoParaTest.FechaDeCierre = _fechaParaTest;
        Assert.AreEqual(_fechaParaTest, _tarjetaDeCreditoParaTest.FechaDeCierre);
    }

    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void UltimosCuatroDigitosSolo1Incorrecto()
    {
        _tarjetaDeCreditoParaTest.UltimosCuatroDigitos = "1";
    }

    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void UltimosCuatroDigitosMasDe4Incorrecto()
    {
        _tarjetaDeCreditoParaTest.UltimosCuatroDigitos = "12345";
    }

    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void UltimosCuatroDigitosNoTodosSonNumerosIncorrecto()
    {
        _tarjetaDeCreditoParaTest.UltimosCuatroDigitos = "12E4";
    }

    [TestMethod]
    public void UltimosCuatroDigitosCorrecto()
    {
        _tarjetaDeCreditoParaTest.UltimosCuatroDigitos = "0456";
        Assert.AreEqual("0456", _tarjetaDeCreditoParaTest.UltimosCuatroDigitos);
    }

    [TestMethod]
    public void DevolverTipoDeCuentaTarjetaDeCreditoCorrecto()
    {
        Assert.AreEqual("tarjetaDeCredito", _tarjetaDeCreditoParaTest.DevolverTipoDeCuenta());
    }
    
    [TestMethod]
    public void ActualizarTarjetaDeCreditoCorrecto()
    {
        TarjetaDeCredito tarjetaDeCreditoActualizada = new TarjetaDeCredito()
        {
            Nombre = "Tarjeta dc",
            BancoEmisor = "BBVA",
            UltimosCuatroDigitos = "4444",
            Moneda = "UYU",
        };

        _tarjetaDeCreditoParaTest.ActualizarCuenta(tarjetaDeCreditoActualizada);
        
        Assert.AreEqual("BBVA", _tarjetaDeCreditoParaTest.BancoEmisor);
    }
    
    [TestMethod]
    public void IngresarIngresoCorrecto()
    {
        TarjetaDeCredito tarjetaDeCreditoRecibeIngreso = new TarjetaDeCredito()
        {
            CreditoDisponible = 1000,
            Moneda = "UYU",
            Nombre = "Nombre editado"
        };

        tarjetaDeCreditoRecibeIngreso.SumarIngreso(20);
        Assert.AreEqual(1020, tarjetaDeCreditoRecibeIngreso.CreditoDisponible);
    }
    
    [TestMethod]
    public void IngresarCostoCorrecto()
    {
        TarjetaDeCredito tarjetaDeCreditoRecibeIngreso = new TarjetaDeCredito()
        {
            CreditoDisponible = 1000,
            Moneda = "UYU",
            Nombre = "Nombre editado"
        };

        tarjetaDeCreditoRecibeIngreso.DescontarCosto(20);
        Assert.AreEqual(980, tarjetaDeCreditoRecibeIngreso.CreditoDisponible);
    }
}