using Dominio;
using Logica;
using Memoria;

namespace LogicaTest;

[TestClass]
public class ReporteCategoriasTest
{
    private TransaccionLogica _transaccionLogicaParaTest;
    private IRepositorio<Transaccion> _transaccionRepositorioParaTest;
    private IRepositorio<ObjetivoDeGasto> _objetivoDeGastoRepositorioParaTest;
    private TipoDeCambioLogica _tipoDeCambioLogicaParaTest;
    private IRepositorio<TipoDeCambio> _tipoDeCambioRepositorioParaTest;
    private EspacioLogica _espacioLogicaParaTest;
    private IRepositorio<Espacio> _espacioRepositorioParaTest;

    private Monetaria _monetariaParaTestPesos;
    private Monetaria _monetariaParaTestDolares;

    private Categoria _categoriaParaTestEducacion;
    private Categoria _categoriaParaTestSalidas;
    private Categoria _categoriaParaTestRopa;
    private Categoria _categoriaParaTestSupermercado;

    private Espacio _espacioParaTest1;

    private Usuario _usuarioParaTest1;

    private DateTime _fechaParaTest1;
    private DateTime _fechaParaTest2;
    private DateTime _fechaParaTest3;
    private DateTime _fechaParaTestActual;

    private ReportesLogica.RangoFechas _rangoDeFechasParaTest1;
    private ReportesLogica.RangoFechas _rangoDeFechasParaTest2;
    private ReportesLogica.RangoFechas _rangoDeFechasParaTest3;

    private TipoDeCambio _tipoDeCambioParaTest1;

    private Transaccion _transaccionParaTest1;
    private Transaccion _transaccionParaTest2;
    private Transaccion _transaccionParaTest3;
    private Transaccion _transaccionParaTest4;
    private Transaccion _transaccionParaTest5;
    private Transaccion _transaccionParaTest6;

