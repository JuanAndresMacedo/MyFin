using Dominio;
using Dominio.Constantes;
using Logica;
using LogicaTest.Contexto;
using Memoria;
using Memoria.BaseDeDatosRepositorios;

namespace LogicaTest;

[TestClass]
public class MonetariaLogicaTest
{
    private SQLContexto _contexto;
    
    private MonetariaLogica _monetariaLogicaParaTest;
    private MonetariaBDRepositorio _cuentaRepositorioParaTest;
    
    private TransaccionLogica _transaccionLogicaParaTest;
    private TransaccionBDRepositorio _transaccionRepositorioParaTest;
    
    private Monetaria _monetariaParaTest1;
    private Monetaria _monetariaParaTest2;
    private Monetaria _monetariaParaTest3;
    
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
        
        _cuentaRepositorioParaTest = new MonetariaBDRepositorio(_contexto);
        _monetariaLogicaParaTest = new MonetariaLogica(_cuentaRepositorioParaTest);
                
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

        _monetariaParaTest1 = new Monetaria()
        {
            Nombre = "Ahorros",
            FechaDeCreacion = DateTime.Now,
            Moneda = _monedaParaTest1,
            Monto = 239999944f,
            Propietario = _usuarioParaTest,
            Espacio = _espacioParaTest,
        };

        _monetariaParaTest2 = new Monetaria()
        {
            Nombre = "Gastos",
            FechaDeCreacion = DateTime.Today,
            Moneda = _monedaParaTest2,
            Monto = 239999944f,
            Propietario = _usuarioParaTest,
            Espacio = _espacioParaTest
        };
        
        _monetariaParaTest3 = new Monetaria()
        {
            Nombre = "Gastos",
            FechaDeCreacion = DateTime.Today,
            Moneda = _monedaParaTest2,
            Monto = 239999944f,
            Propietario = _usuarioParaTest,
            Espacio = _espacioParaTest
        };
        
