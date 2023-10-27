using Dominio;
using Logica;
using Memoria;

namespace LogicaTest;

[TestClass]
public class TiposDeCambioLogicaTest
{
    private TipoDeCambioLogica _tipoDeCambioLogicaParaTest;
    private IRepositorio<TipoDeCambio> _tipoDeCambioRepositorioParaTest;
    
    private TransaccionLogica _transaccionLogicaParaTest;
    private IRepositorio<Transaccion> _transaccionRepositorioParaTest;
    
    private Usuario _usuarioParaTest;
    private Espacio _espacioParaTest1;
    private Espacio _espacioParaTest2;
    
    private TipoDeCambio _tipoDeCambioParaTest1;
    private TipoDeCambio _tipoDeCambioParaTest2;
    private TipoDeCambio _tipoDeCambioParaTest3;
    
    private DateTime _fechaParaTest1;
    
    private Transaccion _transaccionParaTest1;
    private Categoria _categoriaParaTest1;
    private Monetaria _monetariaParaTest1;
    
    [TestInitialize]
    public void Inicio()
    {
        _tipoDeCambioRepositorioParaTest = new TipoDeCambioMemoriaRepositorio();
        _tipoDeCambioLogicaParaTest = new TipoDeCambioLogica(_tipoDeCambioRepositorioParaTest);
        
        _transaccionRepositorioParaTest = new TransaccionMemoriaRepositorio();
        _transaccionLogicaParaTest = new TransaccionLogica(_transaccionRepositorioParaTest);

        _fechaParaTest1 = new DateTime(2023,10,1);
        
        _usuarioParaTest = new Usuario()
        {
            Correo = "hola@gmail.com",
        };

        _espacioParaTest1 = new Espacio()
        {
            Nombre = "Familia",
            Administrador = _usuarioParaTest,
        };
        
        _espacioParaTest2 = new Espacio()
        {
            Nombre = "Amigos",
            Administrador = _usuarioParaTest,
        };

        _tipoDeCambioParaTest1 = new TipoDeCambio()
        {
            UsuarioCreador = _usuarioParaTest,
            Espacio = _espacioParaTest1,
            Fecha = _fechaParaTest1,
            ValorDelDolar = 38.4f
        };

        _tipoDeCambioParaTest2 = new TipoDeCambio()
        {
            UsuarioCreador = _usuarioParaTest,
            Espacio = _espacioParaTest1,
            Fecha = _fechaParaTest1,
            ValorDelDolar = 41.2f
        };
        
        _tipoDeCambioParaTest3 = new TipoDeCambio()
        {
            UsuarioCreador = _usuarioParaTest,
            Espacio = _espacioParaTest2,
            Fecha = new DateTime(2022,11,13),
            ValorDelDolar = 41.2f
        };
        
        _categoriaParaTest1 = new Categoria()
        {
            UsuarioCreador = _usuarioParaTest,
            Nombre = "Comida",
            FechaDeCreacion = DateTime.Today,
            Tipo = "Costo",
            Estatus = "Activa",
        };
        
        _monetariaParaTest1 = new Monetaria()
        {
            Nombre = "Ahorros",
            FechaDeCreacion = DateTime.Today,
            Moneda = "US$",
            Monto = 2344000000f,
            Propietario = _usuarioParaTest
        };
        
        _transaccionParaTest1 = new Transaccion()
        {
            Nombre = "Pago",
            Fecha = _fechaParaTest1,
            Monto = 3431f,
            Moneda = "US$",
            Categoria = _categoriaParaTest1,
            Cuenta = _monetariaParaTest1,
            Espacio = _espacioParaTest1,
            Id = 23,
            Tipo = "Costo"
        };
        
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest1);
    }

    [TestMethod]
    public void AgregarTipoDeCambioCorrecto()
    {
        TipoDeCambio agregado = _tipoDeCambioLogicaParaTest.AgregarTipoDeCambio(_tipoDeCambioParaTest1);
        
        Assert.AreEqual(_tipoDeCambioParaTest1.Espacio, agregado.Espacio);
        Assert.AreEqual(_tipoDeCambioParaTest1.UsuarioCreador, agregado.UsuarioCreador);
        Assert.AreEqual(_tipoDeCambioParaTest1.Fecha, agregado.Fecha);
        Assert.AreEqual(_tipoDeCambioParaTest1.ValorDelDolar, agregado.ValorDelDolar);
    }
    
    [TestMethod]
    [ExpectedException(typeof(LogicaExcepcion))]
    public void AgregarTipoDeCambioYaEstaIncorrecto()
    {
        _tipoDeCambioLogicaParaTest.AgregarTipoDeCambio(_tipoDeCambioParaTest1);
        _tipoDeCambioLogicaParaTest.AgregarTipoDeCambio(_tipoDeCambioParaTest2);
    }
    
    [TestMethod]
    public void EncontrarTipoDeCambioCorrecta()
    {
        TipoDeCambio agregado = _tipoDeCambioLogicaParaTest.AgregarTipoDeCambio(_tipoDeCambioParaTest1);
        Assert.AreEqual(agregado, _tipoDeCambioLogicaParaTest.EncontrarTipoDeCambio(_tipoDeCambioParaTest1.Id));
    }
    
    [TestMethod]
    public void EncontrarTipoDeCambioNoEstaDevuelveNuloCorrecta()
    {
        Assert.AreEqual(null, _tipoDeCambioLogicaParaTest.EncontrarTipoDeCambio(_tipoDeCambioParaTest1.Id));
    }
    
    [TestMethod]
    public void ListarTodosLosTiposDeCambioCorrecto()
    {
        _tipoDeCambioLogicaParaTest.AgregarTipoDeCambio(_tipoDeCambioParaTest1);
        _tipoDeCambioLogicaParaTest.AgregarTipoDeCambio(_tipoDeCambioParaTest3);
        Assert.IsTrue(
            _tipoDeCambioLogicaParaTest.ListarTiposDeCambio()
                .Contains(_tipoDeCambioParaTest1) 
            && 
            _tipoDeCambioLogicaParaTest.ListarTiposDeCambio()
                .Contains(_tipoDeCambioParaTest3));
    }

    [TestMethod]
    public void EliminarTipoDeCambioNoExistenteDevuelveNull()
    {
        Assert.AreEqual(null, _tipoDeCambioLogicaParaTest.EliminarTipoDeCambio(_tipoDeCambioParaTest1.Id));
    }

    [TestMethod]
    public void EliminarTipoDeCambioCorrecto()
    {
        _tipoDeCambioLogicaParaTest.AgregarTipoDeCambio(_tipoDeCambioParaTest1);
        int largo = _tipoDeCambioLogicaParaTest.ListarTiposDeCambio().Count;
        Assert.AreEqual(_tipoDeCambioParaTest1, _tipoDeCambioLogicaParaTest.EliminarTipoDeCambio(_tipoDeCambioParaTest1.Id));
        Assert.IsTrue(_tipoDeCambioLogicaParaTest.ListarTiposDeCambio().Count == largo - 1);
    }
    
    [TestMethod]
    public void ActualizarTipoDeCambioCorrecto()
    {
        _tipoDeCambioLogicaParaTest.AgregarTipoDeCambio(_tipoDeCambioParaTest1);
        _tipoDeCambioLogicaParaTest.ActualizarTipoDeCambio(_tipoDeCambioParaTest2);
        Assert.AreEqual(_tipoDeCambioParaTest2.Fecha, _tipoDeCambioParaTest1.Fecha);
        Assert.AreEqual(_tipoDeCambioParaTest2.ValorDelDolar, _tipoDeCambioParaTest1.ValorDelDolar);
        Assert.AreEqual(_tipoDeCambioParaTest2.Espacio, _tipoDeCambioParaTest1.Espacio);
    }
    
        
    [TestMethod]
    public void ListarTodasLasTransaccionesDeCostoUnEspacioCorrecto()
    {
        _tipoDeCambioLogicaParaTest.AgregarTipoDeCambio(_tipoDeCambioParaTest1);
        _tipoDeCambioLogicaParaTest.AgregarTipoDeCambio(_tipoDeCambioParaTest3);

        Assert.IsTrue(_tipoDeCambioLogicaParaTest.ListarTiposDeCambioDeUnEspacio(_espacioParaTest1)
            .Contains(_tipoDeCambioParaTest1));
        Assert.IsFalse(_tipoDeCambioLogicaParaTest.ListarTiposDeCambioDeUnEspacio(_espacioParaTest1)
            .Contains(_tipoDeCambioParaTest3));
    }
    
    [TestMethod]
    public void ValidarSiTipoDeCambioTieneUnaTransacciónEnDolaresEnSuFechaCorrecto()
    {
        _tipoDeCambioLogicaParaTest.AgregarTipoDeCambio(_tipoDeCambioParaTest1);
        Assert.IsTrue(_tipoDeCambioLogicaParaTest.ValidarSiTipoDeCambioTieneUnaTransacciónEnDolaresEnSuFecha(
            _tipoDeCambioParaTest1, _espacioParaTest1, _transaccionLogicaParaTest));
    }
}