using Dominio;
using Logica;
using Memoria;

namespace LogicaTest;

[TestClass]
public class TransaccionLogicaTest
{
    private TransaccionLogica _transaccionLogicaParaTest;
    private IRepositorio<Transaccion> _transaccionRepositorioParaTest;
    
    private TipoDeCambioLogica _tipoDeCambioLogicaParaTest;
    private IRepositorio<TipoDeCambio> _tipoDeCambioRepositorioParaTest;
    
    private Transaccion _transaccionParaTest1;
    private Transaccion _transaccionParaTest2;
    private Transaccion _transaccionParaTest3;
    
    private Monetaria _monetariaParaTest1;
    private Monetaria _monetariaParaTest2;
    
    private Usuario _usuarioParaTest;
    private Categoria _categoriaParaTest1;
    private Espacio _espacioParaTest;
    private Espacio _espacioParaTest2;
    
    private DateTime _fechaParaTest;
    
    private TipoDeCambio _tipoDeCambioParaTest1;
    private TipoDeCambio _tipoDeCambioParaTest2;

    [TestInitialize]
    public void Inicio()
    {
        _transaccionRepositorioParaTest = new TransaccionMemoriaRepositorio();
        _transaccionLogicaParaTest = new TransaccionLogica(_transaccionRepositorioParaTest);
        
        _tipoDeCambioRepositorioParaTest = new TipoDeCambioMemoriaRepositorio();
        _tipoDeCambioLogicaParaTest = new TipoDeCambioLogica(_tipoDeCambioRepositorioParaTest);

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
            Nombre = "Familia",
            Administrador = _usuarioParaTest,
        };
        
        _espacioParaTest2 = new Espacio()
        {
            Nombre = "Amigos",
            Administrador = _usuarioParaTest,
        };
        
        _monetariaParaTest1 = new Monetaria()
        {
            Nombre = "Ahorros",
            FechaDeCreacion = _fechaParaTest,
            Moneda = "UYU",
            Monto = 2344000000f,
            Propietario = _usuarioParaTest
        };
        
        _monetariaParaTest2 = new Monetaria()
        {
            Nombre = "Hipoteca",
            FechaDeCreacion = _fechaParaTest,
            Moneda = "US$",
            Monto = 23440000f,
            Propietario = _usuarioParaTest
        };

        _categoriaParaTest1 = new Categoria()
        {
            UsuarioCreador = _usuarioParaTest,
            Nombre = "Comida",
            FechaDeCreacion = _fechaParaTest,
            Tipo = "Costo",
            Estatus = "Activa",
        };

        _transaccionParaTest1 = new Transaccion()
        {
            Nombre = "Pago",
            Fecha = _fechaParaTest,
            Monto = 3431f,
            Moneda = "UYU",
            Categoria = _categoriaParaTest1,
            Cuenta = _monetariaParaTest1,
            Espacio = _espacioParaTest,
            Id = 23,
            Tipo = "Costo"
        };
        
        _transaccionParaTest2 = new Transaccion()
        {
            Nombre = "Cobro",
            Fecha = _fechaParaTest,
            Monto = 3644f,
            Moneda = "US$",
            Categoria = _categoriaParaTest1,
            Cuenta = _monetariaParaTest2,
            Espacio = _espacioParaTest,
            Id = 2223,
            Tipo = "Ingreso"
        };
        
        _transaccionParaTest3 = new Transaccion()
        {
            Nombre = "Ropa",
            Fecha = new DateTime(2023, 10, 07),
            Monto = 200f,
            Moneda = "US$",
            Categoria = _categoriaParaTest1,
            Cuenta = _monetariaParaTest2,
            Espacio = _espacioParaTest,
            Id = 2224,
            Tipo = "Costo"
        };
        
        _tipoDeCambioParaTest1 = new TipoDeCambio()
        {
            UsuarioCreador = _usuarioParaTest,
            Espacio = _espacioParaTest,
            Fecha = new DateTime(2023, 10, 06),
            ValorDelDolar = 38.4f
        };
        
        _tipoDeCambioParaTest2 = new TipoDeCambio()
        {
            UsuarioCreador = _usuarioParaTest,
            Espacio = _espacioParaTest,
            Fecha = _fechaParaTest,
            ValorDelDolar = 38.4f
        };

        _tipoDeCambioLogicaParaTest.AgregarTipoDeCambio(_tipoDeCambioParaTest1);
        _tipoDeCambioLogicaParaTest.AgregarTipoDeCambio(_tipoDeCambioParaTest2);
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
        _transaccionParaTest2.Id = 23;
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest1);
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
        _transaccionLogicaParaTest.ValidarExistenciaDeTipoDeCambioParaFechaDeTransaccion
            (_transaccionParaTest3, _espacioParaTest, _tipoDeCambioLogicaParaTest);
    }
    
    [TestMethod]
    public void AgregarTransaccionEnDolaresSiHayTipoDeCambioParaEsaFechaCorrecto()
    {
        _transaccionLogicaParaTest.ValidarExistenciaDeTipoDeCambioParaFechaDeTransaccion
            (_transaccionParaTest2, _espacioParaTest, _tipoDeCambioLogicaParaTest);
    }
    
    [TestMethod]
    [ExpectedException(typeof(LogicaExcepcion))]
    public void AgregarTransaccionEnDolaresEnCuentaPesosIncorrecto()
    {
        _transaccionParaTest1.Cuenta.Moneda = "US$";
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest1);
    }
}