    [TestInitialize]
    public void Inicio()
    {
        _transaccionRepositorioParaTest = new TransaccionMemoriaRepositorio();
        _transaccionLogicaParaTest = new TransaccionLogica(_transaccionRepositorioParaTest);

        _tipoDeCambioRepositorioParaTest = new TipoDeCambioMemoriaRepositorio();
        _tipoDeCambioLogicaParaTest = new TipoDeCambioLogica(_tipoDeCambioRepositorioParaTest);

        _espacioRepositorioParaTest = new EspacioMemoriaRepositorio();
        _espacioLogicaParaTest = new EspacioLogica(_espacioRepositorioParaTest);

        _fechaParaTest1 = new DateTime(2023, 9, 1);
        _fechaParaTest2 = new DateTime(2023, 10, 31);
        _fechaParaTest3 = new DateTime(2023, 12, 15);

        _usuarioParaTest1 = new Usuario()
        {
            Correo = "juan@adinet.com",
            Contrasena = "juanBall12345",
            Nombre = "Juan",
            Apellido = "Min",
            Direccion = "Av.China"
        };

        _espacioParaTest1 = new Espacio()
        {
            Nombre = "Familia",
            Administrador = _usuarioParaTest1,
        };

        _monetariaParaTestPesos = new Monetaria()
        {
            Nombre = "Pesos",
            FechaDeCreacion = _fechaParaTest1,
            Moneda = "UYU",
            Monto = 250000f,
            Propietario = _usuarioParaTest1,
            Espacio = _espacioParaTest1
        };

        _monetariaParaTestDolares = new Monetaria()
        {
            Nombre = "Pesos",
            FechaDeCreacion = _fechaParaTest1,
            Moneda = "US$",
            Monto = 1500f,
            Propietario = _usuarioParaTest1,
            Espacio = _espacioParaTest1
        };

        _categoriaParaTestEducacion = new Categoria()
        {
            Espacio = _espacioParaTest1,
            UsuarioCreador = _usuarioParaTest1,
            Nombre = "Eduacion",
            FechaDeCreacion = _fechaParaTest1,
            Tipo = "Costo",
            Estatus = "Activa",
        };

        _categoriaParaTestSalidas = new Categoria()
        {
            Espacio = _espacioParaTest1,
            UsuarioCreador = _usuarioParaTest1,
            Nombre = "Salidas",
            FechaDeCreacion = _fechaParaTest1,
            Tipo = "Costo",
            Estatus = "Activa",
        };

        _categoriaParaTestRopa = new Categoria()
        {
            Espacio = _espacioParaTest1,
            UsuarioCreador = _usuarioParaTest1,
            Nombre = "Ropa",
            FechaDeCreacion = _fechaParaTest1,
            Tipo = "Costo",
            Estatus = "Activa",
        };

        _categoriaParaTestSupermercado = new Categoria()
        {
            Espacio = _espacioParaTest1,
            UsuarioCreador = _usuarioParaTest1,
            Nombre = "Supermercado",
            FechaDeCreacion = _fechaParaTest1,
            Tipo = "Costo",
            Estatus = "Activa",
        };

        _transaccionParaTest1 = new Transaccion()
        {
            Nombre = "Pago GAP",
            Fecha = _fechaParaTest1,
            Monto = 1000f,
            Moneda = "US$",
            Categoria = _categoriaParaTestRopa,
            Cuenta = _monetariaParaTestDolares,
            Espacio = _espacioParaTest1,
            Tipo = "Costo"
        };

        _transaccionParaTest2 = new Transaccion()
        {
            Nombre = "1ER Pago ingles octb",
            Fecha = _fechaParaTest1,
            Monto = 10000f,
            Moneda = "UYU",
            Categoria = _categoriaParaTestEducacion,
            Cuenta = _monetariaParaTestPesos,
            Espacio = _espacioParaTest1,
            Tipo = "Costo"
        };

        _transaccionParaTest3 = new Transaccion()
        {
            Nombre = "2DO Pago ingles octb",
            Fecha = _fechaParaTest1,
            Monto = 10000f,
            Moneda = "UYU",
            Categoria = _categoriaParaTestEducacion,
            Cuenta = _monetariaParaTestPesos,
            Espacio = _espacioParaTest1,
            Tipo = "Costo"
        };

        _transaccionParaTest4 = new Transaccion()
        {
            Nombre = "TndaInglesa",
            Fecha = _fechaParaTest1,
            Monto = 10000f,
            Moneda = "UYU",
            Categoria = _categoriaParaTestSupermercado,
            Cuenta = _monetariaParaTestPesos,
            Espacio = _espacioParaTest1,
            Tipo = "Costo"
        };

        _transaccionParaTest5 = new Transaccion()
        {
            Nombre = "1ER 2DO Pago ingles sept",
            Fecha = _fechaParaTest2,
            Monto = 37000f,
            Moneda = "UYU",
            Categoria = _categoriaParaTestEducacion,
            Cuenta = _monetariaParaTestPesos,
            Espacio = _espacioParaTest1,
            Tipo = "Costo"
        };

        _transaccionParaTest6 = new Transaccion()
        {
            Nombre = "Cumple 18",
            Fecha = _fechaParaTest1,
            Monto = 10000f,
            Moneda = "UYU",
            Categoria = _categoriaParaTestSalidas,
            Cuenta = _monetariaParaTestPesos,
            Espacio = _espacioParaTest1,
            Tipo = "Costo"
        };

        _tipoDeCambioParaTest1 = new TipoDeCambio()
        {
            UsuarioCreador = _usuarioParaTest1,
            Espacio = _espacioParaTest1,
            Fecha = _fechaParaTest1,
            ValorDelDolar = 10f
        };

        _tipoDeCambioLogicaParaTest.AgregarTipoDeCambio(_tipoDeCambioParaTest1);
        _espacioLogicaParaTest.AgregarEspacio(_espacioParaTest1);
        _espacioLogicaParaTest.AsignarEspacioActual(_espacioParaTest1.Id);

        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest1);
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest2);
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest3);
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest4);
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest5);
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest6);
    }

    [TestMethod]
    public void ReportePorCategoriaEducacionCorrecto()
    {
        List<ReportesLogica.DatosPorCategoria> _reportePorCategoria =
            ReportesLogica.ReportePorCategoria(_transaccionLogicaParaTest, _espacioParaTest1, _fechaParaTest1,
                _tipoDeCambioLogicaParaTest);

        Assert.AreEqual(_reportePorCategoria[1].Categoria, _categoriaParaTestEducacion);
        Assert.AreEqual(_reportePorCategoria[1].SumaDeGastos, 20000);
        Assert.AreEqual(_reportePorCategoria[1].Total, 50000);
        Assert.AreEqual(_reportePorCategoria[1].Porcentaje, 40);
    }

    [TestMethod]
    public void ReportePorCategoriaSalidasCorrecto()
    {
        List<ReportesLogica.DatosPorCategoria> _reportePorCategoria =
            ReportesLogica.ReportePorCategoria(_transaccionLogicaParaTest, _espacioParaTest1, _fechaParaTest1,
                _tipoDeCambioLogicaParaTest);

        Assert.AreEqual(_reportePorCategoria[3].Categoria, _categoriaParaTestSalidas);
        Assert.AreEqual(_reportePorCategoria[3].SumaDeGastos, 10000);
        Assert.AreEqual(_reportePorCategoria[3].Total, 50000);
        Assert.AreEqual(_reportePorCategoria[3].Porcentaje, 20);
    }

    [TestMethod]
    public void ReportePorCategoriaRopaCorrecto()
    {
        List<ReportesLogica.DatosPorCategoria> _reportePorCategoria =
            ReportesLogica.ReportePorCategoria(_transaccionLogicaParaTest, _espacioParaTest1, _fechaParaTest1,
                _tipoDeCambioLogicaParaTest);

        Assert.AreEqual(_reportePorCategoria[0].Categoria, _categoriaParaTestRopa);
        Assert.AreEqual(_reportePorCategoria[0].SumaDeGastos, 10000);
        Assert.AreEqual(_reportePorCategoria[0].Total, 50000);
        Assert.AreEqual(_reportePorCategoria[0].Porcentaje, 20);
    }

    [TestMethod]
    public void ReportePorCategoriaSupermercadoCorrecto()
    {
        List<ReportesLogica.DatosPorCategoria> _reportePorCategoria =
            ReportesLogica.ReportePorCategoria(_transaccionLogicaParaTest, _espacioParaTest1, _fechaParaTest1,
                _tipoDeCambioLogicaParaTest);

        Assert.AreEqual(_reportePorCategoria[2].Categoria, _categoriaParaTestSupermercado);
        Assert.AreEqual(_reportePorCategoria[2].SumaDeGastos, 10000);
        Assert.AreEqual(_reportePorCategoria[2].Total, 50000);
        Assert.AreEqual(_reportePorCategoria[2].Porcentaje, 20);
    }

    [TestMethod]
    public void ReportePorCategoriaEducacionCambiaElMesCorrecto()
    {
        List<ReportesLogica.DatosPorCategoria> _reportePorCategoria =
            ReportesLogica.ReportePorCategoria(_transaccionLogicaParaTest, _espacioParaTest1, _fechaParaTest2,
                _tipoDeCambioLogicaParaTest);

        Assert.AreEqual(_reportePorCategoria[0].Categoria, _categoriaParaTestEducacion);
        Assert.AreEqual(_reportePorCategoria[0].SumaDeGastos, 37000);
        Assert.AreEqual(_reportePorCategoria[0].Total, 37000);
        Assert.AreEqual(_reportePorCategoria[0].Porcentaje, 100);
    }

    [TestMethod]
    public void ReportePorCategoriaNoHayNadaEnElMesCorrecto()
    {
        List<ReportesLogica.DatosPorCategoria> _reportePorCategoria =
            ReportesLogica.ReportePorCategoria(_transaccionLogicaParaTest, _espacioParaTest1, _fechaParaTest3,
                _tipoDeCambioLogicaParaTest);

        Assert.AreEqual(0, _reportePorCategoria.Count);
    }
}