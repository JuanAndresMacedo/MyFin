using Dominio;
using Logica;
using Memoria;

namespace LogicaTest;

[TestClass]
public class CuentaLogicaTest
{
    private CuentaLogica _cuentaLogicaParaTest;
    private CuentaMemoriaRepositorio _cuentaRepositorioParaTest;
    
    private TransaccionLogica _transaccionLogicaParaTest;
    private IRepositorio<Transaccion> _transaccionRepositorioParaTest;
    
    private Monetaria _cuentaMonetariaParaTest;
    private TarjetaDeCredito _cuentaTarjetaDeCreditoParaTest1;
    private TarjetaDeCredito _cuentaTarjetaDeCreditoParaTest2;
    
    private Usuario _usuarioParaTest;
    private Espacio _espacioParaTest;
    
    private Transaccion _transaccionParaTest;
    private Categoria _categoriaParaTest1;
    
    private DateTime _fechaParaTest;

    [TestInitialize]
    public void Inicio()
    {
        _cuentaRepositorioParaTest = new CuentaMemoriaRepositorio();
        _cuentaLogicaParaTest = new CuentaLogica(_cuentaRepositorioParaTest);
                
        _transaccionRepositorioParaTest = new TransaccionMemoriaRepositorio();
        _transaccionLogicaParaTest = new TransaccionLogica(_transaccionRepositorioParaTest);
        
        _fechaParaTest = DateTime.Now;

        _usuarioParaTest = new Usuario()
        {
            Correo = "juan@adinet.com",
            Contrasena = "juanBall12345",
            Nombre = "Juan",
            Apellido = "Min",
            Direccion = "Av.China"
        };

        _espacioParaTest = new Espacio()
        {
            Administrador = _usuarioParaTest,
            Nombre = "ViajeX"
        };

        _cuentaMonetariaParaTest = new Monetaria()
        {
            Nombre = "Ahorros",
            FechaDeCreacion = DateTime.Now,
            Moneda = "UYU",
            Monto = 239999944f,
            Propietario = _usuarioParaTest,
            Espacio = _espacioParaTest,
            Id = 9878
        };

        _cuentaTarjetaDeCreditoParaTest1 = new TarjetaDeCredito()
        {
            Nombre = "Gastos",
            FechaDeCreacion = DateTime.Today,
            FechaDeCierre = DateTime.Today.AddDays(2),
            Moneda = "US$",
            CreditoDisponible = 239999944f,
            Propietario = _usuarioParaTest,
            Espacio = _espacioParaTest
        };
        
        _cuentaTarjetaDeCreditoParaTest2 = new TarjetaDeCredito()
        {
            Nombre = "Gastos",
            FechaDeCreacion = DateTime.Today,
            FechaDeCierre = DateTime.Today,
            Moneda = "US$",
            CreditoDisponible = 239999944f,
            Propietario = _usuarioParaTest,
            Espacio = _espacioParaTest
        };
        
        _categoriaParaTest1 = new Categoria()
        {
            UsuarioCreador = _usuarioParaTest,
            Nombre = "Comida",
            FechaDeCreacion = DateTime.Now,
            Tipo = "Costo",
            Estatus = "Activa",
            Espacio = _espacioParaTest
        };
        
        _transaccionParaTest = new Transaccion()
        {
            Nombre = "Pago",
            Fecha = _fechaParaTest,
            Monto = 3431f,
            Moneda = "UYU",
            Categoria = _categoriaParaTest1,
            Cuenta = _cuentaMonetariaParaTest,
            Espacio = _espacioParaTest,
            Id = 23,
            Tipo = "Costo"
        };

        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest);
    }

    [TestMethod]
    public void AgregarCuentaCorrecta()
    {
        Cuenta agregada = _cuentaLogicaParaTest.AgregarCuenta(_cuentaMonetariaParaTest);
        Monetaria monetariaAgregada = (Monetaria)agregada;
        Assert.AreEqual(_cuentaMonetariaParaTest.Nombre, monetariaAgregada.Nombre);
        Assert.AreEqual(_cuentaMonetariaParaTest.FechaDeCreacion, monetariaAgregada.FechaDeCreacion);
        Assert.AreEqual(_cuentaMonetariaParaTest.Moneda, monetariaAgregada.Moneda);
        Assert.AreEqual(_cuentaMonetariaParaTest.Monto, monetariaAgregada.Monto);
        Assert.AreEqual(_cuentaMonetariaParaTest.Propietario, monetariaAgregada.Propietario);
    }

    [TestMethod]
    public void EncontrarCuentaCorrecta()
    {
        Cuenta agregada = _cuentaLogicaParaTest.AgregarCuenta(_cuentaMonetariaParaTest);
        Assert.AreEqual(agregada, _cuentaLogicaParaTest.EncontrarCuenta(_cuentaMonetariaParaTest.Id));
    }

    [TestMethod]
    public void EncontrarCuentaNoEstaDevuelveNuloCorrecta()
    {
        Assert.AreEqual(null, _cuentaLogicaParaTest.EncontrarCuenta(_cuentaMonetariaParaTest.Id));
    }

    [TestMethod]
    public void ListarTodasLasCuentasCorrecto()
    {
        _cuentaLogicaParaTest.AgregarCuenta(_cuentaMonetariaParaTest);
        _cuentaLogicaParaTest.AgregarCuenta(_cuentaTarjetaDeCreditoParaTest1);
        Assert.IsTrue(
            _cuentaLogicaParaTest.ListarCuentas()
                .Contains(_cuentaMonetariaParaTest)
            &&
            _cuentaLogicaParaTest.ListarCuentas()
                .Contains(_cuentaTarjetaDeCreditoParaTest1));
    }

    [TestMethod]
    public void EliminarCuentaInexistenteDevuelveNull()
    {
        Assert.AreEqual(null, _cuentaLogicaParaTest.EliminarCuenta(_cuentaMonetariaParaTest.Id));
    }

    [TestMethod]
    public void EliminarCuentaCorrecta()
    {
        _cuentaLogicaParaTest.AgregarCuenta(_cuentaMonetariaParaTest);
        int largo = _cuentaLogicaParaTest.ListarCuentas().Count;
        Assert.AreEqual(_cuentaMonetariaParaTest, _cuentaLogicaParaTest.EliminarCuenta(_cuentaMonetariaParaTest.Id));
        Assert.IsTrue(_cuentaLogicaParaTest.ListarCuentas().Count == largo - 1);
    }

    [TestMethod]
    public void DevolverTarjetasDeCreditoCorrecto()
    {
        _cuentaLogicaParaTest.AgregarCuenta(_cuentaTarjetaDeCreditoParaTest1);

        List<TarjetaDeCredito> tarjetasDeCredito =
            _cuentaLogicaParaTest.DevolverTarjetasDeCreditoDeUnEspacio(_espacioParaTest);

        Assert.IsTrue(tarjetasDeCredito.Contains(_cuentaTarjetaDeCreditoParaTest1));
    }

    [TestMethod]
    public void DevolverCuentasMonetariasCorrecto()
    {
        _cuentaLogicaParaTest.AgregarCuenta(_cuentaMonetariaParaTest);

        List<Monetaria> monetarias = _cuentaLogicaParaTest.DevolverCuentasMonetariasDeUnEspacio(_espacioParaTest);

        Assert.IsTrue(monetarias.Contains(_cuentaMonetariaParaTest));
    }

    [TestMethod]
    [ExpectedException(typeof(LogicaExcepcion))]
    public void CrearCuentaMonetariaYaExisteIncorrecto()
    {
        _cuentaLogicaParaTest.AgregarCuenta(_cuentaMonetariaParaTest);
        _cuentaLogicaParaTest.AgregarCuenta(_cuentaMonetariaParaTest);
    }

    [TestMethod]
    public void ActualizarCuentaCorrecto()
    {
        _cuentaLogicaParaTest.AgregarCuenta(_cuentaMonetariaParaTest);

        Monetaria _cuentaActualizada = new Monetaria()
        {
            Moneda = "UYU",
            Monto = 1200,
            Nombre = _cuentaMonetariaParaTest.Nombre,
            Propietario = _usuarioParaTest,
            Espacio = _espacioParaTest,
            Id = 9878
        };

        _cuentaLogicaParaTest.ActualizarCuenta(_cuentaActualizada);

        Assert.AreEqual(1200, _cuentaMonetariaParaTest.Monto);
    }
    
    [TestMethod]
    public void ListarTodasLasCuentasDeUnEspacioCorrecto()
    {
        _cuentaLogicaParaTest.AgregarCuenta(_cuentaMonetariaParaTest);
        _cuentaLogicaParaTest.AgregarCuenta(_cuentaTarjetaDeCreditoParaTest1);

        Assert.IsTrue(_cuentaLogicaParaTest.ListarCuentasDeUnEspacio(_espacioParaTest).Contains
            (_cuentaTarjetaDeCreditoParaTest1));
        Assert.IsTrue (_cuentaLogicaParaTest.ListarCuentasDeUnEspacio(_espacioParaTest).Contains
            (_cuentaMonetariaParaTest));
    }
    
    [TestMethod]
    public void ListarCuentasDeUnEspacioPorMoneda()
    {
        Cuenta retornoCuenta1 = _cuentaLogicaParaTest.AgregarCuenta(_cuentaMonetariaParaTest);
        Cuenta retornoCuenta2 = _cuentaLogicaParaTest.AgregarCuenta(_cuentaTarjetaDeCreditoParaTest1);

        List<Cuenta> cuentasPrueba = _cuentaLogicaParaTest.
            ListarCuentasDeUnEspacioPorMoneda(_espacioParaTest, "UYU");
        
        Assert.IsTrue(cuentasPrueba.Contains(retornoCuenta1));
        Assert.IsFalse(cuentasPrueba.Contains(retornoCuenta2));
    }
    
    [TestMethod]
    [ExpectedException(typeof(LogicaExcepcion))]
    public void CrearTarjetaDeCreditoFechaDeCierreIgualAFechaDeCreacionIncorrecto()
    {
        _cuentaLogicaParaTest.AgregarCuenta(_cuentaTarjetaDeCreditoParaTest2);
    }
    
    [TestMethod]
    public void ValidarSiCuentaTieneUnaTransacciónAsociadaCorrecto()
    {
        Assert.IsTrue(_cuentaLogicaParaTest.ValidarSiCuentaTieneUnaTransacciónAsociada(
            _cuentaMonetariaParaTest, _espacioParaTest, _transaccionLogicaParaTest));
    }
}