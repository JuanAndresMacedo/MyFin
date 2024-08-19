using Dominio;
using Logica;
using LogicaTest.Contexto;
using Memoria;
using Memoria.BaseDeDatosRepositorios;
using Memoria.SesionActual;

namespace LogicaTest;


[TestClass]
public class EspacioLogicaTest
{
    private SQLContexto _contexto;
    
    private UsuarioBDRepositorio _usuarioRepositorioParaTest;
    private UsuarioLogica _usuarioLogicaParaTest;
    
    private EspacioBDRepositorio _espacioRepositorioParaTest;
    private EspacioLogica _espacioLogicaParaTest;
    private SesionActualMemoria _sesionActualParaTest;
    
    private Espacio _espacioParaTest1;
    private Espacio _espacioParaTest2;
    
    private Usuario _usuarioAdminisradorParaTest;
    private Usuario _usuarioParticipanteParaTest1;
    private Usuario _usuarioParticipanteParaTest2;
    private Usuario _usuarioParticipanteParaTest3;
    
    
    [TestInitialize]
    public void inicio()
    {
        FabricaSQLContexto fabricaSQLContext = new FabricaSQLContexto();
        _contexto = fabricaSQLContext.CrearContextoMemoria();

        _sesionActualParaTest = new SesionActualMemoria();
        _espacioRepositorioParaTest = new EspacioBDRepositorio(_contexto);
        _espacioLogicaParaTest = new EspacioLogica(_espacioRepositorioParaTest, _sesionActualParaTest);

        _usuarioRepositorioParaTest = new UsuarioBDRepositorio(_contexto);
        _usuarioLogicaParaTest = new UsuarioLogica(_usuarioRepositorioParaTest, _sesionActualParaTest);
        
        _usuarioAdminisradorParaTest = new Usuario()
        {
            Id = 1,
            Correo = "juan@adinet.com",
            Contrasena = "juanBall12345",
            Nombre = "Juan",
            Apellido = "Min",
            Direccion = "Av.China"
        };

        _usuarioParticipanteParaTest1 = new Usuario()
        {
            Id = 2,
            Correo = "pepe@gmail.com",
            Contrasena = "Pepe111000111",
            Nombre = "Pepe",
            Apellido = "Charl",
            Direccion = "Castillo 9876"
        };

        _usuarioParticipanteParaTest2 = new Usuario()
        {
            Id = 3,
            Correo = "ramon@gmail.com",
            Contrasena = "Ramon45625325",
            Nombre = "Ramon",
            Apellido = "Diaz",
            Direccion = "Belvedere 1202"
        };
        
        _usuarioParticipanteParaTest3 = new Usuario()
        {
            Id = 4,
            Correo = "ignacioSe@gmail.com",
            Contrasena = "2223398745RRRRRR",
            Nombre = "Ignacio",
            Apellido = "Gonzales",
            Direccion = "E.Castellanos 45"
        };

        _espacioParaTest1 = new Espacio()
        {
            Nombre = "espacio1",
            Administrador = _usuarioAdminisradorParaTest,
            Participantes = { _usuarioParticipanteParaTest1, _usuarioParticipanteParaTest2, _usuarioParticipanteParaTest3 }
        };

        _espacioParaTest2 = new Espacio()
        {
            Nombre = "espacio2",
            Administrador = _usuarioParticipanteParaTest1,
            Participantes = { _usuarioAdminisradorParaTest, _usuarioParticipanteParaTest3 }
        };
    }
    
    [TestCleanup]
    public void Fin()
    {
        _contexto.Database.EnsureDeleted();
    }
    
    [TestMethod]
    public void AgregarUnEspacioCorrecto()
    {
        Espacio agregado = _espacioLogicaParaTest.AgregarEspacio(_espacioParaTest1);
        
        Assert.AreEqual(_espacioParaTest1.Nombre, agregado.Nombre);
        Assert.AreEqual(_espacioParaTest1.Administrador, agregado.Administrador);
        Assert.AreEqual(_espacioParaTest1.Participantes, agregado.Participantes);
    }

    [TestMethod]
    [ExpectedException(typeof(LogicaExcepcion))]
    public void AgregarDosEspaciosDeMismoNombreElMismoUsuarioIncorrecto()
    {
        _espacioLogicaParaTest.AgregarEspacio(_espacioParaTest1);
        _espacioLogicaParaTest.AgregarEspacio(_espacioParaTest1);
        
    }

    [TestMethod]
    public void EncontrarUnEspacioCorrecto()
    {
        Espacio agregado = _espacioLogicaParaTest.AgregarEspacio(_espacioParaTest1);
        Assert.AreEqual(agregado, _espacioLogicaParaTest.EncontrarEspacio(_espacioParaTest1.Id));
    }

    [TestMethod]
    public void NoEncontrarEspacioDevuelveNull()
    {
        Assert.AreEqual(null, _espacioLogicaParaTest.EncontrarEspacio(_espacioParaTest1.Id));
    }

