using Dominio;

namespace DominioTest;

[TestClass]
public class EspacioTest
{
    private Espacio _espacioParaTest1;
    private Espacio _espacioParaTest2;
    
    private Usuario _usuarioParaTest1;
    private Usuario _usuarioParaTest2;
    private Usuario _usuarioParaTest3;

    [TestInitialize]
    public void Inicio()
    {
        _usuarioParaTest1 = new Usuario()
        {
            Id = 1,
            Correo = "pepe@gmail.com"
        };
        
        _usuarioParaTest2 = new Usuario()
        {
            Id = 2, 
            Correo = "marcos34@gmail.com",
        };
        
        _usuarioParaTest3 = new Usuario()
        {
            Id = 3,
            Correo = "carlos3@gmail.com"
        };
        
        _espacioParaTest1 = new Espacio()
        {
            Id = 1,
            Nombre = "Personal",
            Administrador = _usuarioParaTest1,
        };
        
        _espacioParaTest2 = new Espacio()
        {
            Id = 2,
            Nombre = "Familia",
            Administrador = _usuarioParaTest1,
        };
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void NombreVacioIncorrecto()
    {
        _espacioParaTest1.Nombre = "";
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void UsuarioNuloIncorrecto()
    {
        _espacioParaTest1.Nombre = null;
    }

    [TestMethod]
    public void UsuarioCorrecto()
    {
        Assert.AreEqual("Personal", _espacioParaTest1.Nombre);
    }

    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void AdminVacioIncorrecto()
    {
        _espacioParaTest1.Administrador = null;
    }

    [TestMethod]
    public void AdminCorrecto()
    {
        Assert.AreEqual(_usuarioParaTest1, _espacioParaTest1.Administrador);
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void NoSePuedeAgregarAdminIncorrecto()
    {
        _espacioParaTest1.AgregarParticipante(_usuarioParaTest1);
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void NoSePuedeAgregarElMismoParticipanteIncorrecto()
    {
        _espacioParaTest1.AgregarParticipante(_usuarioParaTest2);
        _espacioParaTest1.AgregarParticipante(_usuarioParaTest2);
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void ParticipanteVacioIncorrecto()
    {
        _espacioParaTest1.AgregarParticipante(null);
    }

    [TestMethod]
    public void ParticipanteAñadidoCorrectamente()
    {
        _espacioParaTest1.AgregarParticipante(_usuarioParaTest2);
        Assert.IsTrue(_espacioParaTest1.Participantes.Exists(x => x == _usuarioParaTest2));
    }
    
    [TestMethod]
    public void ParticipanteSAñadidoSCorrectamente()
    {
        _espacioParaTest1.AgregarParticipante(_usuarioParaTest2);
        _espacioParaTest1.AgregarParticipante(_usuarioParaTest3);
        Assert.IsTrue(_espacioParaTest1.Participantes.Exists(x => x == _usuarioParaTest2));
        Assert.IsTrue(_espacioParaTest1.Participantes.Exists(x => x == _usuarioParaTest3));
    }

    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void ParticipanteDuplicadoIncorrecto()
    {
        _espacioParaTest1.AgregarParticipante(_usuarioParaTest1);
        _espacioParaTest1.AgregarParticipante(_usuarioParaTest1);
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void BorrarCuandoNoHayParticipantesIncorrecto()
    {
        _espacioParaTest1.EliminarParticipante(_usuarioParaTest1);
    }

    [TestMethod]
    public void BorrarParticipanteCorrecto()
    {
        _espacioParaTest1.AgregarParticipante(_usuarioParaTest2);
        int largo = _espacioParaTest1.Participantes.Count;
        _espacioParaTest1.EliminarParticipante(_usuarioParaTest2);

        Assert.AreEqual(largo - 1, _espacioParaTest1.Participantes.Count);
    }

    [TestMethod]
    public void NoEncuentraParticipante()
    {
        Assert.AreEqual(null, _espacioParaTest1.EncontrarParticipante(_usuarioParaTest1));
    }

    [TestMethod]
    public void EncuentraParticipante()
    {
        _espacioParaTest1.AgregarParticipante(_usuarioParaTest2);
        Assert.AreEqual(_usuarioParaTest2, _espacioParaTest1.EncontrarParticipante(_usuarioParaTest2));
    }

    [TestMethod]
    public void DevuelveListaVacia()
    {
        Assert.AreEqual(0, _espacioParaTest1.Participantes.Count);
    }

    [TestMethod]
    public void DevuelveListaCorrecta()
    {
        _espacioParaTest1.AgregarParticipante(_usuarioParaTest2);
        Assert.AreEqual(1, _espacioParaTest1.Participantes.Count);
    }

    [TestMethod]
    public void SonEspaciosDiferentesCorrecto()
    {
        Assert.IsFalse(_espacioParaTest1.Equals(_espacioParaTest2));
    }
    
    [TestMethod]
    public void SonEspaciosIgualesCorrecto()
    {
        _espacioParaTest2.Id = 1;
        Assert.IsTrue(_espacioParaTest1.Equals(_espacioParaTest2));
    }
    
    [TestMethod]
    [ExpectedException(typeof(DominioExcepcion))]
    public void AgregarAdministradorAParticipantesIncorrecto()
    {
        _espacioParaTest1.AgregarParticipante(_espacioParaTest1.Administrador);
    }
    
    [TestMethod]
    public void AgregarAdministradorIdCorrecto()
    {
        _espacioParaTest1.AdministradorId = 1;
        Assert.AreEqual(1, _espacioParaTest1.AdministradorId);
    }
    
    [TestMethod]
    public void AgregarParticipantesCorrecto()
    {
        List<Usuario> participantes = new List<Usuario>();
        participantes.Add(_usuarioParaTest3);
        _espacioParaTest1.Participantes = participantes;
        Assert.IsTrue(_espacioParaTest1.Participantes.Contains(_usuarioParaTest3));
    }
}