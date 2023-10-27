using Dominio;

namespace DominioTest;

[TestClass]
public class ObjetivoDeGastoTest
{
    private ObjetivoDeGasto _objetivoDeGastoParaTest1;
    private ObjetivoDeGasto _objetivoDeGastoParaTest2;
    private Categoria _categoriaParaTest;
    private Usuario _usuarioParaTest;
    
    private Espacio _espacioParaTest;

    [TestInitialize]
    public void Inicio()
    {
        _usuarioParaTest = new Usuario()
        {
            Correo = "hola@hotmail.com"
        };
        
        _espacioParaTest = new Espacio()
        {
            Nombre = "Hogar",
            Administrador = _usuarioParaTest
        };

        _categoriaParaTest = new Categoria()
        {
            Nombre = "Ropa",
            UsuarioCreador = _usuarioParaTest,
            Espacio = _espacioParaTest
        };

        _objetivoDeGastoParaTest1 = new ObjetivoDeGasto()
        {
            Titulo = "Comida",
            UsuarioCreador = _usuarioParaTest,
            Espacio = _espacioParaTest
        };
        
        _objetivoDeGastoParaTest2 = new ObjetivoDeGasto()
        {
            Titulo = "Comida",
            UsuarioCreador = _usuarioParaTest,
            Espacio = _espacioParaTest
        };
    }

    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void TituloVacioIncorrecto()
    {
        _objetivoDeGastoParaTest1.Titulo = "";
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void TituloNuloIncorrecto()
    {
       _objetivoDeGastoParaTest1.Titulo = null;
    }

    [TestMethod]
    public void TituloCorrectoOk()
    {
        _objetivoDeGastoParaTest1.Titulo = "Menos Noche";
        Assert.AreEqual("Menos Noche", _objetivoDeGastoParaTest1.Titulo);
    }

    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void MontoMaximoEsCeroIncorrecto()
    {
        _objetivoDeGastoParaTest1.MontoMaximo = 0;
    }

    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void MontoMaximoEsMenorACeroIncorrecto()
    {
        _objetivoDeGastoParaTest1.MontoMaximo = -30;
    }

    [TestMethod]
    public void MontoMaximoCorrectoOk()
    {
        _objetivoDeGastoParaTest1.MontoMaximo = 30;
        Assert.AreEqual(30, _objetivoDeGastoParaTest1.MontoMaximo);
    }

    [TestMethod]
    public void AgregarCategoriaOk()
    {
        _objetivoDeGastoParaTest1.AgregarCategoria(_categoriaParaTest);
        Assert.AreEqual(true, _objetivoDeGastoParaTest1.Categorias.Contains(_categoriaParaTest));
    }

    [TestMethod]
    public void SonObjetivosDeGastoDiferentesCorrecto()
    {
        _objetivoDeGastoParaTest1.Titulo = "Comida";
        _objetivoDeGastoParaTest1.UsuarioCreador = _usuarioParaTest;
        
        _objetivoDeGastoParaTest2.Titulo = "Deporte";
        _objetivoDeGastoParaTest2.UsuarioCreador = _usuarioParaTest;
        
        Assert.IsFalse(_objetivoDeGastoParaTest1.Equals(_objetivoDeGastoParaTest2));
    }
    
    [TestMethod]
    public void SonObjetivosDeGastoIgualesCorrecto()
    {
        Assert.IsTrue(_objetivoDeGastoParaTest1.Equals(_objetivoDeGastoParaTest2));
    }
    
    [TestMethod]
    public void AgregarUsuarioCorrecto()
    {
        _objetivoDeGastoParaTest1.UsuarioCreador = _usuarioParaTest;
        Assert.AreEqual(_usuarioParaTest, _objetivoDeGastoParaTest1.UsuarioCreador);
    }
    
    [TestMethod]
    public void AgregarEspacioCorrecto()
    {
        _objetivoDeGastoParaTest1.Espacio = _espacioParaTest;
        Assert.AreEqual(_espacioParaTest, _objetivoDeGastoParaTest1.Espacio);
    }
}