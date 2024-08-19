using Dominio;
using Memoria.SesionActual;

namespace MemoriaTest.SesionActualTest;

[TestClass]
public class SesionActualMemoriaTest
{
    private SesionActualMemoria _sesionActualMemoriaParaTest;
    private Usuario _usuarioParaTest;
    private Espacio _espacioParaTest;

    [TestInitialize]
    public void Inicio()
    {
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

        _sesionActualMemoriaParaTest = new SesionActualMemoria()
        {
            UsuarioActual = _usuarioParaTest,
            EspacioActual = _espacioParaTest,
        };
    }
    
    [TestMethod]
    public void EspacioActualAsignadoCorrectamente()
    {
        Assert.AreEqual(_espacioParaTest, _sesionActualMemoriaParaTest.EspacioActual);
    }
    
    [TestMethod]
    public void UsuarioActualAsignadoCorrectamente()
    {
        Assert.AreEqual(_usuarioParaTest, _sesionActualMemoriaParaTest.UsuarioActual);
    }
    
    [TestMethod]
    public void UsuarioActualPuedeSerNullCorrecto()
    {
        _sesionActualMemoriaParaTest.UsuarioActual = null;
        Assert.AreEqual(null, _sesionActualMemoriaParaTest.UsuarioActual);
    }
    [TestMethod]
    public void EspacioActualPuedeSerNullCorrecto()
    {
        _sesionActualMemoriaParaTest.EspacioActual = null;
        Assert.AreEqual(null, _sesionActualMemoriaParaTest.EspacioActual);
    }
    
}