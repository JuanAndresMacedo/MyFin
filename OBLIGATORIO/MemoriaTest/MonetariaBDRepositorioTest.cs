using Dominio;
using Memoria;
using Memoria.BaseDeDatosRepositorios;
using MemoriaTest.Contexto;

namespace MemoriaTest;

[TestClass]
public class MonetariaBDRepositorioTest
{
    private MonetariaBDRepositorio _repositorioDeCuentaParaTest;
    private SQLContexto _contexto;

    private Monetaria _monetariaParaTest1;
    private Monetaria _monetariaParaTest2;
    private Cuenta _cuentaAgregadoAlRepoParaTest;

    private Usuario _usuarioParaTest;
    private Espacio _espacioParaTest;

    private Moneda _monedaParaTest1;

    [TestInitialize]
    public void Inicio()
    {
        FabricaSQLContexto fabricaSQLContext = new FabricaSQLContexto();
        _contexto = fabricaSQLContext.CrearContextoMemoria();

        _repositorioDeCuentaParaTest = new MonetariaBDRepositorio(_contexto);

        _usuarioParaTest = new Usuario()
        {
            Correo = "www@xxx.com"
        };

        _espacioParaTest = new Espacio()
        {
            Administrador = _usuarioParaTest,
            Nombre = "Ahorros",
        };

        _monedaParaTest1 = new Moneda()
        {
            Id = 1,
            Nombre = "UYU"
        };

        _monetariaParaTest2 = new Monetaria()
        {
            Nombre = "Nico",
            Moneda = _monedaParaTest1,
            Propietario = _usuarioParaTest,
            Espacio = _espacioParaTest,
        };

        _monetariaParaTest1 = new Monetaria()
        {
            Nombre = "NombCuenta",
            Propietario = _usuarioParaTest,
            Espacio = _espacioParaTest,
        };

        _cuentaAgregadoAlRepoParaTest = new Monetaria()
        {
            Propietario = _usuarioParaTest,
            Espacio = _espacioParaTest,
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
        _repositorioDeCuentaParaTest.Agregar(_monetariaParaTest1);
        _repositorioDeCuentaParaTest.Agregar(_monetariaParaTest2);

        Assert.IsTrue(_monetariaParaTest1.Id == 1);
        Assert.IsTrue(_monetariaParaTest2.Id == 2);
    }

    [TestMethod]
    public void AgregarUnaCuentaAlRepositorioCorrecto()
    {
        _repositorioDeCuentaParaTest.Agregar(_monetariaParaTest1);
        Assert.AreEqual(_monetariaParaTest1,
            _repositorioDeCuentaParaTest.Encontrar(x => x.Id == _monetariaParaTest1.Id));
    }

    [TestMethod]
    public void AgregarUnaCuentaLoDevuelveCorrecto()
    {
        _cuentaAgregadoAlRepoParaTest = _repositorioDeCuentaParaTest.Agregar(_monetariaParaTest1);
        Assert.AreEqual(_cuentaAgregadoAlRepoParaTest, _monetariaParaTest1);
    }

    [TestMethod]
    public void NoEstaAgregadoElUsuarioCorrecto()
    {
        _repositorioDeCuentaParaTest.Agregar(_monetariaParaTest1);
        Assert.AreEqual(null,
            _repositorioDeCuentaParaTest.Encontrar(x => x.Id == _monetariaParaTest2.Id));
    }

    [TestMethod]
    public void ListarTodosCorrecto()
    {
        _repositorioDeCuentaParaTest.Agregar(_monetariaParaTest1);
        _repositorioDeCuentaParaTest.Agregar(_monetariaParaTest2);
        Assert.IsTrue(
            _repositorioDeCuentaParaTest.ListarTodos()
                .Contains(_monetariaParaTest1)
            &&
            _repositorioDeCuentaParaTest.ListarTodos()
                .Contains(_monetariaParaTest2));
    }

    [TestMethod]
    public void EliminarUnaCuentaMonetariaQueNoExisteDevuelveNull()
    {
        Assert.AreEqual(null, _repositorioDeCuentaParaTest.Eliminar(_monetariaParaTest1.Id));
    }

    [TestMethod]
    public void EliminarUnaCuentaTarjDeCreditoQueNoExisteDevuelveNull()
    {
        Assert.AreEqual(null, _repositorioDeCuentaParaTest.Eliminar(_monetariaParaTest2.Id));
    }

    [TestMethod]
    public void EliminarCuentaMonetariaCorrecto()
    {
        _repositorioDeCuentaParaTest.Agregar(_monetariaParaTest1);
        Assert.IsTrue(
            _monetariaParaTest1.Equals(_repositorioDeCuentaParaTest.Eliminar(_monetariaParaTest1.Id))
            && _repositorioDeCuentaParaTest.Encontrar(x => x.Id == _monetariaParaTest2.Id) == null);
    }

    [TestMethod]
    public void EliminarCuentaTarjDeCreditoCorrecto()
    {
        _repositorioDeCuentaParaTest.Agregar(_monetariaParaTest2);
        Assert.IsTrue(
            _monetariaParaTest2.Equals(
                _repositorioDeCuentaParaTest.Eliminar(_monetariaParaTest2.Id))
            && _repositorioDeCuentaParaTest.Encontrar(x => x.Id == _monetariaParaTest2.Id) == null);
    }

    [TestMethod]
    public void ActualizarCuentaCorrecto()
    {
        _repositorioDeCuentaParaTest.Agregar(_monetariaParaTest2);

        Monetaria _cuentaActualizada = new Monetaria()
        {
            Moneda = _monedaParaTest1,
            Nombre = _monetariaParaTest2.Nombre,
            Propietario = _usuarioParaTest,
            Espacio = _espacioParaTest,
            Monto = 2000,
            Id = 1,
        };

        _repositorioDeCuentaParaTest.Actualizar(_cuentaActualizada);

        Assert.AreEqual(2000, _monetariaParaTest2.Monto);
    }
}