        _transaccionParaTest = new Transaccion()
        {
            Id = 1,
            Nombre = "Pago",
            Fecha = _fechaParaTest,
            Monto = 3431f,
            Moneda = _monedaParaTest1,
            Categoria = _categoriaParaTest1,
            Cuenta = _monetariaParaTest1,
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
    public void AgregarCuentaMonetariaCorrecto()
    {
        Cuenta agregada = _monetariaLogicaParaTest.AgregarMonetaria(_monetariaParaTest1);
        Monetaria monetariaAgregada = (Monetaria)agregada;
        Assert.AreEqual(_monetariaParaTest1.Nombre, monetariaAgregada.Nombre);
        Assert.AreEqual(_monetariaParaTest1.FechaDeCreacion, monetariaAgregada.FechaDeCreacion);
        Assert.AreEqual(_monetariaParaTest1.Moneda, monetariaAgregada.Moneda);
        Assert.AreEqual(_monetariaParaTest1.Monto, monetariaAgregada.Monto);
        Assert.AreEqual(_monetariaParaTest1.Propietario, monetariaAgregada.Propietario);
    }

    [TestMethod]
    public void EncontrarCuentaMonetariaCorrecto()
    {
        Cuenta agregada = _monetariaLogicaParaTest.AgregarMonetaria(_monetariaParaTest1);
        Assert.AreEqual(agregada, _monetariaLogicaParaTest.EncontrarMonetaria(_monetariaParaTest1.Id));
    }

    [TestMethod]
    public void EncontrarCuentaMonetariaNoEstaDevuelveNuloCorrecto()
    {
        Assert.AreEqual(null, _monetariaLogicaParaTest.EncontrarMonetaria(_monetariaParaTest1.Id));
    }

    [TestMethod]
    public void ListarTodasLasCuentasMonetariasCorrecto()
    {
        _monetariaLogicaParaTest.AgregarMonetaria(_monetariaParaTest1);
        _monetariaLogicaParaTest.AgregarMonetaria(_monetariaParaTest2);
        Assert.IsTrue(
            _monetariaLogicaParaTest.ListarMonetarias()
                .Contains(_monetariaParaTest1)
            &&
            _monetariaLogicaParaTest.ListarMonetarias()
                .Contains(_monetariaParaTest2));
    }

    [TestMethod]
    public void EliminarCuentaMonetariaInexistenteDevuelveNull()
    {
        Assert.AreEqual(null, _monetariaLogicaParaTest.
            EliminarMonetaria(_monetariaParaTest1.Id));
    }

    [TestMethod]
    public void EliminarCuentaMonetariaCorrecto()
    {
        _monetariaLogicaParaTest.AgregarMonetaria(_monetariaParaTest1);
        int largo = _monetariaLogicaParaTest.ListarMonetarias().Count;
        Assert.AreEqual(_monetariaParaTest1, _monetariaLogicaParaTest.EliminarMonetaria(_monetariaParaTest1.Id));
        Assert.IsTrue(_monetariaLogicaParaTest.ListarMonetarias().Count == largo - 1);
    }

    [TestMethod]
    [ExpectedException(typeof(LogicaExcepcion))]
    public void CrearCuentaMonetariaYaExisteIncorrecto()
    {
        _monetariaLogicaParaTest.AgregarMonetaria(_monetariaParaTest1);
        _monetariaLogicaParaTest.AgregarMonetaria(_monetariaParaTest1);
    }

    [TestMethod]
    public void ActualizarCuentaMonetariaCorrecto()
    {
        _monetariaLogicaParaTest.AgregarMonetaria(_monetariaParaTest1);

        Monetaria _cuentaActualizada = new Monetaria()
        {
            Id = _monedaParaTest1.Id,
            Moneda = _monedaParaTest1,
            Monto = 1200,
            Nombre = _monetariaParaTest1.Nombre,
            Propietario = _usuarioParaTest,
            Espacio = _espacioParaTest,
        };

        _monetariaLogicaParaTest.ActualizarMonetaria(_cuentaActualizada);

        Assert.AreEqual(1200, _monetariaParaTest1.Monto);
    }
    
    [TestMethod]
    public void ListarTodasLasCuentasMonetariasDeUnEspacioCorrecto()
    {
        _monetariaLogicaParaTest.AgregarMonetaria(_monetariaParaTest1);
        _monetariaLogicaParaTest.AgregarMonetaria(_monetariaParaTest2);

        Assert.IsTrue(_monetariaLogicaParaTest.ListarMonetariasDeUnEspacio(_espacioParaTest).Contains
            (_monetariaParaTest2));
        Assert.IsTrue (_monetariaLogicaParaTest.ListarMonetariasDeUnEspacio(_espacioParaTest).Contains
            (_monetariaParaTest1));
    }
    
    [TestMethod]
    public void ListarCuentasMonetariasDeUnEspacioPorMoneda()
    {
        Cuenta retornoCuenta1 = _monetariaLogicaParaTest.AgregarMonetaria(_monetariaParaTest1);
        Cuenta retornoCuenta2 = _monetariaLogicaParaTest.AgregarMonetaria(_monetariaParaTest2);

        List<Monetaria> cuentasPrueba = _monetariaLogicaParaTest.
            ListarMonetariasDeUnEspacioPorMoneda(_espacioParaTest, _monedaParaTest1);
        
        Assert.IsTrue(cuentasPrueba.Contains(retornoCuenta1));
        Assert.IsFalse(cuentasPrueba.Contains(retornoCuenta2));
    }
    
    [TestMethod]
    public void ValidarSiCuentaMonetariaTieneUnaTransacciónAsociadaCorrecto()
    {
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest);
        
        Assert.IsTrue(_monetariaLogicaParaTest.ValidarSiMonetariaTieneUnaTransacciónAsociada(
            _monetariaParaTest1, _espacioParaTest, _transaccionLogicaParaTest));
    }
    
    [TestMethod]
    public void EditarCuentaMonetariaYaExisteNoTiraExcepcionCorrecto()
    {
        _monetariaLogicaParaTest.AgregarMonetaria(_monetariaParaTest1);
        _monetariaLogicaParaTest.ActualizarMonetaria(_monetariaParaTest1);
        Assert.IsTrue(_monetariaLogicaParaTest.ListarMonetarias().Contains(_monetariaParaTest1));
    }
    
    [TestMethod]
    [ExpectedException(typeof(LogicaExcepcion))]
    public void EditarCuentaMonetariaYaExisteIncorrecto()
    {
        _monetariaLogicaParaTest.AgregarMonetaria(_monetariaParaTest1);
        _monetariaLogicaParaTest.AgregarMonetaria(_monetariaParaTest2);
        _monetariaParaTest2.Nombre = "CM1";
        _monetariaLogicaParaTest.ActualizarMonetaria(_monetariaParaTest2);
        _monetariaParaTest1.Nombre = "CM1";
        _monetariaLogicaParaTest.ActualizarMonetaria(_monetariaParaTest1);
    }
}