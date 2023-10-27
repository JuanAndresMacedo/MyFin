using Dominio;

namespace DominioTest;

[TestClass]
public class EspacioTest
{
    private Espacio _espacioPrueba;
    private Espacio _espacioPrueba1;
    private Usuario _usuarioPrueba;
    private Usuario _usuarioPrueba2;
    private Usuario _usuarioPrueba3;

    [TestInitialize]
    public void Inicio()
    {
        _usuarioPrueba = new Usuario()
        {
            Correo = "pepe@gmail.com"
        };
        
        _espacioPrueba = new Espacio()
        {
            Administrador = _usuarioPrueba,
        };
        
        _espacioPrueba1 = new Espacio()
        {
            Administrador = _usuarioPrueba,
        };
        
        _usuarioPrueba2 = new Usuario()
        {
            Correo = "pedrox@gmail.com"
        };
        
        _usuarioPrueba3 = new Usuario()
        {
            Correo = "carlos3@gmail.com"
        };
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void NombreVacioIncorrecto()
    {
        _espacioPrueba.Nombre = ("");
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void UsuarioNuloIncorrecto()
    {
        _espacioPrueba.Nombre = (null);
    }

    [TestMethod]
    public void UsuarioCorrecto()
    {
        _espacioPrueba.Nombre = ("Alberto");
        Assert.AreEqual("Alberto", _espacioPrueba.Nombre);
    }

    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void AdminVacioIncorrecto()
    {
        _espacioPrueba.Administrador = (null);
    }

    [TestMethod]
    public void AdminCorrecto()
    {
        _espacioPrueba.Administrador = (_usuarioPrueba);
        Assert.AreEqual(_usuarioPrueba, _espacioPrueba.Administrador);
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void NoSePuedeAgregarAdminIncorrecto()
    {
        _espacioPrueba.AgregarParticipante(_usuarioPrueba);
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void NoSePuedeAgregarElMismoParticipanteIncorrecto()
    {
        _espacioPrueba.AgregarParticipante(_usuarioPrueba2);
        _espacioPrueba.AgregarParticipante(_usuarioPrueba2);
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void ParticipanteVacioIncorrecto()
    {
        _espacioPrueba.AgregarParticipante(null);
    }

    [TestMethod]
    public void ParticipanteAñadidoCorrectamente()
    {
        _espacioPrueba.AgregarParticipante(_usuarioPrueba2);
        Assert.IsTrue(_espacioPrueba.Participantes.Exists(x => x == _usuarioPrueba2));
    }
    
    [TestMethod]
    public void ParticipanteSAñadidoSCorrectamente()
    {
        _espacioPrueba.AgregarParticipante(_usuarioPrueba2);
        _espacioPrueba.AgregarParticipante(_usuarioPrueba3);
        Assert.IsTrue(_espacioPrueba.Participantes.Exists(x => x == _usuarioPrueba2));
        Assert.IsTrue(_espacioPrueba.Participantes.Exists(x => x == _usuarioPrueba3));
    }

    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void ParticipanteDuplicadoIncorrecto()
    {
        _espacioPrueba.AgregarParticipante(_usuarioPrueba);
        _espacioPrueba.AgregarParticipante(_usuarioPrueba);
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void BorrarCuandoNoHayParticipantesIncorrecto()
    {
        _espacioPrueba.BorrarParticipante(_usuarioPrueba);
    }

    [TestMethod]
    public void BorrarParticipanteCorrecto()
    {
        _espacioPrueba.AgregarParticipante(_usuarioPrueba2);
        int largo = _espacioPrueba.Participantes.Count;
        _espacioPrueba.BorrarParticipante(_usuarioPrueba2);

        Assert.AreEqual(largo - 1, _espacioPrueba.Participantes.Count);
    }

    [TestMethod]
    public void NoEncuentraParticipante()
    {
        Assert.AreEqual(null, _espacioPrueba.EncontrarParticipante(_usuarioPrueba));
    }

    [TestMethod]
    public void EncuentraParticipante()
    {
        _espacioPrueba.AgregarParticipante(_usuarioPrueba2);
        Assert.AreEqual(_usuarioPrueba2, _espacioPrueba.EncontrarParticipante(_usuarioPrueba2));
    }

    [TestMethod]
    public void DevuelveListaVacia()
    {
        Assert.AreEqual(0, _espacioPrueba.Participantes.Count);
    }

    [TestMethod]
    public void DevuelveListaCorrecta()
    {
        _usuarioPrueba2.Correo = "marcos34@gmail.com";
        _espacioPrueba.AgregarParticipante(_usuarioPrueba2);

        Assert.AreEqual(1, _espacioPrueba.Participantes.Count);
    }

    [TestMethod]
    public void SonEspaciosDiferentesCorrecto()
    {
        _espacioPrueba.Nombre = ("Personal");
        _espacioPrueba.Administrador = (_usuarioPrueba);
        
        _espacioPrueba1.Nombre = ("Familia");
        _espacioPrueba1.Administrador = (_usuarioPrueba);
        Assert.IsFalse(_espacioPrueba.Equals(_espacioPrueba1));
    }
    
    [TestMethod]
    public void SonEspaciosIgualesCorrecto()
    {
        _espacioPrueba.Id = 0;
        _espacioPrueba.Administrador = (_usuarioPrueba);

        _espacioPrueba1.Id = 0;
        _espacioPrueba1.Administrador = (_usuarioPrueba);
        Assert.IsTrue(_espacioPrueba.Equals(_espacioPrueba1));
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void AgregarAdministradorAParticipantesIncorrecto()
    {
        _espacioPrueba.AgregarParticipante(_espacioPrueba.Administrador);
    }
}