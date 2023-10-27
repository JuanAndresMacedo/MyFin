using Dominio;

namespace DominioTest;

[TestClass]
public class CategoriaTest
{
    private Categoria _categoriaParaTest1;
    private Categoria _categoriaParaTest2;
    private Usuario _usuarioParaTest;
    private Espacio _espacioParaTest;

    private DateTime _fechaParaTest;

    [TestInitialize]
    public void Inicio()
    {
        _categoriaParaTest1 = new Categoria();
        _categoriaParaTest2 = new Categoria();

        _usuarioParaTest = new Usuario();

        _espacioParaTest = new Espacio()
        {
            Nombre = "Familia",
            Administrador = _usuarioParaTest
        };

        _fechaParaTest = DateTime.Now;
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
        _categoriaParaTest1.Estatus = "Activa";
        Assert.AreEqual("Activa", _categoriaParaTest1.Estatus);
    }

    [TestMethod]
    public void EstatusEsInactivaCorrecto()
    {
        _categoriaParaTest1.Estatus = "Inactiva";
        Assert.AreEqual("Inactiva", _categoriaParaTest1.Estatus);
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
        _categoriaParaTest1.Tipo = "Ingreso";
        Assert.AreEqual("Ingreso", _categoriaParaTest1.Tipo);
    }

    [TestMethod]
    public void TipoEsCostoCorrecto()
    {
        _categoriaParaTest1.Tipo = "Costo";
        Assert.AreEqual("Costo", _categoriaParaTest1.Tipo);
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
        _categoriaParaTest1.Nombre = "Juan";
        Assert.AreEqual("Juan", _categoriaParaTest1.Nombre);
    }

    [TestMethod]
    public void FechaDeCreacionCorrecta()
    {
        _categoriaParaTest1.FechaDeCreacion = _fechaParaTest;
        Assert.AreEqual(_fechaParaTest, _categoriaParaTest1.FechaDeCreacion);
    }

    [TestMethod]
    public void SonCategoriasDiferentesCorrecto()
    {
        _categoriaParaTest1.Nombre = "Comida";
        _categoriaParaTest1.UsuarioCreador = _usuarioParaTest;
        _categoriaParaTest1.Espacio = _espacioParaTest;
        
        _categoriaParaTest2.Nombre = "Deporte";
        _categoriaParaTest2.UsuarioCreador = _usuarioParaTest;
        _categoriaParaTest2.Espacio = _espacioParaTest;
        
        Assert.IsFalse(_categoriaParaTest1.Equals(_categoriaParaTest2));
    }
    
    [TestMethod]
    public void SonCategoriasIgualesCorrecto()
    {
        _categoriaParaTest1.Nombre = "Deporte";
        _categoriaParaTest1.UsuarioCreador = _usuarioParaTest;
        _categoriaParaTest1.Espacio = _espacioParaTest;
        
        _categoriaParaTest2.Nombre = "Deporte";
        _categoriaParaTest2.UsuarioCreador = _usuarioParaTest;
        _categoriaParaTest2.Espacio = _espacioParaTest;
        
        Assert.IsTrue(_categoriaParaTest1.Equals(_categoriaParaTest2));
    }
    
    [TestMethod]
    public void UsuarioCorrecto()
    {
        _categoriaParaTest1.Nombre = "Deporte";
        _categoriaParaTest1.UsuarioCreador = _usuarioParaTest;
        
        Assert.IsTrue(_categoriaParaTest1.UsuarioCreador.Equals(_usuarioParaTest));
    }
    
    [TestMethod]
    public void AgregarEspacioCorrecto()
    {
        _categoriaParaTest1.Nombre = "Deporte";
        _categoriaParaTest1.UsuarioCreador = _usuarioParaTest;
        _categoriaParaTest1.Espacio = _espacioParaTest;
        
        Assert.IsTrue(_categoriaParaTest1.Espacio.Equals(_espacioParaTest));
    }
}