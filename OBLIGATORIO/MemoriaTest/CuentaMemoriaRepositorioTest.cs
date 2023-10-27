using Dominio;
using Memoria;

namespace MemoriaTest;
[TestClass]
public class CuentaMemoriaRepositorioTest
{
    private CuentaMemoriaRepositorio _repositorioDeCuentaParaTest;
    
    private Monetaria _cuentaMonetariaParaTest;
    private TarjetaDeCredito _cuentaTarjetaDeCreditoParaTest;
    private Cuenta _cuentaAgregadoAlRepoParaTest;
    
    private Usuario _usuarioParaTest;
    private Espacio _espacioParaTest;

    [TestInitialize]
    public void Inicio()
    { 
        _repositorioDeCuentaParaTest = new CuentaMemoriaRepositorio();

        _usuarioParaTest = new Usuario()
        {
            Correo = "www@xxx.com"
        };
        
        _espacioParaTest = new Espacio()
        {
            Administrador = _usuarioParaTest,
            Nombre = "Ahorros",
        };
        
        _cuentaTarjetaDeCreditoParaTest = new TarjetaDeCredito()
        {
            Nombre = "Nico",
            BancoEmisor = "Santander",
            Moneda = "UYU",
            UltimosCuatroDigitos = "4444",
            Propietario = _usuarioParaTest,
            Espacio = _espacioParaTest,
            Id = 9878
        };

        _cuentaMonetariaParaTest = new Monetaria()
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
    
    [TestMethod]
    public void IdAgregadaCorrectamente()
    {
        _repositorioDeCuentaParaTest.Agregar(_cuentaMonetariaParaTest);
        _repositorioDeCuentaParaTest.Agregar(_cuentaTarjetaDeCreditoParaTest);
        
        Assert.IsTrue(_cuentaMonetariaParaTest.Id == 1);
        Assert.IsTrue(_cuentaTarjetaDeCreditoParaTest.Id == 2);
    }
    
    [TestMethod]
    public void AgregarUnaCuentaAlRepositorioCorrecto()
    {
        _repositorioDeCuentaParaTest.Agregar(_cuentaMonetariaParaTest);
        Assert.AreEqual(_cuentaMonetariaParaTest, 
            _repositorioDeCuentaParaTest.Encontrar(x => x.Id == _cuentaMonetariaParaTest.Id));
    }
    
    [TestMethod]
    public void AgregarUnaCuentaLoDevuelveCorrecto()
    {
        _cuentaAgregadoAlRepoParaTest = _repositorioDeCuentaParaTest.Agregar(_cuentaMonetariaParaTest);
        Assert.AreEqual(_cuentaAgregadoAlRepoParaTest, _cuentaMonetariaParaTest);
    }
    
    [TestMethod]
    public void NoEstaAgregadoElUsuarioCorrecto()
    {
        _repositorioDeCuentaParaTest.Agregar(_cuentaMonetariaParaTest);
        Assert.AreEqual(null, 
            _repositorioDeCuentaParaTest.Encontrar(x => x.Id == _cuentaTarjetaDeCreditoParaTest.Id));
    }
    
    [TestMethod]
    public void ListarTodosCorrecto()
    {
        _repositorioDeCuentaParaTest.Agregar(_cuentaMonetariaParaTest);
        _repositorioDeCuentaParaTest.Agregar(_cuentaTarjetaDeCreditoParaTest);
        Assert.IsTrue(
            _repositorioDeCuentaParaTest.ListarTodos()
                .Contains(_cuentaMonetariaParaTest) 
                      && 
            _repositorioDeCuentaParaTest.ListarTodos()
                .Contains(_cuentaTarjetaDeCreditoParaTest));
    }
    
    [TestMethod]
    public void EliminarUnaCuentaMonetariaQueNoExisteDevuelveNull()
    {
        Assert.AreEqual(null, _repositorioDeCuentaParaTest.Eliminar(_cuentaMonetariaParaTest.Id));
    }
    
    [TestMethod]
    public void EliminarUnaCuentaTarjDeCreditoQueNoExisteDevuelveNull()
    {
        Assert.AreEqual(null, _repositorioDeCuentaParaTest.Eliminar(_cuentaTarjetaDeCreditoParaTest.Id));
    }
    
    [TestMethod]
    public void EliminarCuentaMonetariaCorrecto()
    {
        _repositorioDeCuentaParaTest.Agregar(_cuentaMonetariaParaTest);
        Assert.IsTrue(_cuentaMonetariaParaTest.Equals(_repositorioDeCuentaParaTest.Eliminar(_cuentaMonetariaParaTest.Id))
                      && _repositorioDeCuentaParaTest.Encontrar(x => x.Id == _cuentaTarjetaDeCreditoParaTest.Id) == null);
    }
    
    [TestMethod]
    public void EliminarCuentaTarjDeCreditoCorrecto()
    {
        _repositorioDeCuentaParaTest.Agregar(_cuentaTarjetaDeCreditoParaTest);
        Assert.IsTrue(_cuentaTarjetaDeCreditoParaTest.Equals(_repositorioDeCuentaParaTest.Eliminar(_cuentaTarjetaDeCreditoParaTest.Id))
                      && _repositorioDeCuentaParaTest.Encontrar(x => x.Id == _cuentaTarjetaDeCreditoParaTest.Id) == null);
    }
    
    [TestMethod]
    public void ActualizarCuentaCorrecto()
    {
        _repositorioDeCuentaParaTest.Agregar(_cuentaTarjetaDeCreditoParaTest);

        TarjetaDeCredito _cuentaActualizada = new TarjetaDeCredito()
        {
            BancoEmisor = "BBVA",
            Moneda = "UYU",
            UltimosCuatroDigitos = "1111",
            Nombre = _cuentaTarjetaDeCreditoParaTest.Nombre,
            Propietario = _usuarioParaTest,
            Espacio = _espacioParaTest,
            Id = 9878
        };

        _repositorioDeCuentaParaTest.Actualizar(_cuentaActualizada);
        
        Assert.AreEqual( "BBVA", _cuentaTarjetaDeCreditoParaTest.BancoEmisor);
    }
}