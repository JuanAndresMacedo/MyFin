using Dominio;

namespace DominioTest;

[TestClass]
public class UsuarioTest
{
    private Usuario _usuarioParaTest;
    private Usuario _usuarioParaTest1;

    [TestInitialize]
    public void Inicio()
    {
        _usuarioParaTest = new Usuario();
        _usuarioParaTest1 = new Usuario();
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void CorreoVacioIncorrecto()
    {
        _usuarioParaTest.Correo = "";
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void CorreoNuloIncorrecto()
    {
        _usuarioParaTest.Correo = (null);
    }
    
    [TestMethod]
    public void CorreoFormatoCorreoOk()
    {
        _usuarioParaTest.Correo = "hola@adinet.com";
        Assert.AreEqual("hola@adinet.com", _usuarioParaTest.Correo);
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void CorreoNoTieneArrobaIncorrecto()
    {
        _usuarioParaTest.Correo = "holaadinet.com";
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void CorreoNoTienePuntoComIncorrecto()
    {
        _usuarioParaTest.Correo = "hola@adinetcom";
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void CorreoNoTienePuntoComAlFinalIncorrecto()
    {
        _usuarioParaTest.Correo = "hola@.comadinet";
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void CorreoNoTieneCaracteresALaIzquierdaDelArrobaIncorrecto()
    {
        _usuarioParaTest.Correo = "@adinet.com";
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void CorreoNoTieneCaracteresALaDerechaDelArrobaIncorrecto()
    {
        _usuarioParaTest.Correo = "hola@.com";
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void CorreoNoTieneCaracteresALaDerechaEIzquierdaDelArrobaIncorrecto()
    {
        _usuarioParaTest.Correo = "@.com";
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void CotrasenaVacioIncorrecto()
    {
        _usuarioParaTest.Contrasena = "";
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void ContrasenaNulaIncorrecto()
    {
        _usuarioParaTest.Contrasena = (null);
    }
    
    [TestMethod]
    public void ContrasenaFormatoCorrectoOk()
    {
        _usuarioParaTest.Contrasena = "Hola123456789";
        Assert.AreEqual("Hola123456789", _usuarioParaTest.Contrasena);
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void ContrasenaMenorA10CaracteresIncorrecto()
    {
        _usuarioParaTest.Contrasena = "hola12345";
    }
        
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void ContrasenaMayorA30CaracteresIncorrecto()
    {
        _usuarioParaTest.Contrasena = "hola1234594217548910297498102929489892918";
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void ContrasenaNoContieneMayusculaIncorrecto()
    {
        _usuarioParaTest.Contrasena = "hola123459";
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void NombreVacioIncorrecto()
    {
        _usuarioParaTest.Nombre = "";
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void NombreNuloIncorrecto()
    {
        _usuarioParaTest.Nombre = (null);
    }
    
    [TestMethod]
    public void NombreCorrectoOk()
    {
        _usuarioParaTest.Nombre = "Juancito";
        Assert.AreEqual("Juancito",_usuarioParaTest.Nombre);
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void ApellidoVacioIncorrecto()
    {
        _usuarioParaTest.Apellido = "";
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void ApellidoNuloIncorrecto()
    {
        _usuarioParaTest.Apellido = (null);
    }
    
    [TestMethod]
    public void ApellidoCorrectoOk()
    {
        _usuarioParaTest.Apellido = "Perez";
        Assert.AreEqual("Perez",_usuarioParaTest.Apellido);
    }
    
    [TestMethod]
    public void DireccionVaciaOk()
    {
        _usuarioParaTest.Direccion = "";
        Assert.AreEqual("",_usuarioParaTest.Direccion);
    }
    
    [TestMethod]
    public void DireccionNulaEsVacia()
    {
        _usuarioParaTest.Direccion = (null);
        Assert.AreEqual("", _usuarioParaTest.Direccion);
    }
    
    [TestMethod]
    public void DireccionTieneUnValorOk()
    {
        _usuarioParaTest.Direccion = "Ruta 5 nueva km 67";
        Assert.AreEqual("Ruta 5 nueva km 67",_usuarioParaTest.Direccion);
    }

    [TestMethod]
    public void SonUsuariosDiferentes()
    {
        _usuarioParaTest1.Correo = "hola123@gmail.com";
        _usuarioParaTest.Correo = "chau123@gmail.com";
        Assert.IsFalse(_usuarioParaTest1.Equals(_usuarioParaTest));
    }

    [TestMethod]
    public void SonUsuariosIguales()
    {
        _usuarioParaTest1.Correo = "hola123@gmail.com";
        _usuarioParaTest.Correo = "hola123@gmail.com";
        Assert.IsTrue(_usuarioParaTest1.Equals(_usuarioParaTest));
    }
}