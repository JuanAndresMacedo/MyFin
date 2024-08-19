using Dominio;
using Memoria;
using Memoria.BaseDeDatosRepositorios;
using MemoriaTest.Contexto;

namespace MemoriaTest;

[TestClass]
public class TipoDeCambioBDRepositorioTest
{
    private TipoDeCambioBDRepositorio _repositorioDeTipoDeCambioParaTest;
    private SQLContexto _contexto;

    private TipoDeCambio _tipoDeCambioParaTest1;
    private TipoDeCambio _tipoDeCambioParaTest2;
    private Moneda _monedaParaTest;
    private Usuario _usuarioParaTest;
    private Espacio _espacioParaTest;
    private DateTime _fechaParaTest1;

    [TestInitialize]
    public void Inicio()
    {
        FabricaSQLContexto fabricaSQLContext = new FabricaSQLContexto();
        _contexto = fabricaSQLContext.CrearContextoMemoria();

        _repositorioDeTipoDeCambioParaTest = new TipoDeCambioBDRepositorio(_contexto);

        _fechaParaTest1 = new DateTime(2023, 10, 1);

        _usuarioParaTest = new Usuario()
        {
            Id = 1,
            Correo = "hola@gmail.com",
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
            Nombre = "Pesos Uruguayos",
            SimboloMonetario = "UYU",
        };

        _tipoDeCambioParaTest1 = new TipoDeCambio()
        {
            UsuarioCreador = _usuarioParaTest,
            Espacio = _espacioParaTest,
            Fecha = _fechaParaTest1,
            ValorDeLaMoneda = 38.4f,
            Moneda = _monedaParaTest
        };

        _tipoDeCambioParaTest2 = new TipoDeCambio()
        {
            UsuarioCreador = _usuarioParaTest,
            Espacio = _espacioParaTest,
            Fecha = _fechaParaTest1,
            ValorDeLaMoneda = 41.2f,
            Moneda = _monedaParaTest
        };
    }

    [TestCleanup]
    public void Fin()
    {
        _contexto.Database.EnsureDeleted();
    }

    [TestMethod]
    public void IdAgregadaCorrectamente()
    {
        _repositorioDeTipoDeCambioParaTest.Agregar(_tipoDeCambioParaTest1);
        _repositorioDeTipoDeCambioParaTest.Agregar(_tipoDeCambioParaTest2);

        Assert.IsTrue(_tipoDeCambioParaTest1.Id == 1);
        Assert.IsTrue(_tipoDeCambioParaTest2.Id == 2);
    }

    [TestMethod]
    public void AgregarUnTipoDeCambioAlRepositorioCorrecto()
    {
        _repositorioDeTipoDeCambioParaTest.Agregar(_tipoDeCambioParaTest1);
        Assert.AreEqual(_tipoDeCambioParaTest1, _repositorioDeTipoDeCambioParaTest.Encontrar(
            x => x.Id == _tipoDeCambioParaTest1.Id));
    }

    [TestMethod]
    public void AgregarUnTipoDeCambioLoDevuelveCorrecto()
    {
        TipoDeCambio _TipoDeCambioAgregadoAlRepoParaTest =
            _repositorioDeTipoDeCambioParaTest.Agregar(_tipoDeCambioParaTest1);
        Assert.AreEqual(_TipoDeCambioAgregadoAlRepoParaTest, _tipoDeCambioParaTest1);
    }

    [TestMethod]
    public void NoEstaAgregadoElUnTipoDeCambioCorrecto()
    {
        Assert.AreEqual(null, _repositorioDeTipoDeCambioParaTest.Encontrar(
            x => x.Id == _tipoDeCambioParaTest1.Id));
    }

    [TestMethod]
    public void ListarTodosCorrecto()
    {
        _repositorioDeTipoDeCambioParaTest.Agregar(_tipoDeCambioParaTest1);
        _repositorioDeTipoDeCambioParaTest.Agregar(_tipoDeCambioParaTest2);
        Assert.IsTrue(_repositorioDeTipoDeCambioParaTest.ListarTodos().Contains(_tipoDeCambioParaTest1));
        Assert.IsTrue(_repositorioDeTipoDeCambioParaTest.ListarTodos().Contains(_tipoDeCambioParaTest2));
    }


    [TestMethod]
    public void EliminarUnUnTipoDeCambioQueNoExisteDevuelveNull()
    {
        Assert.AreEqual(null, _repositorioDeTipoDeCambioParaTest.Eliminar(_tipoDeCambioParaTest1.Id));
    }

    [TestMethod]
    public void EliminarUnUnTipoDeCambioCorrecto()
    {
        _repositorioDeTipoDeCambioParaTest.Agregar(_tipoDeCambioParaTest1);
        Assert.IsTrue(
            _tipoDeCambioParaTest1.Equals(_repositorioDeTipoDeCambioParaTest.Eliminar(_tipoDeCambioParaTest1.Id)));
        Assert.IsTrue(_repositorioDeTipoDeCambioParaTest.Encontrar
            (x => x.Id == _tipoDeCambioParaTest1.Id) == null);
    }

    [TestMethod]
    public void ActualizarUnTipoDeCambioCorrecto()
    {
        _repositorioDeTipoDeCambioParaTest.Agregar(_tipoDeCambioParaTest1);
        _tipoDeCambioParaTest2.Id = _tipoDeCambioParaTest1.Id;
        _repositorioDeTipoDeCambioParaTest.Actualizar(_tipoDeCambioParaTest2);

        Assert.AreEqual(_tipoDeCambioParaTest2.Espacio, _tipoDeCambioParaTest1.Espacio);
        Assert.AreEqual(_tipoDeCambioParaTest2.Fecha, _tipoDeCambioParaTest1.Fecha);
        Assert.AreEqual(_tipoDeCambioParaTest2.ValorDeLaMoneda,
            _tipoDeCambioParaTest1.ValorDeLaMoneda);
    }
}