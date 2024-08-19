using Dominio;
using Dominio.Constantes;
using Logica;
using LogicaTest.Contexto;
using Memoria;
using Memoria.BaseDeDatosRepositorios;

namespace LogicaTest;

[TestClass]
public class CategoriaLogicaTest
{
    private SQLContexto _contexto;

    private CategoriaBDRepositorio _categoriaRepositorioParaTest;
    private CategoriaLogica _categoriaLogicaParaTest;

    private TransaccionBDRepositorio _transaccionRepositorioParaTest;
    private TransaccionLogica _transaccionLogicaParaTest;

    private Categoria _categoriaParaTest1;
    private Categoria _categoriaParaTest2;
    private Categoria _categoriaParaTest3;

    private Usuario _usuarioParaTest;

    private Espacio _espacioParaTest1;
    private Espacio _espacioParaTest2;
    private Espacio _espacioParaTest3;

    private Transaccion _transaccionParaTest;

    private DateTime _fechaParaTest;
    private Monetaria _monetariaParaTest;

    private Moneda _monedaParaTest1;

    [TestInitialize]
    public void Inicio()
    {
        FabricaSQLContexto fabricaSQLContext = new FabricaSQLContexto();
        _contexto = fabricaSQLContext.CrearContextoMemoria();

        _categoriaRepositorioParaTest = new CategoriaBDRepositorio(_contexto);
        _categoriaLogicaParaTest = new CategoriaLogica(_categoriaRepositorioParaTest);

        _transaccionRepositorioParaTest = new TransaccionBDRepositorio(_contexto);
        _transaccionLogicaParaTest = new TransaccionLogica(_transaccionRepositorioParaTest);

        _fechaParaTest = DateTime.Now;

        _usuarioParaTest = new Usuario()
        {
            Id = 1,
            Correo = "hola@hotmail.com"
        };

        _espacioParaTest1 = new Espacio()
        {
            Id = 1,
            Nombre = "Amigos",
            Administrador = _usuarioParaTest
        };

        _espacioParaTest2 = new Espacio()
        {
            Id = 2,
            Nombre = "Familia",
            Administrador = _usuarioParaTest
        };

        _espacioParaTest3 = new Espacio()
        {
            Id = 3,
            Nombre = "Familia",
            Administrador = _usuarioParaTest,
        };

        _categoriaParaTest1 = new Categoria()
        {
            UsuarioCreador = _usuarioParaTest,
            Nombre = "Comida",
            FechaDeCreacion = DateTime.Now,
            Tipo = ConstantesCategoria.tipoCosto,
            Estatus = ConstantesCategoria.estatusInactiva,
            Espacio = _espacioParaTest1
        };

        _categoriaParaTest2 = new Categoria()
        {
            UsuarioCreador = _usuarioParaTest,
            Nombre = "Ropa",
            FechaDeCreacion = DateTime.Now,
            Tipo = ConstantesCategoria.tipoCosto,
            Estatus = ConstantesCategoria.estatusActiva,
            Espacio = _espacioParaTest1
        };

        _categoriaParaTest3 = new Categoria()
        {
            UsuarioCreador = _usuarioParaTest,
            Nombre = "Electrodomesticos",
            FechaDeCreacion = DateTime.Now,
            Tipo = ConstantesCategoria.tipoIngreso,
            Estatus = ConstantesCategoria.estatusActiva,
            Espacio = _espacioParaTest2
        };

        _monedaParaTest1 = new Moneda()
        {
            Id = 1,
            Nombre = "UYU"
        };

        _monetariaParaTest = new Monetaria()
        {
            Id = 1,
            Nombre = "Ahorros",
            FechaDeCreacion = _fechaParaTest,
            Moneda = _monedaParaTest1,
            Monto = 23000000f,
            Propietario = _usuarioParaTest
        };

        _transaccionParaTest = new Transaccion()
        {
            Nombre = "Pago",
            Fecha = _fechaParaTest,
            Monto = 3431f,
            Moneda = _monedaParaTest1,
            Categoria = _categoriaParaTest2,
            Cuenta = _monetariaParaTest,
            Espacio = _espacioParaTest3,
            Tipo = ConstantesCategoria.tipoCosto
        };
    }

    [TestCleanup]
    public void Fin()
    {
        _contexto.Database.EnsureDeleted();
    }

    [TestMethod]
    public void AgregarCategoriaCorrecto()
    {
        Categoria agregada = _categoriaLogicaParaTest.AgregarCategoria(_categoriaParaTest1);

        Assert.AreEqual(_categoriaParaTest1.UsuarioCreador, agregada.UsuarioCreador);
        Assert.AreEqual(_categoriaParaTest1.Nombre, agregada.Nombre);
        Assert.AreEqual(_categoriaParaTest1.FechaDeCreacion, agregada.FechaDeCreacion);
        Assert.AreEqual(_categoriaParaTest1.Tipo, agregada.Tipo);
        Assert.AreEqual(_categoriaParaTest1.Estatus, agregada.Estatus);
    }

    [TestMethod]
    [ExpectedException(typeof(LogicaExcepcion))]
    public void AgregarDosCategoriasIgualesIncorrecto()
    {
        _categoriaParaTest2.Nombre = "Comida";
        _categoriaLogicaParaTest.AgregarCategoria(_categoriaParaTest1);
        _categoriaLogicaParaTest.AgregarCategoria(_categoriaParaTest2);
    }

