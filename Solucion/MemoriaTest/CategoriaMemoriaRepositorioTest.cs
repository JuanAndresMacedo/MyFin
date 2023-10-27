using Dominio;
using Memoria;

namespace MemoriaTest;

[TestClass]

public class CategoriaMemoriaRepositorioTest
{
    private CategoriaMemoriaRepositorio _repositorioDeCategoriaParaTest;
    private Categoria _categoriaParaTest1;
    private Categoria _categoriaParaTest2;
    private Usuario _usuarioParaTest;
    private Espacio _espacioParaTest;

    [TestInitialize]
    public void Inicio()
    {
        _repositorioDeCategoriaParaTest = new CategoriaMemoriaRepositorio();

        _usuarioParaTest = new Usuario()
        {
            Correo = "hola@gmail.com",
        };

        _espacioParaTest = new Espacio()
        {
            Nombre = "Familia",
            Administrador = _usuarioParaTest,
        };

        _categoriaParaTest1 = new Categoria()
        {
            Nombre = "Ropa",
            UsuarioCreador = _usuarioParaTest,
            Espacio = _espacioParaTest,
            Estatus = "Inactiva",
            Tipo = "Costo",
        };

        _categoriaParaTest2 = new Categoria()
        {
            Nombre = "Comida",
            UsuarioCreador = _usuarioParaTest,
            Espacio = _espacioParaTest,
        };
    }
    
    [TestMethod]
    public void IdAgregadaCorrectamente()
    {
        _repositorioDeCategoriaParaTest.Agregar(_categoriaParaTest1);
        _repositorioDeCategoriaParaTest.Agregar(_categoriaParaTest2);

        Assert.IsTrue(_categoriaParaTest1.Id == 1);
        Assert.IsTrue(_categoriaParaTest2.Id == 2);

    }

    [TestMethod]
    public void AgregarUnaCategoriaAlRepositorioCorrecto()
    {
        _repositorioDeCategoriaParaTest.Agregar(_categoriaParaTest1);
        Assert.AreEqual(_categoriaParaTest1, _repositorioDeCategoriaParaTest.Encontrar(
            x => x.Id == _categoriaParaTest1.Id));
    }
    
    [TestMethod]
    public void AgregarUnaCategoriaAlRepositorioLaDevuelve()
    {
        Assert.AreEqual(_categoriaParaTest1, _repositorioDeCategoriaParaTest.Agregar(_categoriaParaTest1));
    }

    [TestMethod]
    public void NoEstaAgregadoLaCategoriaCorrecto()
    {
        Assert.AreEqual(null, _repositorioDeCategoriaParaTest.Encontrar(
            x => x.Id == _categoriaParaTest1.Id));
    }
    
    [TestMethod]
    public void ListarTodosCorrecto()
    {
        _repositorioDeCategoriaParaTest.Agregar(_categoriaParaTest1);
        _repositorioDeCategoriaParaTest.Agregar(_categoriaParaTest2);
        Assert.IsTrue(_repositorioDeCategoriaParaTest.ListarTodos().
                          Contains(_categoriaParaTest1) 
                      && _repositorioDeCategoriaParaTest.ListarTodos().
                          Contains(_categoriaParaTest2));
    }
    
    [TestMethod]
    public void EliminarUnaCategoriaQueNoExisteDevuelveNuloCorrecto()
    {
        Assert.AreEqual(null, _repositorioDeCategoriaParaTest.Eliminar(_categoriaParaTest1.Id));
    }
    
    [TestMethod]
    public void EliminarUnaCategoriaCorrecto()
    {
        _repositorioDeCategoriaParaTest.Agregar(_categoriaParaTest1);
        Assert.IsTrue(_categoriaParaTest1.Equals(_repositorioDeCategoriaParaTest.Eliminar(_categoriaParaTest1.Id))
                      && _repositorioDeCategoriaParaTest.Encontrar(x => x.Id == _categoriaParaTest1.Id) == null);
    }
    
    [TestMethod]
    public void ActualizarCategoriaCorrecto()
    {
        _repositorioDeCategoriaParaTest.Agregar(_categoriaParaTest1);

        Categoria _categoriaActualizada = new Categoria()
        {
            Nombre = _categoriaParaTest1.Nombre,
            UsuarioCreador = _usuarioParaTest,
            Espacio = _espacioParaTest,
            Estatus = "Activa",
            Tipo = "Costo"
        };

        _repositorioDeCategoriaParaTest.Actualizar(_categoriaActualizada);
        
        Assert.AreEqual( "Activa", _categoriaParaTest1.Estatus);
    }
}