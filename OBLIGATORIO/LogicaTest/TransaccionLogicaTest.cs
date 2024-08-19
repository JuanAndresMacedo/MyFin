using Dominio;
using Dominio.Constantes;
using Logica;
using LogicaTest.Contexto;
using Memoria;
using Memoria.BaseDeDatosRepositorios;

namespace LogicaTest;

[TestClass]
public class TransaccionLogicaTest
{
    private SQLContexto _contexto;
    
    private TransaccionLogica _transaccionLogicaParaTest;
    private TransaccionBDRepositorio _transaccionRepositorioParaTest;
    
    private TipoDeCambioLogica _tipoDeCambioLogicaParaTest;
    private TipoDeCambioBDRepositorio _tipoDeCambioRepositorioParaTest;
    
    private Transaccion _transaccionParaTest1;
    private Transaccion _transaccionParaTest2;
    private Transaccion _transaccionParaTest3;
    private Transaccion _transaccionParaTest4;
    private Transaccion _transaccionParaTest5;
    private Transaccion _transaccionParaTest6;
    
    private Monetaria _monetariaParaTest1;
    private Monetaria _monetariaParaTest2;
    
    private Usuario _usuarioParaTest;
    private Categoria _categoriaParaTest1;
    private Espacio _espacioParaTest;
    
    private DateTime _fechaParaTest;
    
    private TipoDeCambio _tipoDeCambioParaTest1;
    private TipoDeCambio _tipoDeCambioParaTest2;
    private TipoDeCambio _tipoDeCambioParaTest3;
    
    private Moneda _monedaParaTest1;
    private Moneda _monedaParaTest2;
    private Moneda _monedaParaTest3;