    [TestMethod]
    public void AgregarDosCategoriasCorrecto()
    {
        _categoriaLogicaParaTest.AgregarCategoria(_categoriaParaTest1);
        _categoriaLogicaParaTest.AgregarCategoria(_categoriaParaTest2);
    }

    [TestMethod]
    public void ListarTodasLasCategoriasCorrecto()
    {
        Categoria retornoCategoria1 = _categoriaLogicaParaTest.AgregarCategoria(_categoriaParaTest1);
        ;
        Categoria retornoCategoria2 = _categoriaLogicaParaTest.AgregarCategoria(_categoriaParaTest2);
        ;

        IList<Categoria> categoriasPrueba = _categoriaLogicaParaTest.ListarCategorias();

        Assert.IsTrue(categoriasPrueba.Contains(retornoCategoria1));
        Assert.IsTrue(categoriasPrueba.Contains(retornoCategoria2));
    }

    [TestMethod]
    public void EncontrarCategoriaCorrecto()
    {
        Categoria agregada = _categoriaLogicaParaTest.AgregarCategoria(_categoriaParaTest1);
        Assert.AreEqual(agregada, _categoriaLogicaParaTest
            .EncontrarCategoria(_categoriaParaTest1.Id));
    }

    [TestMethod]
    public void EncontrarCategoriaNoEstaDevuelveNuloCorrecto()
    {
        Assert.AreEqual(null,
            _categoriaLogicaParaTest.EncontrarCategoria(_categoriaParaTest1.Id));
    }

    [TestMethod]
    public void EliminarCategoriaInexistenteDevuelveNull()
    {
        Assert.AreEqual(null, _categoriaLogicaParaTest.EliminarCategoria(_categoriaParaTest1.Id));
    }

    [TestMethod]
    public void EliminarCategoriaCorrecta()
    {
        _categoriaLogicaParaTest.AgregarCategoria(_categoriaParaTest1);
        int largo = _categoriaLogicaParaTest.ListarCategorias().Count;
        Assert.AreEqual(_categoriaParaTest1, _categoriaLogicaParaTest.EliminarCategoria(_categoriaParaTest1.Id));
        Assert.IsTrue(_categoriaLogicaParaTest.ListarCategorias().Count == largo - 1);
    }

    [TestMethod]
    public void ActualizarCategoriaCorrecto()
    {
        _categoriaLogicaParaTest.AgregarCategoria(_categoriaParaTest1);

        Categoria _categoriaActualizada = new Categoria()
        {
            Id = _categoriaParaTest1.Id,
            Nombre = _categoriaParaTest1.Nombre,
            UsuarioCreador = _usuarioParaTest,
            Espacio = _espacioParaTest1,
            Estatus = ConstantesCategoria.estatusInactiva,
            Tipo = ConstantesCategoria.tipoIngreso,
        };

        _categoriaLogicaParaTest.ActualizarCategoria(_categoriaActualizada);

        Assert.AreEqual(ConstantesCategoria.tipoIngreso, _categoriaParaTest1.Tipo);
        Assert.AreEqual(ConstantesCategoria.estatusInactiva,
            _categoriaParaTest1.Estatus);
    }

    [TestMethod]
    public void ListarCategoriasDeUnEspacioCorrecto()
    {
        Categoria retornoCategoria1 = _categoriaLogicaParaTest.AgregarCategoria(_categoriaParaTest1);
        Categoria retornoCategoria2 = _categoriaLogicaParaTest.AgregarCategoria(_categoriaParaTest2);
        Categoria retornoCategoria3 = _categoriaLogicaParaTest.AgregarCategoria(_categoriaParaTest3);

        List<Categoria> categoriasPrueba = _categoriaLogicaParaTest.ListarCategoriasDeUnEspacio(_espacioParaTest1);

        Assert.IsTrue(categoriasPrueba.Contains(retornoCategoria1));
        Assert.IsTrue(categoriasPrueba.Contains(retornoCategoria2));
        Assert.IsFalse(categoriasPrueba.Contains(retornoCategoria3));
    }

    [TestMethod]
    public void ListarCategoriasActivasDeUnEspacioPorTipoCorrecto()
    {
        Categoria retornoCategoria1 = _categoriaLogicaParaTest.AgregarCategoria(_categoriaParaTest1);
        Categoria retornoCategoria2 = _categoriaLogicaParaTest.AgregarCategoria(_categoriaParaTest2);

        List<Categoria> categoriasPrueba = _categoriaLogicaParaTest.
            ListarCategoriasActivasDeUnEspacioPorTipo(_espacioParaTest1, 
                ConstantesCategoria.tipoCosto);

        Assert.IsFalse(categoriasPrueba.Contains(retornoCategoria1));
        Assert.IsTrue(categoriasPrueba.Contains(retornoCategoria2));
    }

    [TestMethod]
    public void ValidarSiCategoriaTieneUnaTransacciónAsociadaCorrecto()
    {
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest);
        Assert.IsTrue(_categoriaLogicaParaTest.ValidarSiCategoriaTieneUnaTransacciónAsociada(
            _categoriaParaTest2, _espacioParaTest3, _transaccionLogicaParaTest));
    }
}