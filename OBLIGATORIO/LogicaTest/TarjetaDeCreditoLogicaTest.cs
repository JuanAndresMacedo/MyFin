using Dominio;
using Dominio.Constantes;
using Logica;
using LogicaTest.Contexto;
using Memoria;
using Memoria.BaseDeDatosRepositorios;

namespace LogicaTest;

[TestClass]
public class TarjetaDeCreditoLogicaTest
{
    private SQLContexto _contexto;

    private TarjetaDeCreditoLogica _tarjetaDeCreditoLogicaParaTest;
    private TarjetaDeCreditoBDRepositorio _cuentaRepositorioParaTest;
    
    private TransaccionLogica _transaccionLogicaParaTest;
    private TransaccionBDRepositorio _transaccionRepositorioParaTest;
    
    private TarjetaDeCredito _tarjetaDeCreditoParaTest1;
    private TarjetaDeCredito _tarjetaDeCreditoParaTest2;
    private TarjetaDeCredito _tarjetaDeCreditoParaTest3;
    
    private Usuario _usuarioParaTest;
    private Espacio _espacioParaTest;
    
    private Transaccion _transaccionParaTest;
    private Categoria _categoriaParaTest1;
    
    private DateTime _fechaParaTest;
    
    private Moneda _monedaParaTest1;
    private Moneda _monedaParaTest2;