    [TestInitialize]
    public void Inicio()
    {
        FabricaSQLContexto fabricaSQLContext = new FabricaSQLContexto();
        _contexto = fabricaSQLContext.CrearContextoMemoria();
        
        _transaccionRepositorioParaTest = new TransaccionBDRepositorio(_contexto);
        _transaccionLogicaParaTest = new TransaccionLogica(_transaccionRepositorioParaTest);
        
        _tipoDeCambioRepositorioParaTest = new TipoDeCambioBDRepositorio(_contexto);
        _tipoDeCambioLogicaParaTest = new TipoDeCambioLogica(_tipoDeCambioRepositorioParaTest);

        _fechaParaTest = DateTime.Now;
        
        _usuarioParaTest = new Usuario()
        {
            Id= 1,
            Correo = "juan@adinet.com",
            Contrasena = "juanBall12345",
            Nombre = "Juan",
            Apellido = "Min",
            Direccion = "Av.China"
        };

        _espacioParaTest = new Espacio()
        {
            Id = 1,
            Nombre = "Familia",
            Administrador = _usuarioParaTest,
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
        
        _monedaParaTest3 = new Moneda()
        {
            Id = 3,
            Nombre = ConstantesMoneda.Euro,
            SimboloMonetario = "Є"
        };
        
        _monetariaParaTest1 = new Monetaria()
        {
            Id = 1,
            Nombre = "Ahorros",
            FechaDeCreacion = _fechaParaTest,
            Moneda = _monedaParaTest1,
            Monto = 3000f,
            Propietario = _usuarioParaTest
        };
        
        _monetariaParaTest2 = new Monetaria()
        {
            Id = 2,
            Nombre = "Hipoteca",
            FechaDeCreacion = _fechaParaTest,
            Moneda = _monedaParaTest2,
            Monto = 2000f,
            Propietario = _usuarioParaTest
        };

        _categoriaParaTest1 = new Categoria()
        {
            Id = 1,
            UsuarioCreador = _usuarioParaTest,
            Nombre = "Comida",
            FechaDeCreacion = _fechaParaTest,
            Tipo = ConstantesCategoria.tipoCosto,
            Estatus = ConstantesCategoria.estatusActiva,
        };
        
        _tipoDeCambioParaTest1 = new TipoDeCambio()
        {
            Id = 1,
            Moneda = _monedaParaTest1,
            UsuarioCreador = _usuarioParaTest,
            Espacio = _espacioParaTest,
            Fecha = new DateTime(2023, 10, 06),
            ValorDeLaMoneda = 38.4f
        };
        
        _tipoDeCambioParaTest2 = new TipoDeCambio()
        {
            Id = 2,
            Moneda = _monedaParaTest2,
            UsuarioCreador = _usuarioParaTest,
            Espacio = _espacioParaTest,
            Fecha = _fechaParaTest,
            ValorDeLaMoneda = 38.4f
        };
        
        _tipoDeCambioParaTest3 = new TipoDeCambio()
        {
            Id = 3,
            Moneda = _monedaParaTest2,
            UsuarioCreador = _usuarioParaTest,
            Espacio = _espacioParaTest,
            Fecha = new DateTime(2023, 10, 07),
            ValorDeLaMoneda = 38.4f
        };

        _transaccionParaTest1 = new Transaccion()
        {
            Nombre = "Pago",
            Fecha = _fechaParaTest,
            Monto = 100f,
            Moneda = _monedaParaTest1,
            Categoria = _categoriaParaTest1,
            Cuenta = _monetariaParaTest1,
            Espacio = _espacioParaTest,
            Tipo = ConstantesCategoria.tipoCosto
        };
        
        _transaccionParaTest2 = new Transaccion()
        {
            Nombre = "Cobro",
            Fecha = _fechaParaTest,
            Monto = 12f,
            Moneda = _monedaParaTest2,
            Categoria = _categoriaParaTest1,
            Cuenta = _monetariaParaTest2,
            Espacio = _espacioParaTest,
            Tipo = ConstantesCategoria.tipoIngreso
        };
        
        _transaccionParaTest3 = new Transaccion()
        {
            Nombre = "Ropa",
            Fecha = new DateTime(2023, 10, 07),
            Monto = 200f,
            Moneda = _monedaParaTest2,
            Categoria = _categoriaParaTest1,
            Cuenta = _monetariaParaTest2,
            Espacio = _espacioParaTest,
            Tipo = ConstantesCategoria.tipoCosto
        };
        
        _transaccionParaTest4 = new Transaccion()
        {
            Nombre = "Ropa",
            Fecha = new DateTime(2023, 10, 07),
            Monto = 50f,
            Moneda = _monedaParaTest2,
            Categoria = _categoriaParaTest1,
            Cuenta = _monetariaParaTest1,
            Espacio = _espacioParaTest,
            Tipo = ConstantesCategoria.tipoCosto
        };
        
        _transaccionParaTest5 = new Transaccion()
        {
            Nombre = "Ropa",
            Fecha = new DateTime(2023, 10, 07),
            Monto = 200f,
            Moneda = _monedaParaTest1,
            Categoria = _categoriaParaTest1,
            Cuenta = _monetariaParaTest2,
            Espacio = _espacioParaTest,
            Tipo = ConstantesCategoria.tipoCosto
        };
        
        _transaccionParaTest6 = new Transaccion()
        {
            Nombre = "Ropa",
            Fecha = new DateTime(2023, 10, 07),
            Monto = 200f,
            Moneda = _monedaParaTest3,
            Categoria = _categoriaParaTest1,
            Cuenta = _monetariaParaTest2,
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
    public void AgregarTransaccionCorrecta()
    {
        Transaccion agregada = _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest1);
        
        Assert.AreEqual(agregada.Nombre, _transaccionParaTest1.Nombre);
        Assert.AreEqual(agregada.Fecha, _transaccionParaTest1.Fecha);
        Assert.AreEqual(agregada.Monto, _transaccionParaTest1.Monto);
        Assert.AreEqual(agregada.Moneda, _transaccionParaTest1.Moneda);
        Assert.AreEqual(agregada.Categoria, _transaccionParaTest1.Categoria);
        Assert.AreEqual(agregada.Cuenta, _transaccionParaTest1.Cuenta);
    }

    [TestMethod]
    public void EncontrarTransaccionCorrecta()
    {
        Transaccion agregada = _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest1);
        
        Assert.AreEqual(agregada, _transaccionLogicaParaTest.EncontrarTransaccion(_transaccionParaTest1.Id));
    }

    [TestMethod]
    public void NoEncontrarTransaccionDevuelveNull()
    {
        Assert.AreEqual(null, _transaccionLogicaParaTest.EncontrarTransaccion(_transaccionParaTest1.Id));
    }

    [TestMethod]
    public void ListarTodasLasTransaccionesCorrecto()
    {
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest1);
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest2);
        
