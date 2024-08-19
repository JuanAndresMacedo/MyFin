using Dominio;
using Dominio.Constantes;
using Logica;
using LogicaTest.Contexto;
using Memoria;
using Memoria.BaseDeDatosRepositorios;
using Memoria.SesionActual;

namespace LogicaTest;

[TestClass]
public class ReporteCategoriasTest
{
    private SQLContexto _contexto;
    
    private ReporteCategoriaLogica _reporteCategoriaLogicaParaTest;
    
    private TransaccionBDRepositorio _transaccionRepositorioParaTest;
    private TransaccionLogica _transaccionLogicaParaTest;
    
    private ObjetivoDeGastoBDRepositorio _objetivoDeGastoRepositorioParaTest;
    
    private TipoDeCambioBDRepositorio _tipoDeCambioRepositorioParaTest;
    private TipoDeCambioLogica _tipoDeCambioLogicaParaTest;
    
    private EspacioBDRepositorio _espacioRepositorioParaTest;
    private EspacioLogica _espacioLogicaParaTest;
    private SesionActualMemoria _sesionActualParaTest;
    
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

    private ReporteCategoriaLogica.RangoFechas _rangoDeFechasParaTest1;
    private ReporteCategoriaLogica.RangoFechas _rangoDeFechasParaTest2;
    private ReporteCategoriaLogica.RangoFechas _rangoDeFechasParaTest3;

    private TipoDeCambio _tipoDeCambioParaTest1;

    private Transaccion _transaccionParaTest1;
    private Transaccion _transaccionParaTest2;
    private Transaccion _transaccionParaTest3;
    private Transaccion _transaccionParaTest4;
    private Transaccion _transaccionParaTest5;
    private Transaccion _transaccionParaTest6;
    
    private Moneda _monedaParaTest1;
    private Moneda _monedaParaTest2;

