using Dominio;
using Logica;
using LogicaTest.Contexto;
using Memoria;
using Memoria.BaseDeDatosRepositorios;
using Memoria.SesionActual;

namespace LogicaTest;

[TestClass]
public class UsuarioLogicaTest
{
    private SQLContexto _contexto;
    private UsuarioLogica _usuarioLogicaParaTest;
    private UsuarioBDRepositorio _usuarioRepositorioParaTest;
    private SesionActualMemoria _sesionActualParaTest;
    
    private Usuario _usuarioParaTest1;
    private Usuario _usuarioParaTest2;
    private Usuario _usuarioParaTest3;
    
    [TestInitialize]
    public void Inicio()
    {
        FabricaSQLContexto fabricaSQLContext = new FabricaSQLContexto();
        _contexto = fabricaSQLContext.CrearContextoMemoria();
        
        _sesionActualParaTest = new SesionActualMemoria();
        _usuarioRepositorioParaTest = new UsuarioBDRepositorio(_contexto);
        _usuarioLogicaParaTest = new UsuarioLogica(_usuarioRepositorioParaTest, _sesionActualParaTest);

        _usuarioParaTest1 = new Usuario()
        {
            Correo = "juan@adinet.com",
            Contrasena = "juanBall12345",
            Nombre = "Juan",
            Apellido = "Min",
            Direccion = "Av.China"
        };
        
        _usuarioParaTest2 = new Usuario()
        {
            Correo = "pepe@gmail.com",
            Contrasena = "Pepe111000111",
            Nombre = "Pepe",
            Apellido = "Charl",
            Direccion = "Castillo 9876"
        };
        
        _usuarioParaTest3 = new Usuario()
        {
            Correo = "pepe@gmail.com",
            Contrasena = "Pepe1AACTUALIZADO",
            Nombre = "Pepe",
            Apellido = "Charl",
            Direccion = "Castillo 9876"
        };
    }
    
    [TestCleanup]
    public void Fin()
    {
        _contexto.Database.EnsureDeleted();
    }

    [TestMethod]
    public void AgregarUsuarioCorrecto()
    {
        Usuario agregado = _usuarioLogicaParaTest.AgregarUsuario(_usuarioParaTest1);
        
        Assert.AreEqual(_usuarioParaTest1.Correo, agregado.Correo);
        Assert.AreEqual(_usuarioParaTest1.Contrasena, agregado.Contrasena);
        Assert.AreEqual(_usuarioParaTest1.Nombre, agregado.Nombre);
        Assert.AreEqual(_usuarioParaTest1.Apellido, agregado.Apellido);
        Assert.AreEqual(_usuarioParaTest1.Direccion, agregado.Direccion);
    }
    
    [TestMethod]
    public void EncontrarUsuarioCorrecta()
    {
        Usuario agregada = _usuarioLogicaParaTest.AgregarUsuario(_usuarioParaTest1);
        Assert.AreEqual(agregada, _usuarioLogicaParaTest.EncontrarUsuario(_usuarioParaTest1.Id));
    }
    
    [TestMethod]
    public void EncontrarUsuarioNoEstaDevuelveNuloCorrecta()
    {
        Assert.AreEqual(null, _usuarioLogicaParaTest.EncontrarUsuario(_usuarioParaTest1.Id));
    }
    
    [TestMethod]
    public void ListarTodosLosUsuariosCorrecto()
    {
        _usuarioLogicaParaTest.AgregarUsuario(_usuarioParaTest1);
        _usuarioLogicaParaTest.AgregarUsuario(_usuarioParaTest2);
        Assert.IsTrue(
            _usuarioLogicaParaTest.ListarUsuarios()
                .Contains(_usuarioParaTest1) 
            && 
            _usuarioLogicaParaTest.ListarUsuarios()
                .Contains(_usuarioParaTest2));
    }

    [TestMethod]
    public void EliminarUsuarioNoExistenteDevuelveNull()
    {
        Assert.AreEqual(null, _usuarioLogicaParaTest.EliminarUsuario(_usuarioParaTest1.Id));
    }

    [TestMethod]
    public void EliminarUsuarioCorrecto()
    {
        _usuarioLogicaParaTest.AgregarUsuario(_usuarioParaTest1);
        int largo = _usuarioLogicaParaTest.ListarUsuarios().Count;
        Assert.AreEqual(_usuarioParaTest1, _usuarioLogicaParaTest.EliminarUsuario(_usuarioParaTest1.Id));
        Assert.IsTrue(_usuarioLogicaParaTest.ListarUsuarios().Count == largo - 1);
    }
    
