using Dominio;
using Memoria;

namespace MemoriaTest;

[TestClass]
public class ObjetivoDeGastoMemoriaRepositorioTest
{
    private ObjetivoDeGastoMemoriaRepositorio _repositorioDeObjDeGastoParaTest;
    private ObjetivoDeGasto _objDeGastoParaTest1;
    private ObjetivoDeGasto _objDeGastoParaTest2;
    private Usuario _usuarioParaTest;
    private Espacio _espacioParaTest;

    [TestInitialize]
    public void Inicio()
    {
        _repositorioDeObjDeGastoParaTest = new ObjetivoDeGastoMemoriaRepositorio();

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

    [TestMethod]
    public void IdAgregadaCorrectamente()
    {
        _repositorioDeObjDeGastoParaTest.Agregar(_objDeGastoParaTest1);
        _repositorioDeObjDeGastoParaTest.Agregar(_objDeGastoParaTest2);
        
        Assert.IsTrue(_objDeGastoParaTest1.Id == 1);
        Assert.IsTrue(_objDeGastoParaTest1.Id == 2);
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
    public void EliminarUnObjDeGastoQueNoExisteDevuelveNull()
    {
        Assert.AreEqual(null, _repositorioDeObjDeGastoParaTest.Eliminar(_objDeGastoParaTest1.Id));
    }

    [TestMethod]
    public void EliminarUnObjDeGastoCorrecto()
    {
        _repositorioDeObjDeGastoParaTest.Agregar(_objDeGastoParaTest1);
        Assert.IsTrue(_objDeGastoParaTest1.Equals(_repositorioDeObjDeGastoParaTest.Eliminar(_objDeGastoParaTest1.Id))
                      && _repositorioDeObjDeGastoParaTest.Encontrar(x => x.Id == _objDeGastoParaTest1.Id) == null);
    }

    [TestMethod]
    [ExpectedException(typeof(NotImplementedException))]
    public void ObjetivoDeGastoNoSePuedeEditarCorrecto()
    {
        _repositorioDeObjDeGastoParaTest.Actualizar(_objDeGastoParaTest1);
    }
}