using Dominio;

namespace DominioTest;

[TestClass]
public class TransaccionTest
{
    private Transaccion _transaccionParaTest2;
    private DateTime _fechaParaTest;
    private Monetaria _cuentaParaTest;
    private Monetaria _cuentaParaTest1;
    private DateTime _fechaParaTest1;
    private TarjetaDeCredito _tarjetaDeCreditoParaTest;
    private Transaccion _transaccionParaTest1;
    private Categoria _categoriaParaTest;
    private Usuario _usuarioParaTest;
    private Espacio _espacioParaTest;

    [TestInitialize]
    public void Inicio()
    {
        _usuarioParaTest = new Usuario()
        {
            Correo = "hola@hotmail.com"
        };

        _espacioParaTest = new Espacio()
        {
            Nombre = "Familia",
            Administrador = _usuarioParaTest,
        };
        
        _categoriaParaTest = new Categoria();
        
        _cuentaParaTest = new Monetaria();
        _cuentaParaTest1 = new Monetaria();
        
        _tarjetaDeCreditoParaTest = new TarjetaDeCredito();
        
        _fechaParaTest1 = new DateTime(2030, 12, 10);
        _fechaParaTest = DateTime.Now;

        _transaccionParaTest1 = new Transaccion()
        {
            Nombre = "MacDonalds",
            Cuenta = _cuentaParaTest,
            Espacio = _espacioParaTest,
        };
        
        _transaccionParaTest2 = new Transaccion()
        {
            Nombre = "Lapices",
            Cuenta = _cuentaParaTest,
            Espacio = _espacioParaTest,
        };
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void NombreVacioIncorrecto()
    {
        _transaccionParaTest2.Nombre = ("");
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void NombreNuloIncorrecto()
    {
        _transaccionParaTest2.Nombre = (null);
    }

    [TestMethod]
    public void NombreCorrecto()
    {
        _transaccionParaTest2.Nombre = ("Ramiro");
        Assert.AreEqual("Ramiro", _transaccionParaTest2.Nombre);
    }

    [TestMethod]
    public void FechaCorrecta()
    {
        DateTime dt = DateTime.Now;
        _transaccionParaTest2.Fecha = dt;
        Assert.AreEqual(DateTime.Now.ToShortDateString(), _transaccionParaTest2.Fecha.ToShortDateString());
    }

    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void MontoCeroIncorrecto()
    {
        _transaccionParaTest2.Monto = (0);
    }

    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void MontoMenorACeroIncorrecto()
    {
        _transaccionParaTest2.Monto = (-1);
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void MontoNuloIncorrecto()
    {
        _transaccionParaTest2.Monto = (null);
    }

    [TestMethod]
    public void MontoCorrecto()
    {
        _transaccionParaTest2.Monto = (80);
        Assert.AreEqual(80, _transaccionParaTest2.Monto);
    }

    [TestMethod]
    public void SinMonedaSeConvierteEnUYU()
    {
        _transaccionParaTest2.Moneda = ("");
        Assert.AreEqual("UYU", _transaccionParaTest2.Moneda);
    }
    
    [TestMethod]
    public void MonedaNulaSeConvierteEnUYU()
    {
        _transaccionParaTest2.Moneda = (null);
        Assert.AreEqual("UYU", _transaccionParaTest2.Moneda);
    }

    [TestMethod]
    public void MonedaCorrectaSeMuestra()
    {
        _transaccionParaTest2.Moneda = ("US$");
        Assert.AreEqual("US$", _transaccionParaTest2.Moneda);
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void CuentaNulaIncorrecta()
    {
        _cuentaParaTest = null;
        _transaccionParaTest2.Cuenta = (_cuentaParaTest);
    }

    [TestMethod]
    public void CuentaMonetariaCorrecta()
    {
        _cuentaParaTest.Nombre =("Santander");
        _cuentaParaTest.Moneda = ("UYU");
        _cuentaParaTest.FechaDeCreacion = (_fechaParaTest);
        _cuentaParaTest.Monto = (20000);
        _transaccionParaTest2.Cuenta = (_cuentaParaTest);
        
        Assert.AreEqual(_cuentaParaTest, _transaccionParaTest2.Cuenta);
    }
    
    [TestMethod]
    public void CuentaTarjetaDeCreditoCorrecta()
    {
        _tarjetaDeCreditoParaTest.Nombre = ("Santander crédito");
        _tarjetaDeCreditoParaTest.Moneda =("US$");
        _tarjetaDeCreditoParaTest.FechaDeCreacion = (_fechaParaTest);
        _tarjetaDeCreditoParaTest.BancoEmisor = ("Santander");
        _tarjetaDeCreditoParaTest.UltimosCuatroDigitos = ("9888");
        _tarjetaDeCreditoParaTest.CreditoDisponible = (20000);
        _tarjetaDeCreditoParaTest.FechaDeCierre = (_fechaParaTest);
        _transaccionParaTest2.Cuenta = (_tarjetaDeCreditoParaTest);
        
        Assert.AreEqual(_tarjetaDeCreditoParaTest, _transaccionParaTest2.Cuenta);
    }

    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void CategoriaNulaIncorrecta()
    {
        _categoriaParaTest = null;
        _transaccionParaTest2.Categoria = _categoriaParaTest;
    }

    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void CategoriaInactivaIncorrecta()
    {
        _categoriaParaTest.Estatus = "Inactiva";
        _categoriaParaTest.Nombre = "Gastos en comida";
        _categoriaParaTest.Tipo = "Costo";
        _categoriaParaTest.FechaDeCreacion = _fechaParaTest;
        _transaccionParaTest2.Categoria = _categoriaParaTest;
    }

    [TestMethod]
    public void CategoriaActivaCorrecta()
    {
        _categoriaParaTest.Estatus = "Activa";
        _categoriaParaTest.Nombre = "Gastos en comida";
        _categoriaParaTest.Tipo = "Costo";
        _categoriaParaTest.FechaDeCreacion = _fechaParaTest;
        _transaccionParaTest2.Categoria = _categoriaParaTest;
        
        Assert.AreEqual(_categoriaParaTest, _transaccionParaTest2.Categoria);
    }
    
    [TestMethod]
    public void SonTransaccionesDiferentesCorrecto()
    {
        Assert.IsFalse(_transaccionParaTest2.Equals(_transaccionParaTest1));
    }
    
    [TestMethod]
    public void AgregarEspacioCorrecto()
    {
        Assert.IsTrue(_transaccionParaTest2.Espacio.Equals(_espacioParaTest));
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void TipoVacioIncorrecto()
    {
        _transaccionParaTest1.Tipo = "";
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void TipoNuloIncorrecto()
    {
        _transaccionParaTest1.Tipo = null;
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void TipoEsOtraCosaIncorrecto()
    {
        _transaccionParaTest1.Tipo = "Gasto";
    }
    
    [TestMethod]
    public void TipoEsCostoCorrecto()
    {
        _transaccionParaTest1.Tipo = "Costo";
        Assert.AreEqual("Costo", _transaccionParaTest1.Tipo);
    }
    
    [TestMethod]
    public void TipoEsIngresoCorrecto()
    {
        _transaccionParaTest1.Tipo = "Ingreso";
        Assert.AreEqual("Ingreso", _transaccionParaTest1.Tipo);
    }
}