    [TestMethod]
    public void IniciarSesionCorrecto()
    {
        _usuarioLogicaParaTest.AgregarUsuario(_usuarioParaTest1);
        _usuarioLogicaParaTest.IniciarSesion(_usuarioParaTest1);
        Assert.AreEqual(_usuarioParaTest1, _usuarioLogicaParaTest.UsuarioActual());
    }
    
    [TestMethod]
    [ExpectedException(typeof(LogicaExcepcion))]
    public void IniciarSesionIncorrecto()
    {
        _usuarioLogicaParaTest.IniciarSesion(_usuarioParaTest1);
    }
    
    [TestMethod]
    public void NadieIniciadoCorrecto()
    {
        Assert.AreEqual(null, _usuarioLogicaParaTest.UsuarioActual());
    }
    
    [TestMethod]
    [ExpectedException(typeof(LogicaExcepcion))]
    public void IniciarSesionContrasenaIncorrecta()
    {
        _usuarioLogicaParaTest.AgregarUsuario(_usuarioParaTest1);
        Usuario contrasenaIncorrecta = new Usuario()
        {
            Correo = _usuarioParaTest1.Correo,
            Contrasena = "juanBall123451"
        };
        _usuarioLogicaParaTest.IniciarSesion(contrasenaIncorrecta);
    }
    
    [TestMethod]
    public void RegistrarseCorrecto()
    {
        _usuarioLogicaParaTest.Registrarse(_usuarioParaTest1, "juanBall12345");
        Assert.AreEqual(_usuarioParaTest1, _usuarioLogicaParaTest.EncontrarUsuario(_usuarioParaTest1.Id));
    }
    
    [TestMethod]
    [ExpectedException(typeof(LogicaExcepcion))]
    public void ConfirmarContrasenaIncorrecto()
    {
        _usuarioLogicaParaTest.Registrarse(_usuarioParaTest1, "pepeDOL167348763");
    }
    
    [TestMethod]
    [ExpectedException(typeof(LogicaExcepcion))]
    public void UsuarioConEseCorreoYaExiste()
    {   
        _usuarioLogicaParaTest.AgregarUsuario(_usuarioParaTest1);
        _usuarioLogicaParaTest.Registrarse(_usuarioParaTest1, "juanBall12345");
    }
    
    [TestMethod]
    public void CerrarSesionCorrecto()
    {
        _usuarioLogicaParaTest.AgregarUsuario(_usuarioParaTest1);
        _usuarioLogicaParaTest.IniciarSesion(_usuarioParaTest1);
        _usuarioLogicaParaTest.CerrarSesion();
        Assert.AreEqual(null, _usuarioLogicaParaTest.UsuarioActual());
    }
    
    [TestMethod]
    public void ActualizarUsuarioCorrecto()
    {
        _usuarioLogicaParaTest.AgregarUsuario(_usuarioParaTest2);
        _usuarioParaTest3.Id = _usuarioParaTest2.Id;
        _usuarioLogicaParaTest.ActualizarUsuario(_usuarioParaTest3);
        Assert.AreEqual(_usuarioParaTest3.Nombre, _usuarioParaTest2.Nombre);
        Assert.AreEqual(_usuarioParaTest3.Apellido, _usuarioParaTest2.Apellido);
        Assert.AreEqual(_usuarioParaTest3.Correo, _usuarioParaTest2.Correo);
        Assert.AreEqual(_usuarioParaTest3.Contrasena, _usuarioParaTest2.Contrasena);
        Assert.AreEqual(_usuarioParaTest3.Direccion, _usuarioParaTest2.Direccion);
    }
    
    [TestMethod]
    [ExpectedException(typeof(LogicaExcepcion))]
    public void PermitirCambiarContraseñaIncorrecto()
    {
        _usuarioLogicaParaTest.PermitirCambiarContraseña(_usuarioParaTest1, "noEsLaDeUsuarioParaTest1");
    }
    
    [TestMethod]
    public void PermitirCambiarContraseñaCorrecto()
    {
        _usuarioLogicaParaTest.PermitirCambiarContraseña(_usuarioParaTest1, "juanBall12345");
    }
    
    
}