    [TestMethod]
    public void ListarTodasLosEspaciosCorrecto()
    {
        _espacioLogicaParaTest.AgregarEspacio(_espacioParaTest1);
        _espacioLogicaParaTest.AgregarEspacio(_espacioParaTest2);
        Assert.IsTrue(
            _espacioLogicaParaTest.ListarEspacios().Contains(_espacioParaTest1)
            && _espacioLogicaParaTest.ListarEspacios().Contains(_espacioParaTest2));
    }

    [TestMethod]
    public void EliminarEspacioNoExistenteDevuelveNull()
    {
        Assert.AreEqual(null, _espacioLogicaParaTest.EliminarEspacio(_espacioParaTest1.Id));
    }

    [TestMethod]
    public void EliminarEspacioCorrecto()
    {
        _espacioLogicaParaTest.AgregarEspacio(_espacioParaTest1);
        
        int largo = _espacioLogicaParaTest.ListarEspacios().Count;
        
        Assert.AreEqual(_espacioParaTest1, _espacioLogicaParaTest.EliminarEspacio(_espacioParaTest1.Id));
        Assert.IsTrue(_espacioLogicaParaTest.ListarEspacios().Count == largo - 1);
    }
    
    [TestMethod]
    public void ListarEspaciosDeUnUsuario()
    {
        _espacioLogicaParaTest.AgregarEspacio(_espacioParaTest1);
        Assert.IsTrue(_espacioLogicaParaTest.ListarEspaciosDeUnUsuario(_usuarioAdminisradorParaTest).Contains(_espacioParaTest1));
    }
    
    [TestMethod]
    public void CrearEspacioPrincipalCorrecto()
    {
        Espacio espacioPrincipal =_espacioLogicaParaTest.CrearEspacioPrincipal(_usuarioAdminisradorParaTest);
        Assert.IsTrue(_espacioLogicaParaTest.ListarEspacios().Contains(espacioPrincipal));
    }

    [TestMethod]
    public void SalirDelEspacioCorrecto()
    {
        _espacioLogicaParaTest.AgregarEspacio(_espacioParaTest1);
        _sesionActualParaTest.EspacioActual = _espacioParaTest1;
        _espacioLogicaParaTest.SalirDelEspacio();
        Assert.AreEqual(null, _espacioLogicaParaTest.EspacioActual());
    }

    [TestMethod]
    public void AsignaryDevolverEspacioActual()
    {
        _espacioLogicaParaTest.AgregarEspacio(_espacioParaTest1);
        _espacioLogicaParaTest.AsignarEspacioActual(_espacioParaTest1.Id);
        Assert.AreEqual(_espacioParaTest1, _espacioLogicaParaTest.EspacioActual());
    }

    [TestMethod]
    [ExpectedException(typeof(LogicaExcepcion))]
    public void ActualizarNombreEspacioIncorrecto()
    {
        _espacioLogicaParaTest.AgregarEspacio(_espacioParaTest1);
        _espacioLogicaParaTest.AsignarEspacioActual(_espacioParaTest1.Id);
        _espacioParaTest2.Administrador = _usuarioAdminisradorParaTest;
        _espacioLogicaParaTest.AgregarEspacio(_espacioParaTest2);
        _espacioLogicaParaTest.ActualizarEspacio(_espacioParaTest2);
    }

    [TestMethod]
    public void ActualizarNombreEspacioCorrecto()
    {
        _espacioLogicaParaTest.AgregarEspacio(_espacioParaTest1);
        _espacioLogicaParaTest.AsignarEspacioActual(_espacioParaTest1.Id);
        
        _espacioParaTest2.Nombre = "Ricardo's dream";
        _espacioParaTest2.Id = _espacioParaTest1.Id;
        _espacioLogicaParaTest.ActualizarEspacio(_espacioParaTest2);
        
        Assert.AreEqual("Ricardo's dream", _espacioLogicaParaTest.EspacioActual().Nombre);
    }

    [TestMethod]
    public void EliminarParticipanteCorrecto()
    {
        _espacioLogicaParaTest.AgregarEspacio(_espacioParaTest2);
        _sesionActualParaTest.EspacioActual = _espacioParaTest2;
        Assert.IsTrue(_espacioParaTest2.Participantes.Contains(_usuarioParticipanteParaTest3));
        _espacioLogicaParaTest.EliminarParticipante(_usuarioParticipanteParaTest3);
        Assert.IsFalse(_espacioParaTest2.Participantes.Contains(_usuarioParticipanteParaTest3));
    }
    
    [TestMethod]
    public void AgregarNuevoParticipante()
    {
        _espacioLogicaParaTest.AgregarEspacio(_espacioParaTest2);
        _usuarioLogicaParaTest.AgregarUsuario(_usuarioParticipanteParaTest2);
        _sesionActualParaTest.EspacioActual = _espacioParaTest2;
        
        _espacioLogicaParaTest.AgregarNuevoParticipante(_usuarioParticipanteParaTest2);
        
        Assert.IsTrue(_espacioParaTest2.Participantes.Contains(_usuarioParticipanteParaTest2));
    }
    
    
}