    [TestInitialize]
    public void Inicio()
    {
        FabricaSQLContexto fabricaSQLContext = new FabricaSQLContexto();
        _contexto = fabricaSQLContext.CrearContextoMemoria();
        
        _cuentaRepositorioParaTest = new TarjetaDeCreditoBDRepositorio(_contexto);
        _tarjetaDeCreditoLogicaParaTest = new TarjetaDeCreditoLogica(_cuentaRepositorioParaTest);
                
        _transaccionRepositorioParaTest = new TransaccionBDRepositorio(_contexto);
        _transaccionLogicaParaTest = new TransaccionLogica(_transaccionRepositorioParaTest);
        
        _fechaParaTest = DateTime.Now;

        _usuarioParaTest = new Usuario()
        {
            Id = 1,
            Correo = "juan@adinet.com",
            Contrasena = "juanBall12345",
            Nombre = "Juan",
            Apellido = "Min",
            Direccion = "Av.China"
        };

        _espacioParaTest = new Espacio()
        {
            Id = 1,
            Administrador = _usuarioParaTest,
            Nombre = "ViajeX"
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
        
        _categoriaParaTest1 = new Categoria()
        {
            Id = 1,
            UsuarioCreador = _usuarioParaTest,
            Nombre = "Comida",
            FechaDeCreacion = DateTime.Now,
            Tipo = ConstantesCategoria.tipoCosto,
            Estatus = ConstantesCategoria.estatusActiva,
            Espacio = _espacioParaTest
        };

        _tarjetaDeCreditoParaTest1 = new TarjetaDeCredito()
        {
            Nombre = "Gastos",
            FechaDeCreacion = DateTime.Today,
            FechaDeCierre = DateTime.Today.AddDays(2),
            Moneda = _monedaParaTest2,
            UltimosCuatroDigitos = "1234",
            CreditoDisponible = 239999944f,
            Propietario = _usuarioParaTest,
            Espacio = _espacioParaTest,
            BancoEmisor = "Itau"
        };
        
        _tarjetaDeCreditoParaTest2 = new TarjetaDeCredito()
        {
            Nombre = "Gasto",
            FechaDeCreacion = DateTime.Today,
            FechaDeCierre = DateTime.Today.AddDays(2),
            Moneda = _monedaParaTest2,
            UltimosCuatroDigitos = "1234",
            CreditoDisponible = 239999944f,
            Propietario = _usuarioParaTest,
            Espacio = _espacioParaTest,
            BancoEmisor = "BBVA"
        };
        
        _tarjetaDeCreditoParaTest3 = new TarjetaDeCredito()
        {
            Nombre = "Ahorros",
            FechaDeCreacion = DateTime.Now,
            FechaDeCierre = DateTime.Today.AddDays(2),
            Moneda = _monedaParaTest1,
            UltimosCuatroDigitos = "1234",
            CreditoDisponible = 239999944f,
            Propietario = _usuarioParaTest,
            Espacio = _espacioParaTest,
        };
        
        _transaccionParaTest = new Transaccion()
        {
            Nombre = "Pago",
            Fecha = _fechaParaTest,
            Monto = 3431f,
            Moneda = _monedaParaTest1,
            Categoria = _categoriaParaTest1,
            Cuenta = _tarjetaDeCreditoParaTest3,
            Espacio = _espacioParaTest,
            Tipo = ConstantesCategoria.tipoCosto
        };
    }
    
    [TestCleanup]
    public void Fin()
    {
        _contexto.Database.EnsureDeleted();
    }

    [TestMethod]
    public void AgregarTarjetaDeCreditoCorrecto()
    {
        TarjetaDeCredito agregada = _tarjetaDeCreditoLogicaParaTest.AgregarTarjetaDeCredito(_tarjetaDeCreditoParaTest3);
        TarjetaDeCredito tarjetaDeCreditoAgregada = (TarjetaDeCredito)agregada;
        Assert.AreEqual(_tarjetaDeCreditoParaTest3.Nombre, tarjetaDeCreditoAgregada.Nombre);
        Assert.AreEqual(_tarjetaDeCreditoParaTest3.FechaDeCreacion, tarjetaDeCreditoAgregada.FechaDeCreacion);
        Assert.AreEqual(_tarjetaDeCreditoParaTest3.Moneda, tarjetaDeCreditoAgregada.Moneda);
        Assert.AreEqual(_tarjetaDeCreditoParaTest3.CreditoDisponible, tarjetaDeCreditoAgregada.CreditoDisponible);
        Assert.AreEqual(_tarjetaDeCreditoParaTest3.Propietario, tarjetaDeCreditoAgregada.Propietario);
    }

    [TestMethod]
    public void EncontrarCuentaCorrecta()
    {
        Cuenta agregada = _tarjetaDeCreditoLogicaParaTest.AgregarTarjetaDeCredito(_tarjetaDeCreditoParaTest3);
        Assert.AreEqual(agregada, _tarjetaDeCreditoLogicaParaTest.EncontrarTarjetaDeCredito(_tarjetaDeCreditoParaTest3.Id));
    }

    [TestMethod]
    public void EncontrarTarjetaDeCreditoNoEstaDevuelveNuloCorrecto()
    {
        Assert.AreEqual(null, _tarjetaDeCreditoLogicaParaTest.EncontrarTarjetaDeCredito(_tarjetaDeCreditoParaTest3.Id));
    }

    [TestMethod]
    public void ListarTodasLasTarjetasDeCreditoCorrecto()
    {
        _tarjetaDeCreditoLogicaParaTest.AgregarTarjetaDeCredito(_tarjetaDeCreditoParaTest3);
        _tarjetaDeCreditoLogicaParaTest.AgregarTarjetaDeCredito(_tarjetaDeCreditoParaTest1);
        Assert.IsTrue(
            _tarjetaDeCreditoLogicaParaTest.ListarTarjetasDeCredito()
                .Contains(_tarjetaDeCreditoParaTest3)
            &&
            _tarjetaDeCreditoLogicaParaTest.ListarTarjetasDeCredito()
                .Contains(_tarjetaDeCreditoParaTest1));
    }

    [TestMethod]
    public void EliminarTarjetaDeCreditoInexistenteDevuelveNull()
    {
        Assert.AreEqual(null, _tarjetaDeCreditoLogicaParaTest.EliminarTarjetaDeCredito(_tarjetaDeCreditoParaTest3.Id));
    }

    [TestMethod]
    public void EliminarTarjetaDeCreditoCorrecto()
    {
        _tarjetaDeCreditoLogicaParaTest.AgregarTarjetaDeCredito(_tarjetaDeCreditoParaTest3);
        int largo = _tarjetaDeCreditoLogicaParaTest.ListarTarjetasDeCredito().Count;
        Assert.AreEqual(_tarjetaDeCreditoParaTest3, _tarjetaDeCreditoLogicaParaTest.EliminarTarjetaDeCredito(_tarjetaDeCreditoParaTest3.Id));
        Assert.IsTrue(_tarjetaDeCreditoLogicaParaTest.ListarTarjetasDeCredito().Count == largo - 1);
    }

    [TestMethod]
    [ExpectedException(typeof(LogicaExcepcion))]
    public void CrearTarjetaDeCreditoYaExisteIncorrecto()
    {
        _tarjetaDeCreditoLogicaParaTest.AgregarTarjetaDeCredito(_tarjetaDeCreditoParaTest3);
        _tarjetaDeCreditoLogicaParaTest.AgregarTarjetaDeCredito(_tarjetaDeCreditoParaTest3);
    }

    [TestMethod]
    public void ActualizarTarjetaDeCreditoCorrecto()
    {
        _tarjetaDeCreditoLogicaParaTest.AgregarTarjetaDeCredito(_tarjetaDeCreditoParaTest3);

        TarjetaDeCredito _cuentaActualizada = new TarjetaDeCredito()
        {
            Id = _tarjetaDeCreditoParaTest3.Id,
            Moneda = _monedaParaTest1,
            CreditoDisponible = 1200,
            UltimosCuatroDigitos = "1234",
            Nombre = _tarjetaDeCreditoParaTest3.Nombre,
            Propietario = _usuarioParaTest,
            BancoEmisor = "Santander",
            Espacio = _espacioParaTest,
        };

        _tarjetaDeCreditoLogicaParaTest.ActualizarTarjetaDeCredito(_cuentaActualizada);

        Assert.AreEqual(1200, _tarjetaDeCreditoParaTest3.CreditoDisponible);
    }
    
    [TestMethod]
    public void ListarTodasLasTarjetasDeCreditoDeUnEspacioCorrecto()
    {
        _tarjetaDeCreditoLogicaParaTest.AgregarTarjetaDeCredito(_tarjetaDeCreditoParaTest3);
        _tarjetaDeCreditoLogicaParaTest.AgregarTarjetaDeCredito(_tarjetaDeCreditoParaTest1);

        Assert.IsTrue(_tarjetaDeCreditoLogicaParaTest.ListarTarjetasDeCreditoDeUnEspacio(_espacioParaTest).Contains
            (_tarjetaDeCreditoParaTest1));
        Assert.IsTrue (_tarjetaDeCreditoLogicaParaTest.ListarTarjetasDeCreditoDeUnEspacio(_espacioParaTest).Contains
            (_tarjetaDeCreditoParaTest3));
    }
    
    [TestMethod]
    public void ListarTarjetasDeCreditoDeUnEspacioPorMoneda()
    {
        TarjetaDeCredito retornoCuenta1 = _tarjetaDeCreditoLogicaParaTest.AgregarTarjetaDeCredito(_tarjetaDeCreditoParaTest3);
        TarjetaDeCredito retornoCuenta2 = _tarjetaDeCreditoLogicaParaTest.AgregarTarjetaDeCredito(_tarjetaDeCreditoParaTest1);

        List<TarjetaDeCredito> cuentasPrueba = _tarjetaDeCreditoLogicaParaTest.
            ListarTarjetasDeCreditoDeUnEspacioPorMoneda(_espacioParaTest, _monedaParaTest1);
        
        Assert.IsTrue(cuentasPrueba.Contains(retornoCuenta1));
        Assert.IsFalse(cuentasPrueba.Contains(retornoCuenta2));
    }
    
    [TestMethod]
    [ExpectedException(typeof(LogicaExcepcion))]
    public void CrearTarjetaDeCreditoFechaDeCierreIgualAFechaDeCreacionIncorrecto()
    {
        _tarjetaDeCreditoParaTest2.FechaDeCierre = DateTime.Today;
        _tarjetaDeCreditoLogicaParaTest.ActualizarTarjetaDeCredito(_tarjetaDeCreditoParaTest2);
        _tarjetaDeCreditoLogicaParaTest.AgregarTarjetaDeCredito(_tarjetaDeCreditoParaTest2);
    }
    
    [TestMethod]
    public void ValidarSiTarjetaDeCreditoTieneUnaTransacciónAsociadaCorrecto()
    {
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest);
        
        Assert.IsTrue(_tarjetaDeCreditoLogicaParaTest.ValidarSiTarjetaDeCreditoTieneUnaTransacciónAsociada(
            _tarjetaDeCreditoParaTest3, _espacioParaTest, _transaccionLogicaParaTest));
    }
    
