using Dominio;
using Logica;
using Memoria;

namespace LogicaTest;

[TestClass]
public class CategoriaLogicaTest
{
    private CategoriaLogica _categoriaLogicaParaTest;
    private IRepositorio<Categoria> _categoriaRepositorioParaTest;
    
    private TransaccionLogica _transaccionLogicaParaTest;
    private IRepositorio<Transaccion> _transaccionRepositorioParaTest;
    
    private Categoria _categoriaParaTest1;
    private Categoria _categoriaParaTest2;
    private Categoria _categoriaParaTest3;
    
    private Usuario _usuarioParaTest;
    
    private Espacio _espacioParaTest1;
    private Espacio _espacioParaTest2;

    private Transaccion _transaccionParaTest;
    
    private DateTime _fechaParaTest;
    private Monetaria _monetariaParaTest;
    private Espacio _espacioParaTest;

    [TestInitialize]
    public void Inicio()
    {
        _categoriaRepositorioParaTest = new CategoriaMemoriaRepositorio();
        _categoriaLogicaParaTest = new CategoriaLogica(_categoriaRepositorioParaTest);
        
        _transaccionRepositorioParaTest = new TransaccionMemoriaRepositorio();
        _transaccionLogicaParaTest = new TransaccionLogica(_transaccionRepositorioParaTest);

        _fechaParaTest = DateTime.Now;
        
        _usuarioParaTest = new Usuario()
        {
            Correo = "hola@hotmail.com"
        };

        _espacioParaTest1 = new Espacio()
        {
            Nombre = "Amigos",
            Administrador = _usuarioParaTest
        };
        
        _espacioParaTest2 = new Espacio()
        {
            Nombre = "Familia",
            Administrador = _usuarioParaTest
        };

        _categoriaParaTest1 = new Categoria()
        {
            UsuarioCreador = _usuarioParaTest,
            Nombre = "Comida",
            FechaDeCreacion = DateTime.Now,
            Tipo = "Costo",
            Estatus = "Inactiva",
            Espacio = _espacioParaTest1
        };
        
        _categoriaParaTest2 = new Categoria()
        {
            UsuarioCreador = _usuarioParaTest,
            Nombre = "Ropa",
            FechaDeCreacion = DateTime.Now,
            Tipo = "Costo",
            Estatus = "Activa",
            Espacio = _espacioParaTest1
        };
        
        _categoriaParaTest3 = new Categoria()
        {
            UsuarioCreador = _usuarioParaTest,
            Nombre = "Electrodomesticos",
            FechaDeCreacion = DateTime.Now,
            Tipo = "Ingreso",
            Estatus = "Activa",
            Espacio = _espacioParaTest2
        };
        
        _monetariaParaTest = new Monetaria()
        {
            Nombre = "Ahorros",
            FechaDeCreacion = _fechaParaTest,
            Moneda = "UYU",
            Monto = 23000000f,
            Propietario = _usuarioParaTest
        };
        
        _espacioParaTest = new Espacio()
        {
            Nombre = "Familia",
            Administrador = _usuarioParaTest,
        };
        
        _transaccionParaTest = new Transaccion()
        {
            Nombre = "Pago",
            Fecha = _fechaParaTest,
            Monto = 3431f,
            Moneda = "UYU",
            Categoria = _categoriaParaTest2,
            Cuenta = _monetariaParaTest,
            Espacio = _espacioParaTest,
            Id = 23,
            Tipo = "Costo"
        };

        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest);
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
        Categoria retornoCategoria1 = _categoriaLogicaParaTest.AgregarCategoria(_categoriaParaTest1);;
        Categoria retornoCategoria2 = _categoriaLogicaParaTest.AgregarCategoria(_categoriaParaTest2);;

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
            Nombre = _categoriaParaTest1.Nombre,
            UsuarioCreador = _usuarioParaTest,
            Espacio = _espacioParaTest1,
            Estatus = "Inactiva",
            Tipo = "Ingreso",
        };

        _categoriaLogicaParaTest.ActualizarCategoria(_categoriaActualizada);
        
        Assert.AreEqual("Ingreso", _categoriaParaTest1.Tipo);
        Assert.AreEqual("Inactiva", _categoriaParaTest1.Estatus);
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
            ListarCategoriasActivasDeUnEspacioPorTipo(_espacioParaTest1, "Costo");

        Assert.IsFalse(categoriasPrueba.Contains(retornoCategoria1));
        Assert.IsTrue(categoriasPrueba.Contains(retornoCategoria2));
    }
    
    [TestMethod]
    public void ValidarSiCategoriaTieneUnaTransacciónAsociadaCorrecto()
    {
        Assert.IsTrue(_categoriaLogicaParaTest.ValidarSiCategoriaTieneUnaTransacciónAsociada(
            _categoriaParaTest2, _espacioParaTest, _transaccionLogicaParaTest));
    }
}