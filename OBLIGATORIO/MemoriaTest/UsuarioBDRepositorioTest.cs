using Dominio;
using Memoria;
using Memoria.BaseDeDatosRepositorios;
using MemoriaTest.Contexto;

namespace MemoriaTest;

[TestClass]
public class UsuarioBDRepositorioTest
{
    private UsuarioBDRepositorio _repositorioDeUsuarioParaTest;
    private SQLContexto _contexto;

    private Usuario _usuarioParaTest1;
    private Usuario _usuarioParaTest2;
    private Usuario _usuarioParaTest3;
    private Usuario _usuarioAgregadoAlRepoParaTest;

    [TestInitialize]
    public void Inicio()
    {
        FabricaSQLContexto fabricaSQLContext = new FabricaSQLContexto();
        _contexto = fabricaSQLContext.CrearContextoMemoria();

        _repositorioDeUsuarioParaTest = new UsuarioBDRepositorio(_contexto);

        _usuarioParaTest1 = new Usuario()
        {
            Nombre = "Pep",
            Apellido = "Pepn",
            Correo = "pepebien@gmail.com",
            Contrasena = "ContraMALL1234",
        };

        _usuarioParaTest2 = new Usuario()
        {
            Nombre = "Pepebien",
            Apellido = "Pepebien",
            Correo = "pepebien@gmail.com",
            Contrasena = "Contrasenabien1234",
            Direccion = "Direccionbien"
        };

        _usuarioParaTest3 = new Usuario()
        {
            Nombre = "Pablo",
            Apellido = "Perez",
            Correo = "pablo@gmail.com",
            Contrasena = "Contrasenabien1234",
            Direccion = "Direccionbien"
        };

        _usuarioAgregadoAlRepoParaTest = new Usuario();
    }

    [TestCleanup]
    public void Fin()
    {
        _contexto.Database.EnsureDeleted();
    }

    [TestMethod]
    public void IdAgregadaCorrectamente()
    {
        _repositorioDeUsuarioParaTest.Agregar(_usuarioParaTest1);
        _repositorioDeUsuarioParaTest.Agregar(_usuarioParaTest2);
        _repositorioDeUsuarioParaTest.Agregar(_usuarioParaTest3);

        Assert.IsTrue(_usuarioParaTest1.Id == 1);
        Assert.IsTrue(_usuarioParaTest2.Id == 2);
        Assert.IsTrue(_usuarioParaTest3.Id == 3);
    }

    [TestMethod]
    public void AgregarUnUsuarioAlRepositorioCorrecto()
    {
        _repositorioDeUsuarioParaTest.Agregar(_usuarioParaTest1);
        Assert.AreEqual(_usuarioParaTest1, _repositorioDeUsuarioParaTest.Encontrar(
            x => x.Id == _usuarioParaTest1.Id));
    }

    [TestMethod]
    public void AgregarUnUsuarioLoDevuelveCorrecto()
    {
        _usuarioAgregadoAlRepoParaTest = _repositorioDeUsuarioParaTest.Agregar(_usuarioParaTest1);
        Assert.AreEqual(_usuarioAgregadoAlRepoParaTest, _usuarioParaTest1);
    }

    [TestMethod]
    public void NoEstaAgregadoElUsuarioCorrecto()
    {
        Assert.AreEqual(null, _repositorioDeUsuarioParaTest.Encontrar(
            x => x.Id == _usuarioParaTest1.Id));
    }

    [TestMethod]
    public void ListarTodosCorrecto()
    {
        _repositorioDeUsuarioParaTest.Agregar(_usuarioParaTest1);
        _repositorioDeUsuarioParaTest.Agregar(_usuarioParaTest2);
        _repositorioDeUsuarioParaTest.Agregar(_usuarioParaTest3);
        Assert.IsTrue(_repositorioDeUsuarioParaTest.ListarTodos().Contains(_usuarioParaTest1)
                      &&
                      _repositorioDeUsuarioParaTest.ListarTodos().Contains(_usuarioParaTest2)
                      &&
                      _repositorioDeUsuarioParaTest.ListarTodos().Contains(_usuarioParaTest3));
    }

    [TestMethod]
    public void EliminarUnUsuarioQueNoExisteDevuelveNull()
    {
        Assert.AreEqual(null, _repositorioDeUsuarioParaTest.Eliminar(_usuarioParaTest1.Id));
    }

    [TestMethod]
    public void EliminarUnUsuarioCorrecto()
    {
        _repositorioDeUsuarioParaTest.Agregar(_usuarioParaTest1);
        Assert.IsTrue(_usuarioParaTest1.Equals(_repositorioDeUsuarioParaTest.Eliminar(_usuarioParaTest1.Id))
                      && _repositorioDeUsuarioParaTest.Encontrar(x => x.Id == _usuarioParaTest1.Id) == null);
    }

    [TestMethod]
    public void ActualizarUnUsuarioCorrecto()
    {
        _repositorioDeUsuarioParaTest.Agregar(_usuarioParaTest1);
        _usuarioParaTest2.Id = _usuarioParaTest1.Id;
        _repositorioDeUsuarioParaTest.Actualizar(_usuarioParaTest2);

        Assert.AreEqual(_usuarioParaTest2.Nombre, _usuarioParaTest1.Nombre);
        Assert.AreEqual(_usuarioParaTest2.Apellido, _usuarioParaTest1.Apellido);
        Assert.AreEqual(_usuarioParaTest2.Correo, _usuarioParaTest1.Correo);
        Assert.AreEqual(_usuarioParaTest2.Contrasena, _usuarioParaTest1.Contrasena);
        Assert.AreEqual(_usuarioParaTest2.Direccion, _usuarioParaTest1.Direccion);
    }
}