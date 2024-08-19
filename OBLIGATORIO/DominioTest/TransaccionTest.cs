using Dominio;
using Dominio.Constantes;

namespace DominioTest;

[TestClass]
public class TransaccionTest
{
    private Transaccion _transaccionParaTest2;
    private DateTime _fechaParaTest1;
    private DateTime _fechaParaTest2;
    private Monetaria _cuentaMonetariaParaTest;
    private TarjetaDeCredito _tarjetaDeCreditoParaTest;
    private Transaccion _transaccionParaTest1;
    private Categoria _categoriaParaTest;
    private Usuario _usuarioParaTest;
    private Espacio _espacioParaTest;
    private Moneda _monedaParaTest;

    [TestInitialize]
    public void Inicio()
    {
        _fechaParaTest1 = DateTime.Now;
        _fechaParaTest2 = new DateTime(2030, 12, 10);

        _usuarioParaTest = new Usuario()
        {
            Id = 1,
            Correo = "hola@hotmail.com"
        };

        _espacioParaTest = new Espacio()
        {
            Id = 1,
            Nombre = "Familia",
            Administrador = _usuarioParaTest,
        };

        _monedaParaTest = new Moneda()
        {
            Id = 1,
            Nombre = "Dolares",
            SimboloMonetario = "UYU"
        };

        _categoriaParaTest = new Categoria()
        {
            Nombre = "Gastos en comida",
            Estatus = ConstantesCategoria.estatusInactiva,
            Tipo = ConstantesCategoria.tipoCosto,
            FechaDeCreacion = _fechaParaTest1
        };

        _cuentaMonetariaParaTest = new Monetaria()
        {
            Id = 1,
            Nombre = "Santander",
            Moneda = _monedaParaTest,
            FechaDeCreacion = _fechaParaTest1,
            Monto = 20000,
        };

        _tarjetaDeCreditoParaTest = new TarjetaDeCredito()
        {
            Id = 2,
            Nombre = "Santander crédito",
            Moneda = _monedaParaTest,
            FechaDeCreacion = _fechaParaTest1,
            FechaDeCierre = _fechaParaTest1,
            BancoEmisor = "Santander",
            UltimosCuatroDigitos = "9888",
            CreditoDisponible = 20000
        };

        _transaccionParaTest1 = new Transaccion()
        {
            Id = 1,
            Nombre = "MacDonalds",
            Tipo = ConstantesCategoria.tipoCosto,
            Cuenta = _cuentaMonetariaParaTest,
            Espacio = _espacioParaTest,
            Monto = 50,
        };

        _transaccionParaTest2 = new Transaccion()
        {
            Id = 2,
            Nombre = "Lapices",
            Tipo = ConstantesCategoria.tipoIngreso,
            Moneda = _monedaParaTest,
            Cuenta = _tarjetaDeCreditoParaTest,
            Espacio = _espacioParaTest,
            Fecha = _fechaParaTest1,
            Monto = 80,
        };
    }

    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void NombreVacioIncorrecto()
    {
        _transaccionParaTest2.Nombre = "";
    }

    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void NombreNuloIncorrecto()
    {
        _transaccionParaTest2.Nombre = null;
    }

    [TestMethod]
    public void NombreCorrecto()
    {
        Assert.AreEqual("Lapices", _transaccionParaTest2.Nombre);
    }

    [TestMethod]
    public void FechaCorrecta()
    {
        Assert.AreEqual(_fechaParaTest1, _transaccionParaTest2.Fecha);
    }

    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void MontoCeroIncorrecto()
    {
        _transaccionParaTest2.Monto = 0;
    }

    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void MontoMenorACeroIncorrecto()
    {
        _transaccionParaTest2.Monto = -1;
    }

    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void MontoNuloIncorrecto()
    {
        _transaccionParaTest2.Monto = null;
    }

    [TestMethod]
    public void MontoCorrecto()
    {
        Assert.AreEqual(80, _transaccionParaTest2.Monto);
    }

    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void CuentaNulaIncorrecta()
    {
        _cuentaMonetariaParaTest = null;
        _transaccionParaTest2.Cuenta = (_cuentaMonetariaParaTest);
    }

    [TestMethod]
    public void CuentaMonetariaCorrecta()
    {
        Assert.AreEqual(_cuentaMonetariaParaTest, _transaccionParaTest1.Cuenta);
    }

    [TestMethod]
    public void CuentaTarjetaDeCreditoCorrecta()
    {
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
        _transaccionParaTest2.Categoria = _categoriaParaTest;
    }

    [TestMethod]
    public void CategoriaActivaCorrecta()
    {
        _categoriaParaTest.Estatus = ConstantesCategoria.estatusActiva;
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
        Assert.AreEqual(ConstantesCategoria.tipoCosto, _transaccionParaTest1.Tipo);
    }

    [TestMethod]
    public void TipoEsIngresoCorrecto()
    {
        Assert.AreEqual(ConstantesCategoria.tipoIngreso, _transaccionParaTest2.Tipo);
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void MonedaVaciaIncorrecto()
    {
        _transaccionParaTest1.Moneda = null;
    }
}