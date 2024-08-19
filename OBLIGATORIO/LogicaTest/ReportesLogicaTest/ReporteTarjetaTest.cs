using Dominio;
using Dominio.Constantes;
using Logica;
using LogicaTest.Contexto;
using Memoria;
using Memoria.BaseDeDatosRepositorios;
using Memoria.SesionActual;

namespace LogicaTest;

[TestClass]
public class ReporteTarjetaTest
{
    private SQLContexto _contexto;

    private ReportesTarjetasDeCreditoLogica _reportesTarjetasDeCreditoLogicaParaTest;
    private TransaccionBDRepositorio _transaccionRepositorioParaTest;
    private TransaccionLogica _transaccionLogicaParaTest;
    
    private ObjetivoDeGastoBDRepositorio _objetivoDeGastoRepositorioParaTest;
    private ObjetivoDeGastoLogica _objetivoDeGastoLogicaParaTest;
    
    private TipoDeCambioBDRepositorio _tipoDeCambioRepositorioParaTest;
    private TipoDeCambioLogica _tipoDeCambioLogicaParaTest;
    
    private EspacioBDRepositorio _espacioRepositorioParaTest;
    private EspacioLogica _espacioLogicaParaTest;
    private SesionActualMemoria _sesionActualParaTest;
    
    private Transaccion _transaccionParaTest1;
    private Transaccion _transaccionParaTest2;
    private Transaccion _transaccionParaTest3;
    private Transaccion _transaccionParaTest4;
    private Transaccion _transaccionParaTest5;
    
    private Espacio _espacioParaTest1;
    
    private TarjetaDeCredito _tarjetaDeCreditoParaTest1;
    private Cuenta _monetariaParaTest1;
    
    private Usuario _usuarioParaTest1;
    
    private Categoria _categoriaParaTest1;
    private Categoria _categoriaParaTest2;
    
    private DateTime _fechaParaTest1;
    private DateTime _fechaParaTest2;
    private DateTime _fechaParaTest3;
    private DateTime _fechaParaTest4;
    private DateTime _fechaParaTest5;

    private Moneda _monedaParaTest1;

