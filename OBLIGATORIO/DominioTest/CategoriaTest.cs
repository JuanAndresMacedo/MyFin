using Dominio;
using Dominio.Constantes;

namespace DominioTest;

[TestClass]
public class CategoriaTest
{
    private Categoria _categoriaParaTest1;
    private Categoria _categoriaParaTest2;
    private Usuario _usuarioParaTest;
    private Espacio _espacioParaTest;
    private ObjetivoDeGasto _objetivoDeGastoParaTest1;

    private DateTime _fechaParaTest;

    [TestInitialize]
    public void Inicio()
    {
        _usuarioParaTest = new Usuario();

        _espacioParaTest = new Espacio()
        {
            Nombre = "Familia",
            Administrador = _usuarioParaTest
        };

        _fechaParaTest = DateTime.Now;

        _objetivoDeGastoParaTest1 = new ObjetivoDeGasto()
        {
            Id = 1,
            Titulo = "Comida",
            UsuarioCreador = _usuarioParaTest,
            Espacio = _espacioParaTest,
            MontoMaximo = 30,
        };
        
        _categoriaParaTest1 = new Categoria()
        {
            Id = 1,
            Estatus = ConstantesCategoria.estatusActiva,
            Tipo = ConstantesCategoria.tipoCosto,
            Nombre = "Comida",
            UsuarioCreador = _usuarioParaTest,
            Espacio = _espacioParaTest,
            FechaDeCreacion = _fechaParaTest
        };

        _categoriaParaTest2 = new Categoria()
        {
            Id = 2,
            Estatus = ConstantesCategoria.estatusActiva,
            Tipo = ConstantesCategoria.tipoCosto,
            Nombre = "Deporte",
            UsuarioCreador = _usuarioParaTest,
            Espacio = _espacioParaTest,
            FechaDeCreacion = _fechaParaTest
        };
    }

    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void EstatusVacioIncorrecto()
    {
        _categoriaParaTest1.Estatus = "";
    }

    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void EstatusNuloIncorrecto()
    {
        _categoriaParaTest1.Estatus = null;
    }

    [TestMethod]
    public void EstatusEsActivaCorrecto()
    {
        Assert.AreEqual(ConstantesCategoria.estatusActiva,
            _categoriaParaTest1.Estatus);
    }

    [TestMethod]
    public void EstatusEsInactivaCorrecto()
    {
        _categoriaParaTest1.Estatus = ConstantesCategoria.estatusInactiva;
        Assert.AreEqual(ConstantesCategoria.estatusInactiva, 
            _categoriaParaTest1.Estatus);
    }

    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void EstatusEsInactivaSinMayusculaIncorrecto()
    {
        _categoriaParaTest1.Estatus = "inactiva";
    }

    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void EstatusEsActivaSinMayusculaIncorrecto()
    {
        _categoriaParaTest1.Estatus = "activa";
    }

    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void EstatusEsOtraCosaIncorrecto()
    {
        _categoriaParaTest1.Estatus = "klksdkljds";
    }

    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void TipoVacioIncorrecto()
    {
        _categoriaParaTest1.Tipo = "";
    }

    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void TipoNuloIncorrecto()
    {
        _categoriaParaTest1.Tipo = null;
    }

    [TestMethod]
    public void TipoEsIngresoCorrecto()
    {
        _categoriaParaTest1.Tipo = ConstantesCategoria.tipoIngreso;
        Assert.AreEqual(ConstantesCategoria.tipoIngreso, _categoriaParaTest1.Tipo);
    }

    [TestMethod]
    public void TipoEsCostoCorrecto()
    {
        Assert.AreEqual(ConstantesCategoria.tipoCosto, _categoriaParaTest1.Tipo);
    }

    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void TipoEsOtraCosaIncorrecto()
    {
        _categoriaParaTest1.Tipo = "Gasto";
    }

    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void NombreVacioIncorrecto()
    {
        _categoriaParaTest1.Nombre = "";
    }

    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void NombreNuloIncorrecto()
    {
        _categoriaParaTest1.Nombre = null;
    }

    [TestMethod]
    public void NombreCorrecto()
    {
        Assert.AreEqual("Comida", _categoriaParaTest1.Nombre);
    }

    [TestMethod]
    public void FechaDeCreacionCorrecta()
    {
        Assert.AreEqual(_fechaParaTest, _categoriaParaTest1.FechaDeCreacion);
    }

    [TestMethod]
    public void SonCategoriasDiferentesCorrecto()
    {
        Assert.IsFalse(_categoriaParaTest1.Equals(_categoriaParaTest2));
    }

    [TestMethod]
    public void SonCategoriasIgualesCorrecto()
    {
        _categoriaParaTest2.Id = 1;
        Assert.IsTrue(_categoriaParaTest1.Equals(_categoriaParaTest2));
    }

    [TestMethod]
    public void UsuarioCorrecto()
    {
        Assert.IsTrue(_categoriaParaTest1.UsuarioCreador.Equals(_usuarioParaTest));
    }

    [TestMethod]
    public void AgregarEspacioCorrecto()
    {
        Assert.IsTrue(_categoriaParaTest1.Espacio.Equals(_espacioParaTest));
    }
    
    [TestMethod]
    public void AgregarObjetivosDeGastoCorrecto()
    {
        List<ObjetivoDeGasto> objetivoDeGasto = new List<ObjetivoDeGasto>();
        objetivoDeGasto.Add(_objetivoDeGastoParaTest1);
        _categoriaParaTest1.ObjetivosDeGasto = objetivoDeGasto;
        Assert.IsTrue(_categoriaParaTest1.ObjetivosDeGasto.Contains(_objetivoDeGastoParaTest1));
    }
}