    [TestMethod]
    public void EditarCuentaTarjetaDeCreditoYaExisteNoTiraExcepcionCorrecto()
    {
        _tarjetaDeCreditoLogicaParaTest.AgregarTarjetaDeCredito(_tarjetaDeCreditoParaTest1);
        _tarjetaDeCreditoLogicaParaTest.ActualizarTarjetaDeCredito(_tarjetaDeCreditoParaTest1);
        Assert.IsTrue(_tarjetaDeCreditoLogicaParaTest.ListarTarjetasDeCredito().Contains(_tarjetaDeCreditoParaTest1));
    }
    
    [TestMethod]
    [ExpectedException(typeof(LogicaExcepcion))]
    public void EditarCuentaMonetariaYaExisteIncorrecto()
    {
        _tarjetaDeCreditoLogicaParaTest.AgregarTarjetaDeCredito(_tarjetaDeCreditoParaTest1);
        _tarjetaDeCreditoLogicaParaTest.AgregarTarjetaDeCredito(_tarjetaDeCreditoParaTest2);
        _tarjetaDeCreditoParaTest2.Nombre = "CM1";
        _tarjetaDeCreditoLogicaParaTest.ActualizarTarjetaDeCredito(_tarjetaDeCreditoParaTest2);
        _tarjetaDeCreditoParaTest1.Nombre = "CM1";
        _tarjetaDeCreditoLogicaParaTest.ActualizarTarjetaDeCredito(_tarjetaDeCreditoParaTest1);
    }
}