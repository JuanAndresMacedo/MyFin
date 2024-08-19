using Dominio;
using Dominio.Constantes;
using Memoria;
using Memoria.BaseDeDatosRepositorios;
using MemoriaTest.Contexto;

namespace MemoriaTest;

[TestClass]

public class CategoriaBDRepositorioTest
{
    private CategoriaBDRepositorio _repositorioDeCategoriaParaTest;
    private SQLContexto _contexto;
    
    private Categoria _categoriaParaTest1;
    private Categoria _categoriaParaTest2;
    private Usuario _usuarioParaTest;
    private Espacio _espacioParaTest;

    [TestInitialize]
    public void Inicio()
    {
        FabricaSQLContexto fabricaSQLContext = new FabricaSQLContexto();
        _contexto = fabricaSQLContext.CrearContextoMemoria();
        
        _repositorioDeCategoriaParaTest = new CategoriaBDRepositorio(_contexto);

        _usuarioParaTest = new Usuario()
        {
            Id = 1,
            Correo = "hola@gmail.com",
        };

        _espacioParaTest = new Espacio()
        {
            Id = 1,
            Nombre = "Familia",
            Administrador = _usuarioParaTest,
        };

        _categoriaParaTest1 = new Categoria()
        {
            Nombre = "Ropa",
            UsuarioCreador = _usuarioParaTest,
            Espacio = _espacioParaTest,
            Estatus = ConstantesCategoria.estatusInactiva,
            Tipo = ConstantesCategoria.tipoCosto,
        };

        _categoriaParaTest2 = new Categoria()
        {
            Nombre = "Comida",
            UsuarioCreador = _usuarioParaTest,
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
            Id = 1,
            Nombre = _categoriaParaTest1.Nombre,
            UsuarioCreador = _usuarioParaTest,
            Espacio = _espacioParaTest,
            Estatus = ConstantesCategoria.estatusActiva,
            Tipo = ConstantesCategoria.tipoCosto
        };

        _repositorioDeCategoriaParaTest.Actualizar(_categoriaActualizada);
        
        Assert.AreEqual( ConstantesCategoria.estatusActiva, _categoriaParaTest1.Estatus);
    }
}