        Assert.IsTrue(_transaccionLogicaParaTest.ListarTransacciones().Contains(_transaccionParaTest1)
                      && _transaccionLogicaParaTest.ListarTransacciones().Contains(_transaccionParaTest2));
    }

    [TestMethod]
    public void EliminarUnaTransaccionQueNoExisteDevuelveNull()
    {
        Assert.AreEqual(null, _transaccionLogicaParaTest.EliminarTransaccion(_transaccionParaTest1.Id));
    }

    [TestMethod]
    public void EliminarTransaccionCorrecta()
    {
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest1);
        int largo = _transaccionLogicaParaTest.ListarTransacciones().Count;
        Assert.AreEqual(_transaccionParaTest1, _transaccionLogicaParaTest.EliminarTransaccion(_transaccionParaTest1.Id));
        Assert.IsTrue(_transaccionLogicaParaTest.ListarTransacciones().Count == largo - 1);
    }
    
    [TestMethod]
    public void ListarTodasLasTransaccionesDeUnEspacioCorrecto()
    {
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest1);
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest2);
        
        Assert.IsTrue(_transaccionLogicaParaTest.ListarTransaccionesDeUnEspacio(_espacioParaTest).Contains(_transaccionParaTest1)
                      && _transaccionLogicaParaTest.ListarTransaccionesDeUnEspacio(_espacioParaTest).Contains(_transaccionParaTest2));
    }
    
    [TestMethod]
    public void ActualizarTransaccionCorrecto()
    {
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest1);
        _transaccionParaTest2.Id = _transaccionParaTest1.Id;
        _transaccionLogicaParaTest.ActualizarTransaccion(_transaccionParaTest2);
        Assert.AreEqual(_transaccionParaTest2.Nombre, _transaccionParaTest1.Nombre);
        Assert.AreEqual(_transaccionParaTest2.Id, _transaccionParaTest1.Id);
    }
    
    [TestMethod]
    public void DuplicarTransaccionCorrecto()
    {
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest1);
        Transaccion _transaccionDuplicada = _transaccionLogicaParaTest.DuplicarTransaccion(_transaccionParaTest1);
        Assert.IsFalse(_transaccionDuplicada.Equals(_transaccionParaTest1));
        Assert.AreEqual(_transaccionDuplicada.Nombre, _transaccionParaTest1.Nombre);
        Assert.AreEqual(_transaccionDuplicada.Monto, _transaccionParaTest1.Monto);
        Assert.AreEqual(_transaccionDuplicada.Moneda, _transaccionParaTest1.Moneda);
        Assert.AreEqual(_transaccionDuplicada.Cuenta, _transaccionParaTest1.Cuenta);
        Assert.AreEqual(_transaccionDuplicada.Categoria, _transaccionParaTest1.Categoria);
        Assert.AreEqual(_transaccionDuplicada.Monto, _transaccionParaTest1.Monto);
        Assert.AreEqual(_transaccionDuplicada.Tipo, _transaccionParaTest1.Tipo);
        Assert.AreEqual(_transaccionDuplicada.Espacio, _transaccionParaTest1.Espacio);
    }
    
    [TestMethod]
    public void ListarTodasLasTransaccionesDeCostoUnEspacioCorrecto()
    {
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest1);
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest2);

        Assert.IsTrue(_transaccionLogicaParaTest.ListarCostosDeUnEspacio(_espacioParaTest)
            .Contains(_transaccionParaTest1));
        Assert.IsFalse(_transaccionLogicaParaTest.ListarCostosDeUnEspacio(_espacioParaTest)
            .Contains(_transaccionParaTest2));
    }
    
    [TestMethod]
    [ExpectedException(typeof(LogicaExcepcion))]
    public void AgregarTransaccionEnDolaresSinTipoDeCambioParaEsaFechaIncorrecto()
    {
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest3);
        _tipoDeCambioLogicaParaTest.AgregarTipoDeCambio(_tipoDeCambioParaTest1);
        _tipoDeCambioLogicaParaTest.AgregarTipoDeCambio(_tipoDeCambioParaTest2);
        
        _transaccionLogicaParaTest.ValidarExistenciaDeTipoDeCambioParaFechaDeTransaccion
            (_transaccionParaTest3, _espacioParaTest, _tipoDeCambioLogicaParaTest);
    }
    
    [TestMethod]
    public void AgregarTransaccionEnDolaresSiHayTipoDeCambioParaEsaFechaCorrecto()
    {
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest3);
        _tipoDeCambioLogicaParaTest.AgregarTipoDeCambio(_tipoDeCambioParaTest1);
        _tipoDeCambioLogicaParaTest.AgregarTipoDeCambio(_tipoDeCambioParaTest2);
        
        _transaccionLogicaParaTest.ValidarExistenciaDeTipoDeCambioParaFechaDeTransaccion
            (_transaccionParaTest2, _espacioParaTest, _tipoDeCambioLogicaParaTest);
    }
    
    [TestMethod]
    [ExpectedException(typeof(LogicaExcepcion))]
    public void AgregarTransaccionEnDolaresACuentaPesosUruguayosSinTipoDeCambioIncorrecto()
    {
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest4);
        
        _transaccionLogicaParaTest.ValidarExistenciaDeTipoDeCambioParaFechaDeTransaccion
            (_transaccionParaTest4, _espacioParaTest, _tipoDeCambioLogicaParaTest);
    }
    
    [TestMethod]
    public void AgregarTransaccionEnDolaresACuentaPesosUruguayosConTipoDeCambioCorrecto()
    {
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest4);
        _tipoDeCambioLogicaParaTest.AgregarTipoDeCambio(_tipoDeCambioParaTest3);
        
        _transaccionLogicaParaTest.ValidarExistenciaDeTipoDeCambioParaFechaDeTransaccion
            (_transaccionParaTest4, _espacioParaTest, _tipoDeCambioLogicaParaTest);
    }
    
    [TestMethod]
    [ExpectedException(typeof(LogicaExcepcion))]
    public void AgregarTransaccionEnPesosUruguayosACuentaDolaresSinTipoDeCambioIncorrecto()
    {
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest5);
        
        _transaccionLogicaParaTest.ValidarExistenciaDeTipoDeCambioParaFechaDeTransaccion
            (_transaccionParaTest5, _espacioParaTest, _tipoDeCambioLogicaParaTest);
    }
    
    [TestMethod]
    public void AgregarTransaccionEnPesosUruguayosACuentaDolaresConTipoDeCambioCorrecto()
    {
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest5);
        _tipoDeCambioLogicaParaTest.AgregarTipoDeCambio(_tipoDeCambioParaTest3);
        
        _transaccionLogicaParaTest.ValidarExistenciaDeTipoDeCambioParaFechaDeTransaccion
            (_transaccionParaTest5, _espacioParaTest, _tipoDeCambioLogicaParaTest);
    }
    
    [TestMethod]
    [ExpectedException(typeof(LogicaExcepcion))]
    public void AgregarTransaccionEnEurosACuentaDolaresSinTipoDeCambioIncorrecto()
    {
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest6);
        
        _transaccionLogicaParaTest.ValidarExistenciaDeTipoDeCambioParaFechaDeTransaccion
            (_transaccionParaTest6, _espacioParaTest, _tipoDeCambioLogicaParaTest);
    }
    
    [TestMethod]
    public void DescontarDineroACuentaMismaMonedaQueTransaccionCorrecto()
    {
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest1);
        
        _transaccionLogicaParaTest.MovimientoDeDinero
            (_transaccionParaTest1, _tipoDeCambioLogicaParaTest);
        
        Assert.AreEqual(3000-100, _monetariaParaTest1.DevolverDineroCuenta());
    }
    
    [TestMethod]
    public void AumentarDineroACuentaMismaMonedaQueTransaccionCorrecto()
    {
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest2);
        
        _transaccionLogicaParaTest.MovimientoDeDinero
            (_transaccionParaTest2, _tipoDeCambioLogicaParaTest);
        
        Assert.AreEqual(2000+12, _monetariaParaTest2.DevolverDineroCuenta());
    }
    
    [TestMethod]
    public void DescontarDineroACuentaEnDolaresConTransaccionEnPesosUruguayosCorrecto()
    {
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest5);
        _tipoDeCambioLogicaParaTest.AgregarTipoDeCambio(_tipoDeCambioParaTest3);
        
        _transaccionLogicaParaTest.MovimientoDeDinero
            (_transaccionParaTest5, _tipoDeCambioLogicaParaTest);

        Assert.AreEqual( 2000-(200/38.4f), 
            _monetariaParaTest2.DevolverDineroCuenta());
    }
    
    [TestMethod]
    public void DescontarDineroACuentaEnPesosUruguayosConTransaccionEnDolaresCorrecto()
    {
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest4);
        _tipoDeCambioLogicaParaTest.AgregarTipoDeCambio(_tipoDeCambioParaTest3);
        
        _transaccionLogicaParaTest.MovimientoDeDinero
            (_transaccionParaTest4, _tipoDeCambioLogicaParaTest);

        Assert.AreEqual( (3000-(50*38.4f)),  
            (_monetariaParaTest1.DevolverDineroCuenta()));
    }
}