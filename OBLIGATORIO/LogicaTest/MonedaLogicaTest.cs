using Dominio;
using Logica;
using LogicaTest.Contexto;
using Memoria;
using Memoria.BaseDeDatosRepositorios;

namespace LogicaTest;

[TestClass]
public class MonedaLogicaTest
{
    private MonedaBDRepositorio _monedaRepositorioParaTest;
    private MonedaLogica _monedaLogicaParaTest;
    private SQLContexto _contexto;
    
    private Moneda _monedaParaTest1;
    private Moneda _monedaParaTest2;

    [TestInitialize]
    public void Inicio()
    {
        FabricaSQLContexto fabricaSQLContext = new FabricaSQLContexto();
        _contexto = fabricaSQLContext.CrearContextoMemoria();

        _monedaRepositorioParaTest = new MonedaBDRepositorio(_contexto);
        _monedaLogicaParaTest = new MonedaLogica(_monedaRepositorioParaTest);
        
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
    public void AgregarMonedaCorrecto()
    {
        Moneda agregada = _monedaLogicaParaTest.AgregarMoneda(_monedaParaTest1);
        
        Assert.AreEqual(_monedaParaTest1.Id, agregada.Id);
        Assert.AreEqual(_monedaParaTest1.Nombre, agregada.Nombre);
        Assert.AreEqual(_monedaParaTest1.SimboloMonetario, agregada.SimboloMonetario);
    }
    
    [TestMethod]
    public void EncontrarUnaMonedaCorrecto()
    {
        Moneda agregada = _monedaLogicaParaTest.AgregarMoneda(_monedaParaTest1);
        Assert.AreEqual(agregada, 
            _monedaLogicaParaTest.EncontrarMoneda(_monedaParaTest1.Id));
    }
    
    [TestMethod]
    public void NoEncontrarMonedaDevuelveNull()
    {
        Assert.AreEqual(null, 
            _monedaLogicaParaTest.EncontrarMoneda(_monedaParaTest1.Id));
    }
    
    [TestMethod]
    public void ListarTodasLasMonedasCorrecto()
    {
        _monedaLogicaParaTest.AgregarMoneda(_monedaParaTest1);
        _monedaLogicaParaTest.AgregarMoneda(_monedaParaTest2);

        IList<Moneda> _listaMonedas = _monedaLogicaParaTest.ListarMonedas();
        
        Assert.IsTrue(_listaMonedas.Contains(_monedaParaTest1));
        Assert.IsTrue(_listaMonedas.Contains(_monedaParaTest2));
    }
}