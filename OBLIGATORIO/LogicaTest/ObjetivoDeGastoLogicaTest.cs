using Dominio;
using Logica;
using Memoria;

namespace LogicaTest;

[TestClass]
public class ObjetivoDeGastoLogicaTest
{
    private ObjetivoDeGastoLogica _objetivoDeGastoLogicaParaTest;
    private IRepositorio<ObjetivoDeGasto> _objetivoDeGastoRepositorioParaTest;
    
    private ObjetivoDeGasto _objetivoDeGastoParaTest1;
    private ObjetivoDeGasto _objetivoDeGastoParaTest2;
    private ObjetivoDeGasto _objetivoDeGastoParaTest3;
    
    private Usuario _usuarioParaTest;
    
    private Espacio _espacioParaTest1;
    private Espacio _espacioParaTest2;
    
    private Categoria _categoriaParaTest1;

    [TestInitialize]
    public void Inicio()
    {
        _objetivoDeGastoRepositorioParaTest = new ObjetivoDeGastoMemoriaRepositorio();
        _objetivoDeGastoLogicaParaTest = new ObjetivoDeGastoLogica(_objetivoDeGastoRepositorioParaTest);
        
        _usuarioParaTest = new Usuario()
        {
            Correo = "juan@adinet.com",
            Contrasena = "juanBall12345",
            Nombre = "Juan",
            Apellido = "Min",
            Direccion = "Av.China"
        };

        _espacioParaTest1 = new Espacio()
        {
            Nombre = "Ropa",
            Administrador = _usuarioParaTest
        };
        
        _espacioParaTest2 = new Espacio()
        {
            Nombre = "Familia",
            Administrador = _usuarioParaTest
        };

        _categoriaParaTest1 = new Categoria()
        {
            Nombre = "Ropa",
            UsuarioCreador = _usuarioParaTest,
            Espacio = _espacioParaTest1,
            Estatus = "Inactiva",
            Tipo = "Costo",
        };
        
        _objetivoDeGastoParaTest1 = new ObjetivoDeGasto()
        {
            Titulo = "Comida",
            MontoMaximo = 30000,
            UsuarioCreador = _usuarioParaTest,
            Espacio = _espacioParaTest1,
        };

        _objetivoDeGastoParaTest2 = new ObjetivoDeGasto()
        {
            Titulo = "Deporte",
            MontoMaximo = 30000,
            UsuarioCreador = _usuarioParaTest,
            Espacio = _espacioParaTest1
        };
        
        _objetivoDeGastoParaTest3 = new ObjetivoDeGasto()
        {
            Titulo = "Deporte",
            MontoMaximo = 30000,
            UsuarioCreador = _usuarioParaTest,
            Espacio = _espacioParaTest2
        };
        
        _objetivoDeGastoParaTest1.AgregarCategoria(_categoriaParaTest1);
        _objetivoDeGastoParaTest2.AgregarCategoria(_categoriaParaTest1);
        _objetivoDeGastoParaTest3.AgregarCategoria(_categoriaParaTest1);
    }

    [TestMethod]
    public void AgregarObjetivoDeGastoCorrecto()
    {
        ObjetivoDeGasto agregado = _objetivoDeGastoLogicaParaTest.AgregarObjetivoDeGasto
            (_objetivoDeGastoParaTest1);

        Assert.AreEqual(_objetivoDeGastoParaTest1.Titulo, agregado.Titulo);
        Assert.AreEqual(_objetivoDeGastoParaTest1.MontoMaximo, agregado.MontoMaximo);
        Assert.AreEqual(_objetivoDeGastoParaTest1.Categorias, agregado.Categorias);
        Assert.AreEqual(_objetivoDeGastoParaTest1.UsuarioCreador, agregado.UsuarioCreador);
    }

    [TestMethod]
    [ExpectedException(typeof(LogicaExcepcion))]
    public void AgregarDosObjetivosDeGastoIgualesIncorrecto()
    {
        _objetivoDeGastoParaTest2.Titulo = "Comida";
        _objetivoDeGastoLogicaParaTest.AgregarObjetivoDeGasto(_objetivoDeGastoParaTest1);
        _objetivoDeGastoLogicaParaTest.AgregarObjetivoDeGasto(_objetivoDeGastoParaTest2);
    }

    [TestMethod]
    public void AgregarDosObjetivosDeGastoCorrecto()
    {
        _objetivoDeGastoLogicaParaTest.AgregarObjetivoDeGasto(_objetivoDeGastoParaTest1);
        _objetivoDeGastoLogicaParaTest.AgregarObjetivoDeGasto(_objetivoDeGastoParaTest2);
    }

