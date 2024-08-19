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
            Id = 1,
            Correo = "hola@hotmail.com"
        };
        
        _espacioParaTest = new Espacio()
        {
            Id = 1,
            Nombre = "Hogar",
            Administrador = _usuarioParaTest
        };

        _categoriaParaTest = new Categoria()
        {
            Id = 1,
            Nombre = "Ropa",
            UsuarioCreador = _usuarioParaTest,
            Espacio = _espacioParaTest
        };

        _objetivoDeGastoParaTest1 = new ObjetivoDeGasto()
        {
            Id = 1,
            Titulo = "Comida",
            UsuarioCreador = _usuarioParaTest,
            Espacio = _espacioParaTest,
            MontoMaximo = 30,
        };
        
        _objetivoDeGastoParaTest2 = new ObjetivoDeGasto()
        {
            Id = 2,
            Titulo = "Comida",
            UsuarioCreador = _usuarioParaTest,
            Espacio = _espacioParaTest
        };
        
        _objetivoDeGastoParaTest1.AgregarCategoria(_categoriaParaTest);
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
        Assert.AreEqual("Comida", _objetivoDeGastoParaTest1.Titulo);
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
        Assert.AreEqual(30, _objetivoDeGastoParaTest1.MontoMaximo);
    }

    [TestMethod]
    public void AgregarCategoriaOk()
    {
        Assert.AreEqual(true, _objetivoDeGastoParaTest1.Categorias.Contains(_categoriaParaTest));
    }

    [TestMethod]
    public void SonObjetivosDeGastoDiferentesCorrecto()
    {
        Assert.IsFalse(_objetivoDeGastoParaTest1.Equals(_objetivoDeGastoParaTest2));
    }
    
    [TestMethod]
    public void SonObjetivosDeGastoIgualesCorrecto()
    {
        _objetivoDeGastoParaTest2.Id = 1;
        Assert.IsTrue(_objetivoDeGastoParaTest1.Equals(_objetivoDeGastoParaTest2));
    }
    
    [TestMethod]
    public void AgregarUsuarioCorrecto()
    {
        Assert.AreEqual(_usuarioParaTest, _objetivoDeGastoParaTest1.UsuarioCreador);
    }
    
    [TestMethod]
    public void AgregarEspacioCorrecto()
    {
        Assert.AreEqual(_espacioParaTest, _objetivoDeGastoParaTest1.Espacio);
    }
}