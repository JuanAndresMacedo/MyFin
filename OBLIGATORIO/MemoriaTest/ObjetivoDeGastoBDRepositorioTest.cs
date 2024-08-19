using Dominio;
using Memoria;
using Memoria.BaseDeDatosRepositorios;
using MemoriaTest.Contexto;

namespace MemoriaTest;

[TestClass]
public class ObjetivoDeGastoBDRepositorioTest
{
    private ObjetivoDeGastoBDRepositorio _repositorioDeObjDeGastoParaTest;
    private SQLContexto _contexto;
    
    private ObjetivoDeGasto _objDeGastoParaTest1;
    private ObjetivoDeGasto _objDeGastoParaTest2;
    private Usuario _usuarioParaTest;
    private Espacio _espacioParaTest;

    [TestInitialize]
    public void Inicio()
    {
        FabricaSQLContexto fabricaSQLContext = new FabricaSQLContexto();
        _contexto = fabricaSQLContext.CrearContextoMemoria();
        
        _repositorioDeObjDeGastoParaTest = new ObjetivoDeGastoBDRepositorio(_contexto);

        _usuarioParaTest = new Usuario()
        {
            Nombre = "Frank"
        };

        _espacioParaTest = new Espacio()
        {
            Administrador = _usuarioParaTest
        };

        _objDeGastoParaTest1 = new ObjetivoDeGasto()
        {
            Titulo = "Comida",
            UsuarioCreador = _usuarioParaTest,
            Espacio = _espacioParaTest
        };

        _objDeGastoParaTest2 = new ObjetivoDeGasto()
        {
            Titulo = "Comida",
            UsuarioCreador = _usuarioParaTest,
            Espacio = _espacioParaTest
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
        _repositorioDeObjDeGastoParaTest.Agregar(_objDeGastoParaTest1);
        _repositorioDeObjDeGastoParaTest.Agregar(_objDeGastoParaTest2);
        
        Assert.IsTrue(_objDeGastoParaTest1.Id == 1);
        Assert.IsTrue(_objDeGastoParaTest2.Id == 2);
    }
    
    [TestMethod]
    public void AgregarUnObjetivoDeGastoAlRepositorioCorrecto()
    {
        _repositorioDeObjDeGastoParaTest.Agregar(_objDeGastoParaTest1);
        Assert.AreEqual(_objDeGastoParaTest1, _repositorioDeObjDeGastoParaTest.Encontrar(x => x.Id == _objDeGastoParaTest1.Id));
    }

    [TestMethod]
    public void AgregarUnObjDeGastoLoDevuelveCorrecto()
    {
        _repositorioDeObjDeGastoParaTest.Agregar(_objDeGastoParaTest1);
        Assert.AreEqual(_objDeGastoParaTest1,
            _repositorioDeObjDeGastoParaTest.Encontrar(x => x.Id == _objDeGastoParaTest1.Id));
    }

    [TestMethod]
    public void NoEstaAgregadoElObjetivoDeGastoCorrecto()
    {
        Assert.AreEqual(null, _repositorioDeObjDeGastoParaTest.Encontrar(x => x.Id == _objDeGastoParaTest1.Id));
    }

    [TestMethod]
    public void ListarTodosCorrecto()
    {
        _repositorioDeObjDeGastoParaTest.Agregar(_objDeGastoParaTest1);
        _repositorioDeObjDeGastoParaTest.Agregar(_objDeGastoParaTest2);
        Assert.IsTrue(_repositorioDeObjDeGastoParaTest.ListarTodos().Contains(_objDeGastoParaTest1)
                      && _repositorioDeObjDeGastoParaTest.ListarTodos().Contains(_objDeGastoParaTest2));
    }
    
    [TestMethod]
    public void ActualizarObjetivoDeGastoCorrecto()
    {
       _repositorioDeObjDeGastoParaTest.Agregar(_objDeGastoParaTest1);

        ObjetivoDeGasto _objetivoDeGastoActualizado = new ObjetivoDeGasto()
        {
            Titulo = "Comida",
            UsuarioCreador = _usuarioParaTest,
            Token = "2323-34234-23dfdf-343434",
            Espacio = _espacioParaTest,
            Id = 1,
        };

        _repositorioDeObjDeGastoParaTest.Actualizar(_objetivoDeGastoActualizado);

        Assert.AreEqual("2323-34234-23dfdf-343434", _objDeGastoParaTest1.Token);
    }
}