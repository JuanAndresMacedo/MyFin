using Dominio;
using Logica;
using Memoria;

namespace LogicaTest;


[TestClass]
public class EspacioLogicaTest
{
    private EspacioMemoriaRepositorio _espacioRepositorioParaTest;
    private EspacioLogica _espacioLogicaParaTest;
    private Espacio _espacioParaTest;
    private Espacio _espacioParaTest2;
    private Usuario _usuarioAdminisradorParaTest;
    private Usuario _usuarioParticipanteParaTest1;
    private Usuario _usuarioParticipanteParaTest2;
    private Usuario _usuarioParticipanteParaTest3;
    
    
    [TestInitialize]
    public void inicio()
    {
        _espacioRepositorioParaTest = new EspacioMemoriaRepositorio();
        _espacioLogicaParaTest = new EspacioLogica(_espacioRepositorioParaTest);

        _usuarioAdminisradorParaTest = new Usuario()
        {
            Correo = "juan@adinet.com",
            Contrasena = "juanBall12345",
            Nombre = "Juan",
            Apellido = "Min",
            Direccion = "Av.China"
        };

        _usuarioParticipanteParaTest1 = new Usuario()
        {
            Correo = "pepe@gmail.com",
            Contrasena = "Pepe111000111",
            Nombre = "Pepe",
            Apellido = "Charl",
            Direccion = "Castillo 9876"
        };

        _usuarioParticipanteParaTest2 = new Usuario()
        {
            Correo = "ramon@gmail.com",
            Contrasena = "Ramon45625325",
            Nombre = "Ramon",
            Apellido = "Diaz",
            Direccion = "Belvedere 1202"
        };
        
        _usuarioParticipanteParaTest3 = new Usuario()
        {
            Correo = "ignacioSe@gmail.com",
            Contrasena = "2223398745RRRRRR",
            Nombre = "Ignacio",
            Apellido = "Gonzales",
            Direccion = "E.Castellanos 45"
        };

        _espacioParaTest = new Espacio()
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
    
    [TestMethod]
    public void AgregarUnEspacioCorrecto()
    {
        Espacio agregado = _espacioLogicaParaTest.AgregarEspacio(_espacioParaTest);
        
        Assert.AreEqual(_espacioParaTest.Nombre, agregado.Nombre);
        Assert.AreEqual(_espacioParaTest.Administrador, agregado.Administrador);
        Assert.AreEqual(_espacioParaTest.Participantes, agregado.Participantes);
    }

    [TestMethod]
    [ExpectedException(typeof(LogicaExcepcion))]
    public void AgregarDosEspaciosDeMismoNombreElMismoUsuarioIncorrecto()
    {
        _espacioLogicaParaTest.AgregarEspacio(_espacioParaTest);
        _espacioLogicaParaTest.AgregarEspacio(_espacioParaTest);
        
    }

    [TestMethod]
    public void EncontrarUnEspacioCorrecto()
    {
        Espacio agregado = _espacioLogicaParaTest.AgregarEspacio(_espacioParaTest);
        Assert.AreEqual(agregado, _espacioLogicaParaTest.EncontrarEspacio(_espacioParaTest.Id));
    }

    [TestMethod]
    public void NoEncontrarEspacioDevuelveNull()
    {
        Assert.AreEqual(null, _espacioLogicaParaTest.EncontrarEspacio(_espacioParaTest.Id));
    }

    [TestMethod]
    public void ListarTodasLosEspaciosCorrecto()
    {
        _espacioLogicaParaTest.AgregarEspacio(_espacioParaTest);
        _espacioLogicaParaTest.AgregarEspacio(_espacioParaTest2);
        Assert.IsTrue(
            _espacioLogicaParaTest.ListarEspacios().Contains(_espacioParaTest)
            && _espacioLogicaParaTest.ListarEspacios().Contains(_espacioParaTest2));
    }

    [TestMethod]
    public void EliminarEspacioNoExistenteDevuelveNull()
    {
        Assert.AreEqual(null, _espacioLogicaParaTest.EliminarEspacio(_espacioParaTest.Id));
    }

    [TestMethod]
    public void EliminarEspacioCorrecto()
    {
        _espacioLogicaParaTest.AgregarEspacio(_espacioParaTest);
        int largo = _espacioLogicaParaTest.ListarEspacios().Count;
        Assert.AreEqual(_espacioParaTest, _espacioLogicaParaTest.EliminarEspacio(_espacioParaTest.Id));
        Assert.IsTrue(_espacioLogicaParaTest.ListarEspacios().Count == largo - 1);
    }
    
    [TestMethod]
    public void ListarEspaciosDeUnUsuario()
    {
        _espacioLogicaParaTest.AgregarEspacio(_espacioParaTest);
        Assert.IsTrue(_espacioLogicaParaTest.ListarEspaciosDeUnUsuario(_usuarioAdminisradorParaTest).Contains(_espacioParaTest));
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
        _espacioLogicaParaTest.AgregarEspacio(_espacioParaTest);
        _espacioLogicaParaTest.EspacioActual = _espacioParaTest;
        _espacioLogicaParaTest.SalirDelEspacio();
        Assert.AreEqual(null, _espacioLogicaParaTest.EspacioActual);
    }

    [TestMethod]
    public void AsignaryDevolverEspacioActual()
    {
        _espacioLogicaParaTest.AgregarEspacio(_espacioParaTest);
        _espacioLogicaParaTest.AsignarEspacioActual(_espacioParaTest.Id);
        Assert.AreEqual(_espacioParaTest, _espacioLogicaParaTest.EspacioActual);
    }

    [TestMethod]
    [ExpectedException(typeof(LogicaExcepcion))]
    public void ActualizarNombreEspacioIncorrecto()
    {
        _espacioLogicaParaTest.AgregarEspacio(_espacioParaTest);
        _espacioLogicaParaTest.AsignarEspacioActual(_espacioParaTest.Id);
        _espacioParaTest2.Administrador = _usuarioAdminisradorParaTest;
        _espacioLogicaParaTest.AgregarEspacio(_espacioParaTest2);
        _espacioLogicaParaTest.ActualizarEspacio(_espacioParaTest2);
    }

    [TestMethod]
    public void ActualizarNombreEspacioCorrecto()
    {
        _espacioLogicaParaTest.AgregarEspacio(_espacioParaTest);
        _espacioLogicaParaTest.AsignarEspacioActual(_espacioParaTest.Id);
        _espacioParaTest2.Nombre = "Ricardo's dream";
        _espacioParaTest2.Id = _espacioParaTest.Id;
        _espacioLogicaParaTest.ActualizarEspacio(_espacioParaTest2);
        Assert.AreEqual("Ricardo's dream", _espacioLogicaParaTest.EspacioActual.Nombre);
    }

    [TestMethod]
    public void EliminarParticipanteCorrecto()
    {
        _espacioLogicaParaTest.AgregarEspacio(_espacioParaTest2);
        _espacioLogicaParaTest.EspacioActual = _espacioParaTest2;
        Assert.IsTrue(_espacioParaTest2.Participantes.Contains(_usuarioParticipanteParaTest3));
        _espacioLogicaParaTest.EliminarParticipante(_usuarioParticipanteParaTest3);
        Assert.IsFalse(_espacioParaTest2.Participantes.Contains(_usuarioParticipanteParaTest3));
    }
    
    [TestMethod]
    public void AgregarNuevoParticipante()
    {
        _espacioLogicaParaTest.AgregarEspacio(_espacioParaTest2);
        _espacioLogicaParaTest.EspacioActual = _espacioParaTest2;
        _espacioLogicaParaTest.AgregarNuevoParticipante(_usuarioParticipanteParaTest2);
        Assert.IsTrue(_espacioParaTest2.Participantes.Contains(_usuarioParticipanteParaTest2));
    }
    
    
}