using Dominio;

namespace DominioTest;

[TestClass]
public class TarjetaDeCreditoTest
{
    private TarjetaDeCredito _tarjetaDeCreditoParaTest;
    private Usuario _usuarioParaTest;
    private Espacio _espacioParaTest;
    private DateTime _fechaParaTest;
    private Moneda _monedaParaTest;

    [TestInitialize]
    public void Inicio()
    {
        _fechaParaTest = DateTime.Now;
        
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

        _tarjetaDeCreditoParaTest = new TarjetaDeCredito()
        {
            Id = 1,
            Nombre = "Debito",
            Propietario = _usuarioParaTest,
            Espacio = _espacioParaTest,
            BancoEmisor = "Santander",
            UltimosCuatroDigitos = "0456",
            Moneda = _monedaParaTest,
            CreditoDisponible = 2000.5f,
            FechaDeCierre = _fechaParaTest
        };
        
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
        Assert.AreEqual("Santander", _tarjetaDeCreditoParaTest.BancoEmisor);
    }

    [TestMethod]
    public void CreditoDisponibleCorrecto()
    {
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
        Assert.AreEqual("0456", _tarjetaDeCreditoParaTest.UltimosCuatroDigitos);
    }
    
    [TestMethod]
    public void DevolverDineroDeCuentaTarjetaDeCreditoCorrecto()
    {
        Assert.AreEqual(2000.5f, _tarjetaDeCreditoParaTest.DevolverDineroCuenta());
    }
    
    [TestMethod]
    public void AsignarDineroDeCuentaTarjetaDeCreditoCorrecto()
    {
        _tarjetaDeCreditoParaTest.AsignarDineroCuenta(200.75f);
        Assert.AreEqual(200.75f, _tarjetaDeCreditoParaTest.CreditoDisponible);
    }
}