using Dominio;
using Memoria;
using Memoria.BaseDeDatosRepositorios;
using MemoriaTest.Contexto;

namespace MemoriaTest;

[TestClass]
public class EspacioBDRepositorioTest
{
    private EspacioBDRepositorio _repositorioParaTest;
    private SQLContexto _contexto;
    
    private Espacio _espacioParaTest;
    private Espacio _espacioParaTest1;
    private Usuario _usuarioParaTest;

    [TestInitialize]
    public void inicio()
    {
        FabricaSQLContexto fabricaSQLContext = new FabricaSQLContexto();
        _contexto = fabricaSQLContext.CrearContextoMemoria();
        
        _repositorioParaTest = new EspacioBDRepositorio(_contexto);
        
        _espacioParaTest = new Espacio();
        _espacioParaTest1 = new Espacio();
        
        _usuarioParaTest = new Usuario();
        
        _usuarioParaTest.Correo = ("hola@gmail.com");
        
        _espacioParaTest.Nombre = ("Familia");
        _espacioParaTest.Administrador = (_usuarioParaTest);
        
        _espacioParaTest1.Nombre = ("Amigos");
        _espacioParaTest1.Administrador = (_usuarioParaTest);
    }
    
    [TestCleanup]
    public void Fin()
    {
        _contexto.Database.EnsureDeleted();
    }
    
    [TestMethod]
    public void IdAgregadaCorrectamente()
    {
        _repositorioParaTest.Agregar(_espacioParaTest);
        _repositorioParaTest.Agregar(_espacioParaTest1);
 
        Assert.IsTrue(_espacioParaTest.Id == 1);
        Assert.IsTrue(_espacioParaTest1.Id == 2);
    }
    
    [TestMethod]
    public void AgregarUnEspacioAlRepositorioLoDevuelve()
    {
        Assert.AreEqual(_espacioParaTest, _repositorioParaTest.Agregar(_espacioParaTest));
    }
    
    [TestMethod]
    public void AgregarUnEspacioAlRepositorioCorrecto()
    {
        _repositorioParaTest.Agregar(_espacioParaTest);
        Assert.AreEqual(_espacioParaTest, _repositorioParaTest.Encontrar(x => x.Id == _espacioParaTest.Id));
    }

    [TestMethod]
    public void EspacioNoEstaAgregadoAlRepositorio()
    {
        Assert.AreEqual(null, _repositorioParaTest.Encontrar(x => x.Id == _espacioParaTest.Id));
    }
    
    [TestMethod]
    public void RepositorioDevuelveListaVacia()
    {
        Assert.IsTrue(_repositorioParaTest.ListarTodos().Count == 0);
    }

    [TestMethod]
    public void RepositorioDevuelveListaCompleta()
    {
        _repositorioParaTest.Agregar(_espacioParaTest);
        _repositorioParaTest.Agregar(_espacioParaTest1);
        
        Assert.IsTrue(_repositorioParaTest.ListarTodos().Contains(_espacioParaTest) && _repositorioParaTest.ListarTodos().Contains(_espacioParaTest1));
    }

    [TestMethod]
    public void EliminarUnEspacioQueNoExisteDevuelveNull()
    {
        Assert.AreEqual(null, _repositorioParaTest.Eliminar(_espacioParaTest.Id));
    }

    [TestMethod]
    public void EliminarUnEspacioCorrecto()
    {
        _repositorioParaTest.Agregar(_espacioParaTest);
        Assert.IsTrue(_espacioParaTest.Equals(_repositorioParaTest.Eliminar(_espacioParaTest.Id))
                      && _repositorioParaTest.Encontrar(x => x.Id == _espacioParaTest.Id) == null);
    }
    
    [TestMethod]
    public void ActualizarUnEspacioCorrecto()
    {
        _repositorioParaTest.Agregar(_espacioParaTest);
        _espacioParaTest1.Id = _espacioParaTest.Id;
        _repositorioParaTest.Actualizar(_espacioParaTest1);
        Assert.AreEqual(_espacioParaTest1.Nombre, _espacioParaTest.Nombre);
    }
}