    [TestInitialize]
    public void Inicio()
    {
        FabricaSQLContexto fabricaSQLContext = new FabricaSQLContexto();
        _contexto = fabricaSQLContext.CrearContextoMemoria();

        _reportesTarjetasDeCreditoLogicaParaTest = new ReportesTarjetasDeCreditoLogica();
        
        _transaccionRepositorioParaTest = new TransaccionBDRepositorio(_contexto);
        _transaccionLogicaParaTest = new TransaccionLogica(_transaccionRepositorioParaTest);

        _objetivoDeGastoRepositorioParaTest = new ObjetivoDeGastoBDRepositorio(_contexto);
        _objetivoDeGastoLogicaParaTest = new ObjetivoDeGastoLogica(_objetivoDeGastoRepositorioParaTest);

        _tipoDeCambioRepositorioParaTest = new TipoDeCambioBDRepositorio(_contexto);
        _tipoDeCambioLogicaParaTest = new TipoDeCambioLogica(_tipoDeCambioRepositorioParaTest);

        _sesionActualParaTest = new SesionActualMemoria();
        _espacioRepositorioParaTest = new EspacioBDRepositorio(_contexto);
        _espacioLogicaParaTest = new EspacioLogica(_espacioRepositorioParaTest, _sesionActualParaTest);

        _fechaParaTest1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        _fechaParaTest2 = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, DateTime.Now.AddDays(-45).Day);
        _fechaParaTest3 = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-2).Month, DateTime.Now.Day);
        _fechaParaTest4 = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, DateTime.Now.AddDays(1).Day);
        _fechaParaTest5 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-1).Day);
        
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
            Nombre = "Amigos",
            Administrador = _usuarioParaTest1,
            Id = 1
        };
        
        _monedaParaTest1 = new Moneda()
        {
            Id = 1,
            Nombre = ConstantesMoneda.PesoUruguayo,
            SimboloMonetario = "UYU"
        };
        
        _tarjetaDeCreditoParaTest1 = new TarjetaDeCredito()
        {
            Nombre = "Santander Credito",
            Id = 350,
            Propietario = _usuarioParaTest1,
            Espacio = _espacioParaTest1,
            Moneda = _monedaParaTest1,
            FechaDeCreacion = _fechaParaTest3,
            FechaDeCierre = _fechaParaTest1,
            BancoEmisor = "Santander",
            UltimosCuatroDigitos = "3455",
            CreditoDisponible = 100000f
        };
        
        _categoriaParaTest1 = new Categoria()
        {
            Id = 1,
            Espacio = _espacioParaTest1,
            UsuarioCreador = _usuarioParaTest1,
            Nombre = "Eduacion",
            FechaDeCreacion = _fechaParaTest1,
            Tipo = ConstantesCategoria.tipoCosto,
            Estatus = ConstantesCategoria.estatusActiva,
        };

        _categoriaParaTest2 = new Categoria()
        {
            Id = 2,
            Espacio = _espacioParaTest1,
            UsuarioCreador = _usuarioParaTest1,
            Nombre = "Salidas",
            FechaDeCreacion = _fechaParaTest1,
            Tipo = ConstantesCategoria.tipoCosto,
            Estatus = ConstantesCategoria.estatusActiva,
        };
        
        _transaccionParaTest1 = new Transaccion()
        {
            Nombre = "Pago GAP",
            Fecha = _fechaParaTest1,
            Monto = 10000f,
            Moneda = _monedaParaTest1,
            Categoria = _categoriaParaTest2,
            Cuenta = _tarjetaDeCreditoParaTest1,
            Espacio = _espacioParaTest1,
            Tipo = ConstantesCategoria.tipoCosto
        };

        _transaccionParaTest2 = new Transaccion()
        {
            Nombre = "1ER Pago ingles octb",
            Fecha = _fechaParaTest2,
            Monto = 10000f,
            Moneda = _monedaParaTest1,
            Categoria = _categoriaParaTest1,
            Cuenta = _tarjetaDeCreditoParaTest1,
            Espacio = _espacioParaTest1,
            Tipo = ConstantesCategoria.tipoCosto
        };

        _transaccionParaTest3 = new Transaccion()
        {
            Nombre = "2DO Pago ingles octb",
            Fecha = _fechaParaTest3,
            Monto = 10000f,
            Moneda = _monedaParaTest1,
            Categoria = _categoriaParaTest1,
            Cuenta = _tarjetaDeCreditoParaTest1,
            Espacio = _espacioParaTest1,
            Tipo = ConstantesCategoria.tipoCosto
        };

        _transaccionParaTest4 = new Transaccion()
        {
            Nombre = "TndaInglesa",
            Fecha = _fechaParaTest4,
            Monto = 10000f,
            Moneda = _monedaParaTest1,
            Categoria = _categoriaParaTest2,
            Cuenta = _tarjetaDeCreditoParaTest1,
            Espacio = _espacioParaTest1,
            Tipo = ConstantesCategoria.tipoCosto
        };

        _transaccionParaTest5 = new Transaccion()
        {
            Nombre = "1ER 2DO Pago ingles sept",
            Fecha = _fechaParaTest5,
            Monto = 2000f,
            Moneda = _monedaParaTest1,
            Categoria = _categoriaParaTest1,
            Cuenta = _tarjetaDeCreditoParaTest1,
            Espacio = _espacioParaTest1,
            Tipo = ConstantesCategoria.tipoCosto
        };

        _espacioLogicaParaTest.AgregarEspacio(_espacioParaTest1);
        _espacioLogicaParaTest.AsignarEspacioActual(_espacioParaTest1.Id);
        
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest1);
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest2);
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest3);
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest4);
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest5);
    }
    
    [TestCleanup]
    public void Fin()
    {
        _contexto.Database.EnsureDeleted();
    }
    
    [TestMethod]
    public void ListadoDeGastosUltimoMesCorrecto()
    {
        Assert.IsTrue(_reportesTarjetasDeCreditoLogicaParaTest
            .GastosDeUnaTarjetaDelUltimoMes(_transaccionLogicaParaTest, _espacioLogicaParaTest, _tarjetaDeCreditoParaTest1)
            .Contains(_transaccionParaTest1));
        Assert.IsTrue(_reportesTarjetasDeCreditoLogicaParaTest
            .GastosDeUnaTarjetaDelUltimoMes(_transaccionLogicaParaTest, _espacioLogicaParaTest, _tarjetaDeCreditoParaTest1)
            .Contains(_transaccionParaTest4));
        Assert.IsTrue(_reportesTarjetasDeCreditoLogicaParaTest
            .GastosDeUnaTarjetaDelUltimoMes(_transaccionLogicaParaTest, _espacioLogicaParaTest, _tarjetaDeCreditoParaTest1)
            .Contains(_transaccionParaTest5));
    }

    [TestMethod]
    public void NoListaGastosDeOtroMesCorrecto()
    {
        Assert.IsFalse(_reportesTarjetasDeCreditoLogicaParaTest
            .GastosDeUnaTarjetaDelUltimoMes(_transaccionLogicaParaTest, _espacioLogicaParaTest, _tarjetaDeCreditoParaTest1)
            .Contains(_transaccionParaTest2));
        Assert.IsFalse(_reportesTarjetasDeCreditoLogicaParaTest
            .GastosDeUnaTarjetaDelUltimoMes(_transaccionLogicaParaTest, _espacioLogicaParaTest, _tarjetaDeCreditoParaTest1)
            .Contains(_transaccionParaTest3));
    }
}