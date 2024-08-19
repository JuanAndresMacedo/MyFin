using Dominio;
using Memoria;
using Memoria.BaseDeDatosRepositorios;
using MemoriaTest.Contexto;

namespace MemoriaTest;

[TestClass]
public class TarjetaDeCreditoBDRepositorioTest
{
    private TarjetaDeCreditoBDRepositorio _repositorioDeCuentaParaTest;
    private SQLContexto _contexto;

    private TarjetaDeCredito _tarjetaDeCreditoParaTest1;
    private TarjetaDeCredito _tarjetaDeCreditoParaTest2;
    private Cuenta _cuentaAgregadoAlRepoParaTest;

    private Usuario _usuarioParaTest;
    private Espacio _espacioParaTest;

    private Moneda _monedaParaTest1;

    [TestInitialize]
    public void Inicio()
    {
        FabricaSQLContexto fabricaSQLContext = new FabricaSQLContexto();
        _contexto = fabricaSQLContext.CrearContextoMemoria();

        _repositorioDeCuentaParaTest = new TarjetaDeCreditoBDRepositorio(_contexto);

        _usuarioParaTest = new Usuario()
        {
            Id = 1,
            Correo = "www@xxx.com"
        };

        _espacioParaTest = new Espacio()
        {
            Id = 2,
            Administrador = _usuarioParaTest,
            Nombre = "Ahorros",
        };

        _monedaParaTest1 = new Moneda()
        {
            Id = 1,
            Nombre = "UYU"
        };

        _tarjetaDeCreditoParaTest2 = new TarjetaDeCredito()
        {
            Nombre = "Nico",
            BancoEmisor = "Santander",
            Moneda = _monedaParaTest1,
            UltimosCuatroDigitos = "4444",
            Propietario = _usuarioParaTest,
            Espacio = _espacioParaTest,
        };

        _tarjetaDeCreditoParaTest1 = new TarjetaDeCredito()
        {
            Nombre = "NombCuenta",
            Propietario = _usuarioParaTest,
            Espacio = _espacioParaTest,
        };

        _cuentaAgregadoAlRepoParaTest = new TarjetaDeCredito()
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
        _repositorioDeCuentaParaTest.Agregar(_tarjetaDeCreditoParaTest1);
        _repositorioDeCuentaParaTest.Agregar(_tarjetaDeCreditoParaTest2);

        Assert.IsTrue(_tarjetaDeCreditoParaTest1.Id == 1);
        Assert.IsTrue(_tarjetaDeCreditoParaTest2.Id == 2);
    }

    [TestMethod]
    public void AgregarUnaCuentaAlRepositorioCorrecto()
    {
        _repositorioDeCuentaParaTest.Agregar(_tarjetaDeCreditoParaTest1);
        Assert.AreEqual(_tarjetaDeCreditoParaTest1,
            _repositorioDeCuentaParaTest.Encontrar(x => x.Id == _tarjetaDeCreditoParaTest1.Id));
    }

    [TestMethod]
    public void AgregarUnaCuentaLoDevuelveCorrecto()
    {
        _cuentaAgregadoAlRepoParaTest = _repositorioDeCuentaParaTest.Agregar(_tarjetaDeCreditoParaTest1);
        Assert.AreEqual(_cuentaAgregadoAlRepoParaTest, _tarjetaDeCreditoParaTest1);
    }

    [TestMethod]
    public void NoEstaAgregadoElUsuarioCorrecto()
    {
        _repositorioDeCuentaParaTest.Agregar(_tarjetaDeCreditoParaTest1);
        Assert.AreEqual(null,
            _repositorioDeCuentaParaTest.Encontrar(x => x.Id == _tarjetaDeCreditoParaTest2.Id));
    }

    [TestMethod]
    public void ListarTodosCorrecto()
    {
        _repositorioDeCuentaParaTest.Agregar(_tarjetaDeCreditoParaTest1);
        _repositorioDeCuentaParaTest.Agregar(_tarjetaDeCreditoParaTest2);
        Assert.IsTrue(
            _repositorioDeCuentaParaTest.ListarTodos()
                .Contains(_tarjetaDeCreditoParaTest1)
            &&
            _repositorioDeCuentaParaTest.ListarTodos()
                .Contains(_tarjetaDeCreditoParaTest2));
    }

    [TestMethod]
    public void EliminarUnaCuentaMonetariaQueNoExisteDevuelveNull()
    {
        Assert.AreEqual(null, _repositorioDeCuentaParaTest.Eliminar(_tarjetaDeCreditoParaTest1.Id));
    }

    [TestMethod]
    public void EliminarUnaCuentaTarjDeCreditoQueNoExisteDevuelveNull()
    {
        Assert.AreEqual(null, _repositorioDeCuentaParaTest.Eliminar(_tarjetaDeCreditoParaTest2.Id));
    }

    [TestMethod]
    public void EliminarCuentaMonetariaCorrecto()
    {
        _repositorioDeCuentaParaTest.Agregar(_tarjetaDeCreditoParaTest1);
        Assert.IsTrue(
            _tarjetaDeCreditoParaTest1.Equals(_repositorioDeCuentaParaTest.Eliminar(_tarjetaDeCreditoParaTest1.Id))
            && _repositorioDeCuentaParaTest.Encontrar(x => x.Id == _tarjetaDeCreditoParaTest2.Id) == null);
    }

    [TestMethod]
    public void EliminarCuentaTarjDeCreditoCorrecto()
    {
        _repositorioDeCuentaParaTest.Agregar(_tarjetaDeCreditoParaTest2);
        Assert.IsTrue(
            _tarjetaDeCreditoParaTest2.Equals(
                _repositorioDeCuentaParaTest.Eliminar(_tarjetaDeCreditoParaTest2.Id))
            && _repositorioDeCuentaParaTest.Encontrar(x => x.Id == _tarjetaDeCreditoParaTest2.Id) == null);
    }

    [TestMethod]
    public void ActualizarCuentaCorrecto()
    {
        _repositorioDeCuentaParaTest.Agregar(_tarjetaDeCreditoParaTest2);

        TarjetaDeCredito _cuentaActualizada = new TarjetaDeCredito()
        {
            BancoEmisor = "BBVA",
            Moneda = _monedaParaTest1,
            UltimosCuatroDigitos = "1111",
            Nombre = _tarjetaDeCreditoParaTest2.Nombre,
            Propietario = _usuarioParaTest,
            Espacio = _espacioParaTest,
            Id = 1
        };

        _repositorioDeCuentaParaTest.Actualizar(_cuentaActualizada);

        Assert.AreEqual("BBVA", _tarjetaDeCreditoParaTest2.BancoEmisor);
    }
}