    [TestInitialize]
    public void Inicio()
    {
        FabricaSQLContexto fabricaSQLContext = new FabricaSQLContexto();
        _contexto = fabricaSQLContext.CrearContextoMemoria();

        _reporteCategoriaLogicaParaTest = new ReporteCategoriaLogica();
        
        _transaccionRepositorioParaTest = new TransaccionBDRepositorio(_contexto);
        _transaccionLogicaParaTest = new TransaccionLogica(_transaccionRepositorioParaTest);

        _tipoDeCambioRepositorioParaTest = new TipoDeCambioBDRepositorio(_contexto);
        _tipoDeCambioLogicaParaTest = new TipoDeCambioLogica(_tipoDeCambioRepositorioParaTest);

        _sesionActualParaTest = new SesionActualMemoria();
        _espacioRepositorioParaTest = new EspacioBDRepositorio(_contexto);
        _espacioLogicaParaTest = new EspacioLogica(_espacioRepositorioParaTest, _sesionActualParaTest);
        
        _fechaParaTest1 = new DateTime(2023, 9, 1);
        _fechaParaTest2 = new DateTime(2023, 10, 31);
        _fechaParaTest3 = new DateTime(2023, 12, 15);

        _usuarioParaTest1 = new Usuario()
        {
            Id = 1,
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
        
        _monedaParaTest1 = new Moneda()
        {
            Id = 1,
            Nombre = ConstantesMoneda.PesoUruguayo,
            SimboloMonetario = "UYU"
        };

        _monedaParaTest2 = new Moneda()
        {
            Id = 2,
            Nombre = ConstantesMoneda.Dolar,
            SimboloMonetario = "U$S"
        };

        _monetariaParaTestPesos = new Monetaria()
        {
            Id = 1,
            Nombre = "Pesos",
            FechaDeCreacion = _fechaParaTest1,
            Moneda = _monedaParaTest1,
            Monto = 250000f,
            Propietario = _usuarioParaTest1,
            Espacio = _espacioParaTest1
        };

        _monetariaParaTestDolares = new Monetaria()
        {
            Id = 2,
            Nombre = "Dolares",
            FechaDeCreacion = _fechaParaTest1,
            Moneda = _monedaParaTest2,
            Monto = 1500f,
            Propietario = _usuarioParaTest1,
            Espacio = _espacioParaTest1
        };

        _categoriaParaTestEducacion = new Categoria()
        {
            Id = 1,
            Espacio = _espacioParaTest1,
            UsuarioCreador = _usuarioParaTest1,
            Nombre = "Eduacion",
            FechaDeCreacion = _fechaParaTest1,
            Tipo = ConstantesCategoria.tipoCosto,
            Estatus = ConstantesCategoria.estatusActiva,
        };

        _categoriaParaTestSalidas = new Categoria()
        {
            Id = 2,
            Espacio = _espacioParaTest1,
            UsuarioCreador = _usuarioParaTest1,
            Nombre = "Salidas",
            FechaDeCreacion = _fechaParaTest1,
            Tipo = ConstantesCategoria.tipoCosto,
            Estatus = ConstantesCategoria.estatusActiva,
        };

        _categoriaParaTestRopa = new Categoria()
        {
            Id = 3,
            Espacio = _espacioParaTest1,
            UsuarioCreador = _usuarioParaTest1,
            Nombre = "Ropa",
            FechaDeCreacion = _fechaParaTest1,
            Tipo = ConstantesCategoria.tipoCosto,
            Estatus = ConstantesCategoria.estatusActiva,
        };

        _categoriaParaTestSupermercado = new Categoria()
        {
            Id = 4,
            Espacio = _espacioParaTest1,
            UsuarioCreador = _usuarioParaTest1,
            Nombre = "Supermercado",
            FechaDeCreacion = _fechaParaTest1,
            Tipo = ConstantesCategoria.tipoCosto,
            Estatus = ConstantesCategoria.estatusActiva,
        };

        _transaccionParaTest1 = new Transaccion()
        {
            Nombre = "Pago GAP",
            Fecha = _fechaParaTest1,
            Monto = 1000f,
            Moneda = _monedaParaTest2,
            Categoria = _categoriaParaTestRopa,
            Cuenta = _monetariaParaTestDolares,
            Espacio = _espacioParaTest1,
            Tipo = ConstantesCategoria.tipoCosto
        };

        _transaccionParaTest2 = new Transaccion()
        {
            Nombre = "1ER Pago ingles octb",
            Fecha = _fechaParaTest1,
            Monto = 10000f,
            Moneda = _monedaParaTest1,
            Categoria = _categoriaParaTestEducacion,
            Cuenta = _monetariaParaTestPesos,
            Espacio = _espacioParaTest1,
            Tipo = ConstantesCategoria.tipoCosto
        };

        _transaccionParaTest3 = new Transaccion()
        {
            Nombre = "2DO Pago ingles octb",
            Fecha = _fechaParaTest1,
            Monto = 10000f,
            Moneda = _monedaParaTest1,
            Categoria = _categoriaParaTestEducacion,
            Cuenta = _monetariaParaTestPesos,
            Espacio = _espacioParaTest1,
            Tipo = ConstantesCategoria.tipoCosto
        };

        _transaccionParaTest4 = new Transaccion()
        {
            Nombre = "TndaInglesa",
            Fecha = _fechaParaTest1,
            Monto = 10000f,
            Moneda = _monedaParaTest1,
            Categoria = _categoriaParaTestSupermercado,
            Cuenta = _monetariaParaTestPesos,
            Espacio = _espacioParaTest1,
            Tipo = ConstantesCategoria.tipoCosto
        };

        _transaccionParaTest5 = new Transaccion()
        {
            Nombre = "1ER 2DO Pago ingles sept",
            Fecha = _fechaParaTest2,
            Monto = 37000f,
            Moneda = _monedaParaTest1,
            Categoria = _categoriaParaTestEducacion,
            Cuenta = _monetariaParaTestPesos,
            Espacio = _espacioParaTest1,
            Tipo = ConstantesCategoria.tipoCosto
        };

        _transaccionParaTest6 = new Transaccion()
        {
            Nombre = "Cumple 18",
            Fecha = _fechaParaTest1,
            Monto = 10000f,
            Moneda = _monedaParaTest1,
            Categoria = _categoriaParaTestSalidas,
            Cuenta = _monetariaParaTestPesos,
            Espacio = _espacioParaTest1,
            Tipo = ConstantesCategoria.tipoCosto
        };

        _tipoDeCambioParaTest1 = new TipoDeCambio()
        {
            UsuarioCreador = _usuarioParaTest1,
            Espacio = _espacioParaTest1,
            Fecha = _fechaParaTest1,
            Moneda = _monedaParaTest2,
            ValorDeLaMoneda = 10f
        };
        _espacioLogicaParaTest.AgregarEspacio(_espacioParaTest1);
        _espacioLogicaParaTest.AsignarEspacioActual(_espacioParaTest1.Id);
        _tipoDeCambioLogicaParaTest.AgregarTipoDeCambio(_tipoDeCambioParaTest1);
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest1);
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest2);
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest3);
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest4);
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest5);
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest6);
    }
    
    [TestCleanup]
    public void Fin()
    {
        _contexto.Database.EnsureDeleted();
    }

    [TestMethod]
    public void ReportePorCategoriaEducacionCorrecto()
    {

        List<ReporteCategoriaLogica.DatosPorCategoria> _reportePorCategoria =
            _reporteCategoriaLogicaParaTest.ReportePorCategoria(_transaccionLogicaParaTest, _espacioParaTest1, _fechaParaTest1,
                _tipoDeCambioLogicaParaTest);

        Assert.AreEqual(_reportePorCategoria[1].Categoria, _categoriaParaTestEducacion);
        Assert.AreEqual(_reportePorCategoria[1].SumaDeGastos, 20000);
        Assert.AreEqual(_reportePorCategoria[1].Total, 50000);
        Assert.AreEqual(_reportePorCategoria[1].Porcentaje, 40);
    }

    [TestMethod]
    public void ReportePorCategoriaSalidasCorrecto()
    {
        List<ReporteCategoriaLogica.DatosPorCategoria> _reportePorCategoria =
            _reporteCategoriaLogicaParaTest.ReportePorCategoria(_transaccionLogicaParaTest, _espacioParaTest1, _fechaParaTest1,
                _tipoDeCambioLogicaParaTest);

        Assert.AreEqual(_reportePorCategoria[3].Categoria, _categoriaParaTestSalidas);
        Assert.AreEqual(_reportePorCategoria[3].SumaDeGastos, 10000);
        Assert.AreEqual(_reportePorCategoria[3].Total, 50000);
        Assert.AreEqual(_reportePorCategoria[3].Porcentaje, 20);
    }

    [TestMethod]
    public void ReportePorCategoriaRopaCorrecto()
    {
        List<ReporteCategoriaLogica.DatosPorCategoria> _reportePorCategoria =
            _reporteCategoriaLogicaParaTest.ReportePorCategoria(_transaccionLogicaParaTest, _espacioParaTest1, _fechaParaTest1,
                _tipoDeCambioLogicaParaTest);

        Assert.AreEqual(_reportePorCategoria[0].Categoria, _categoriaParaTestRopa);
        Assert.AreEqual(_reportePorCategoria[0].SumaDeGastos, 10000);
        Assert.AreEqual(_reportePorCategoria[0].Total, 50000);
        Assert.AreEqual(_reportePorCategoria[0].Porcentaje, 20);
    }

    [TestMethod]
    public void ReportePorCategoriaSupermercadoCorrecto()
    {
        List<ReporteCategoriaLogica.DatosPorCategoria> _reportePorCategoria =
            _reporteCategoriaLogicaParaTest.ReportePorCategoria(_transaccionLogicaParaTest, _espacioParaTest1, _fechaParaTest1,
                _tipoDeCambioLogicaParaTest);

        Assert.AreEqual(_reportePorCategoria[2].Categoria, _categoriaParaTestSupermercado);
        Assert.AreEqual(_reportePorCategoria[2].SumaDeGastos, 10000);
        Assert.AreEqual(_reportePorCategoria[2].Total, 50000);
        Assert.AreEqual(_reportePorCategoria[2].Porcentaje, 20);
    }

    [TestMethod]
    public void ReportePorCategoriaEducacionCambiaElMesCorrecto()
    {
        List<ReporteCategoriaLogica.DatosPorCategoria> _reportePorCategoria =
            _reporteCategoriaLogicaParaTest.ReportePorCategoria(_transaccionLogicaParaTest, _espacioParaTest1, _fechaParaTest2,
                _tipoDeCambioLogicaParaTest);

        Assert.AreEqual(_reportePorCategoria[0].Categoria, _categoriaParaTestEducacion);
        Assert.AreEqual(_reportePorCategoria[0].SumaDeGastos, 37000);
        Assert.AreEqual(_reportePorCategoria[0].Total, 37000);
        Assert.AreEqual(_reportePorCategoria[0].Porcentaje, 100);
    }

    [TestMethod]
    public void ReportePorCategoriaNoHayNadaEnElMesCorrecto()
    {
        List<ReporteCategoriaLogica.DatosPorCategoria> _reportePorCategoria =
            _reporteCategoriaLogicaParaTest.ReportePorCategoria(_transaccionLogicaParaTest, _espacioParaTest1, _fechaParaTest3,
                _tipoDeCambioLogicaParaTest);

        Assert.AreEqual(0, _reportePorCategoria.Count);
    }
}