using Dominio;

namespace DominioTest;

[TestClass]
public class UsuarioTest
{
    private Usuario _usuarioParaTest1;
    private Usuario _usuarioParaTest2;
    private Espacio _espacioParaTest1;

    [TestInitialize]
    public void Inicio()
    {
        _usuarioParaTest1 = new Usuario()
        {
            Id = 1,
            Correo = "hola@adinet.com",
            Contrasena = "Hola123456789",
            Nombre = "Juancito",
            Apellido = "Perez",
            Direccion = ""
        };

        _usuarioParaTest2 = new Usuario()
        {
            Id = 2,
            Correo = "hola123@gmail.com"
        };
        _espacioParaTest1 = new Espacio()
        {
            Id = 1,
            Nombre = "Personal",
            Administrador = _usuarioParaTest1,
        };
        
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void CorreoVacioIncorrecto()
    {
        _usuarioParaTest1.Correo = "";
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void CorreoNuloIncorrecto()
    {
        _usuarioParaTest1.Correo = (null);
    }
    
    [TestMethod]
    public void CorreoFormatoCorreoOk()
    {
        Assert.AreEqual("hola@adinet.com", _usuarioParaTest1.Correo);
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void CorreoNoTieneArrobaIncorrecto()
    {
        _usuarioParaTest1.Correo = "holaadinet.com";
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void CorreoNoTienePuntoComIncorrecto()
    {
        _usuarioParaTest1.Correo = "hola@adinetcom";
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void CorreoNoTienePuntoComAlFinalIncorrecto()
    {
        _usuarioParaTest1.Correo = "hola@.comadinet";
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void CorreoNoTieneCaracteresALaIzquierdaDelArrobaIncorrecto()
    {
        _usuarioParaTest1.Correo = "@adinet.com";
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void CorreoNoTieneCaracteresALaDerechaDelArrobaIncorrecto()
    {
        _usuarioParaTest1.Correo = "hola@.com";
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void CorreoNoTieneCaracteresALaDerechaEIzquierdaDelArrobaIncorrecto()
    {
        _usuarioParaTest1.Correo = "@.com";
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void CotrasenaVacioIncorrecto()
    {
        _usuarioParaTest1.Contrasena = "";
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void ContrasenaNulaIncorrecto()
    {
        _usuarioParaTest1.Contrasena = (null);
    }
    
    [TestMethod]
    public void ContrasenaFormatoCorrectoOk()
    {
        Assert.AreEqual("Hola123456789", _usuarioParaTest1.Contrasena);
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void ContrasenaMenorA10CaracteresIncorrecto()
    {
        _usuarioParaTest1.Contrasena = "hola12345";
    }
        
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void ContrasenaMayorA30CaracteresIncorrecto()
    {
        _usuarioParaTest1.Contrasena = "hola1234594217548910297498102929489892918";
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void ContrasenaNoContieneMayusculaIncorrecto()
    {
        _usuarioParaTest1.Contrasena = "hola123459";
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void NombreVacioIncorrecto()
    {
        _usuarioParaTest1.Nombre = "";
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void NombreNuloIncorrecto()
    {
        _usuarioParaTest1.Nombre = null;
    }
    
    [TestMethod]
    public void NombreCorrectoOk()
    {
        Assert.AreEqual("Juancito",_usuarioParaTest1.Nombre);
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void ApellidoVacioIncorrecto()
    {
        _usuarioParaTest1.Apellido = "";
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void ApellidoNuloIncorrecto()
    {
        _usuarioParaTest1.Apellido = null;
    }
    
    [TestMethod]
    public void ApellidoCorrectoOk()
    {
        Assert.AreEqual("Perez",_usuarioParaTest1.Apellido);
    }
    
    [TestMethod]
    public void DireccionVaciaOk()
    {
        Assert.AreEqual("",_usuarioParaTest1.Direccion);
    }
    
    [TestMethod]
    public void DireccionNulaEsVacia()
    {
        _usuarioParaTest1.Direccion = null;
        Assert.AreEqual("", _usuarioParaTest1.Direccion);
    }
    
    [TestMethod]
    public void DireccionTieneUnValorOk()
    {
        _usuarioParaTest1.Direccion = "Ruta 5 nueva km 67";
        Assert.AreEqual("Ruta 5 nueva km 67",_usuarioParaTest1.Direccion);
    }

    [TestMethod]
    public void SonUsuariosDiferentes()
    {
        Assert.IsFalse(_usuarioParaTest2.Equals(_usuarioParaTest1));
    }

    [TestMethod]
    public void SonUsuariosIguales()
    {
        _usuarioParaTest2.Id = 1;
        Assert.IsTrue(_usuarioParaTest2.Equals(_usuarioParaTest1));
    }
    
    [TestMethod]
    public void AgregarEspaciosQueAdministraCorrecto()
    {
        List<Espacio> espaciosQueAdministra = new List<Espacio>();
        espaciosQueAdministra.Add(_espacioParaTest1);
        _usuarioParaTest1.EspaciosQueAdministra = espaciosQueAdministra;
        Assert.IsTrue( _usuarioParaTest1.EspaciosQueAdministra.Contains(_espacioParaTest1));
    }
    
    [TestMethod]
    public void AgregarEspaciosCorrecto()
    {
        List<Espacio> espacios = new List<Espacio>();
        espacios.Add(_espacioParaTest1);
        _usuarioParaTest2.Espacios = espacios;
        Assert.IsTrue( _usuarioParaTest2.Espacios.Contains(_espacioParaTest1));
    }
}