    [TestMethod]
    public void ListarTodosLosObjetivosDeGastoCorrecto()
    {
        ObjetivoDeGasto retornoObjetivoDeGasto1 =
            _objetivoDeGastoLogicaParaTest.AgregarObjetivoDeGasto(_objetivoDeGastoParaTest1);
        ;
        ObjetivoDeGasto retornoObjetivoDeGasto2 =
            _objetivoDeGastoLogicaParaTest.AgregarObjetivoDeGasto(_objetivoDeGastoParaTest2);
        ;

        IList<ObjetivoDeGasto> objetivosDeGastoPrueba = _objetivoDeGastoLogicaParaTest.ListarObjetivosDeGasto();

        Assert.IsTrue(objetivosDeGastoPrueba.Contains(retornoObjetivoDeGasto1));
        Assert.IsTrue(objetivosDeGastoPrueba.Contains(retornoObjetivoDeGasto2));
    }

    [TestMethod]
    public void EncontrarObjetivoDeGastoCorrecto()
    {
        ObjetivoDeGasto agregado = _objetivoDeGastoLogicaParaTest.AgregarObjetivoDeGasto(_objetivoDeGastoParaTest1);
        Assert.AreEqual(agregado, _objetivoDeGastoLogicaParaTest
            .EncontrarObjetivoDeGasto(_objetivoDeGastoParaTest1.Id));
    }

    [TestMethod]
    public void EncontrarObjetivoDeGastoNoEstaDevuelveNuloCorrecto()
    {
        Assert.AreEqual(null,
            _objetivoDeGastoLogicaParaTest.EncontrarObjetivoDeGasto(_objetivoDeGastoParaTest1.Id));
    }

    [TestMethod]
    public void EliminarObjetivoDeGastoNoExistenteDevuelveNull()
    {
        Assert.AreEqual(null, _objetivoDeGastoLogicaParaTest.EliminarObjetivoDeGasto(_objetivoDeGastoParaTest1.Id));
    }

    [TestMethod]
    public void EliminarObjetivoDeGastoCorrecto()
    {
        _objetivoDeGastoLogicaParaTest.AgregarObjetivoDeGasto(_objetivoDeGastoParaTest1);
        int largo = _objetivoDeGastoLogicaParaTest.ListarObjetivosDeGasto().Count;
        
        Assert.AreEqual(_objetivoDeGastoParaTest1,
            _objetivoDeGastoLogicaParaTest.EliminarObjetivoDeGasto(_objetivoDeGastoParaTest1.Id));
        Assert.IsTrue(_objetivoDeGastoLogicaParaTest.ListarObjetivosDeGasto().Count == largo - 1);
    }
    
    [TestMethod]
    public void ListarCategoriasDeUnEspacioCorrecto()
    {
        ObjetivoDeGasto retornoObjetivoDeGasto1 = 
            _objetivoDeGastoLogicaParaTest.AgregarObjetivoDeGasto(_objetivoDeGastoParaTest1);
        ObjetivoDeGasto retornoObjetivoDeGasto2 = 
            _objetivoDeGastoLogicaParaTest.AgregarObjetivoDeGasto(_objetivoDeGastoParaTest2);
        ObjetivoDeGasto retornoObjetivoDeGasto3 = 
            _objetivoDeGastoLogicaParaTest.AgregarObjetivoDeGasto(_objetivoDeGastoParaTest3);

        List<ObjetivoDeGasto> categoriasPrueba = 
            _objetivoDeGastoLogicaParaTest.ListarObjetivosDeGastosDeUnEspacio(_espacioParaTest1);

        Assert.IsTrue(categoriasPrueba.Contains(retornoObjetivoDeGasto1));
        Assert.IsTrue(categoriasPrueba.Contains(retornoObjetivoDeGasto2));
        Assert.IsFalse(categoriasPrueba.Contains(retornoObjetivoDeGasto3));     
    }
    
    [TestMethod]
    [ExpectedException(typeof(LogicaExcepcion))]
    public void ObjetivoDeGastoSinCategoriasIncorrecto()
    {
        _objetivoDeGastoParaTest1.Categorias = new List<Categoria>();
        ObjetivoDeGasto retornoCategoria1 = 
            _objetivoDeGastoLogicaParaTest.AgregarObjetivoDeGasto(_objetivoDeGastoParaTest1);
    }
}