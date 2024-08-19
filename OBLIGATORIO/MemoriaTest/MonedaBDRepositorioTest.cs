using Dominio;
using Memoria;
using Memoria.BaseDeDatosRepositorios;
using MemoriaTest.Contexto;

namespace MemoriaTest;

[TestClass]
public class MonedaBDRepositorioTest
{
    private MonedaBDRepositorio _repositorioDeMonedaParaTest;
    private SQLContexto _contexto;
    
    private Moneda _monedaParaTest1;
    private Moneda _monedaParaTest2;

    [TestInitialize]
    public void Inicio()
    { 
        FabricaSQLContexto fabricaSQLContext = new FabricaSQLContexto();
        _contexto = fabricaSQLContext.CrearContextoMemoria();
        
        _repositorioDeMonedaParaTest = new MonedaBDRepositorio(_contexto);
        
        _monedaParaTest1 = new Moneda()
        {
            Nombre = "Pesos Uruguayos",
            SimboloMonetario = "UYU"
        };
        
        _monedaParaTest2 = new Moneda()
        {
            Nombre = "Dolares",
            SimboloMonetario = "U$S"
        };
    }
    
    [TestCleanup]
    public void Fin()
    {
        _contexto.Database.EnsureDeleted();
    }

    [TestMethod]
    public void IdMonedaAgregadaCorrectamente()
    {
        _repositorioDeMonedaParaTest.Agregar(_monedaParaTest1);
        Assert.IsTrue(_monedaParaTest1.Id == 1);
    }
    
    [TestMethod]
    public void AgregarUnaMonedaAlRepositorioLaDevuelveCorreto()
    {
        Assert.AreEqual(_monedaParaTest1, 
            _repositorioDeMonedaParaTest.Agregar(_monedaParaTest1));
    }
    
    [TestMethod]
    public void AgregarUnaMonedaAlRepositorioCorrecto()
    {
        _repositorioDeMonedaParaTest.Agregar(_monedaParaTest1);
        Assert.AreEqual(_monedaParaTest1, 
            _repositorioDeMonedaParaTest.Encontrar(x => x.Id == _monedaParaTest1.Id));
    }
    
    [TestMethod]
    public void MonedaNoEstaAgregadaAlRepositorio()
    {
        Assert.AreEqual(null, 
            _repositorioDeMonedaParaTest.Encontrar(x => x.Id == _monedaParaTest1.Id));
    }
    
    [TestMethod]
    public void RepositorioMonedaDevuelveListaVacia()
    {
        Assert.IsTrue(_repositorioDeMonedaParaTest.ListarTodos().Count == 0);
    }
    
    [TestMethod]
    public void RepositorioMonedaDevuelveListaCompleta()
    {
        _repositorioDeMonedaParaTest.Agregar(_monedaParaTest1);
        _repositorioDeMonedaParaTest.Agregar(_monedaParaTest2);
        
        Assert.IsTrue(_repositorioDeMonedaParaTest.ListarTodos().
            Contains(_monedaParaTest1) && _repositorioDeMonedaParaTest.ListarTodos().
            Contains(_monedaParaTest2));
    }
}