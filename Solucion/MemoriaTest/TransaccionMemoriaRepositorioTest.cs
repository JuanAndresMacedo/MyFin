using Dominio;
using Memoria;

namespace MemoriaTest;

[TestClass]
public class TransaccionMemoriaRepositorioTest
{
    private TransaccionMemoriaRepositorio _repositorioParaTest;
    private Transaccion _transaccionParaTest1;
    private Transaccion _transaccionParaTest2;
    private Cuenta _cuentaParaTest;
    private Usuario _usuarioParaTest;
    private Espacio _espacioParaTest;

    [TestInitialize]
    public void Inicio()
    {
        _repositorioParaTest = new TransaccionMemoriaRepositorio();
        
        _usuarioParaTest = new Usuario()
        {
            Correo = ("juancito@adinet.com"),
        };

        _espacioParaTest = new Espacio()
        {
            Nombre = "Familia",
            Administrador = _usuarioParaTest,
        };

        _cuentaParaTest = new Monetaria()
        {
            Propietario = _usuarioParaTest,
        };

        _transaccionParaTest1 = new Transaccion()
        {
            Cuenta = _cuentaParaTest,
            Nombre = "Pepinos",
            Espacio = _espacioParaTest,
            Monto = 1,
            Id = 789
        };

        _transaccionParaTest2 = new Transaccion()
        {
            Cuenta = _cuentaParaTest,
            Nombre = "Pepinos",
            Espacio = _espacioParaTest,
            Monto = 32,
            Id = 900
        };
    }
    
    [TestMethod]
    public void IdAgregadaCorrectamente()
    {
        _repositorioParaTest.Agregar(_transaccionParaTest1);
        _repositorioParaTest.Agregar(_transaccionParaTest2);
        
        Assert.IsTrue(_transaccionParaTest1.Id == 1);
        Assert.IsTrue(_transaccionParaTest2.Id == 2);
    }
    
    [TestMethod]
    public void AgregarUnaTransaccionAlRepositorioCorrecto()
    {
        _repositorioParaTest.Agregar(_transaccionParaTest1);
        Assert.AreEqual(_transaccionParaTest1, _repositorioParaTest.Encontrar(x => x.Id == _transaccionParaTest1.Id));
    }

    [TestMethod]
    public void AgregarUnaTransaccionLaDevuelve()
    {
        Assert.AreEqual(_transaccionParaTest2, _repositorioParaTest.Agregar(_transaccionParaTest2));
    }

    [TestMethod]
    public void TransaccionNoEstaAgregadoAlRepositorio()
    {
        Assert.AreEqual(null, _repositorioParaTest.Encontrar(x => x.Id == _transaccionParaTest1.Id));
    }
    
    [TestMethod]
    public void RepositorioDevuelveListaVacia()
    {
        Assert.IsTrue(_repositorioParaTest.ListarTodos().Count == 0);
    }
    
    [TestMethod]
    public void RepositorioDevuelveListaCompleta()
    {
        _repositorioParaTest.Agregar(_transaccionParaTest1);
        _repositorioParaTest.Agregar(_transaccionParaTest2);
        Assert.IsTrue(_repositorioParaTest.ListarTodos().Contains(_transaccionParaTest1) && _repositorioParaTest.ListarTodos().Contains(_transaccionParaTest2));
    }

    [TestMethod]
    public void EliminarUnaTransaccionQueNoExisteDevuelveNull()
    {
        Assert.AreEqual(null, _repositorioParaTest.Eliminar(_transaccionParaTest1.Id));
    }

    [TestMethod]
    public void EliminarUnaTransaccionCorrecta()
    {
        _repositorioParaTest.Agregar(_transaccionParaTest1);
        Assert.IsTrue(_transaccionParaTest1.Equals(_repositorioParaTest.Eliminar(_transaccionParaTest1.Id))
                      && _repositorioParaTest.Encontrar(x => x.Id == _transaccionParaTest1.Id) == null);
    }
    
    [TestMethod]
    public void ActualizarTransaccionCorrecto()
    {
        _transaccionParaTest2.Id = 789;
        _repositorioParaTest.Agregar(_transaccionParaTest1);
        _repositorioParaTest.Actualizar(_transaccionParaTest2);
        Assert.AreEqual(_transaccionParaTest2.Nombre, _transaccionParaTest1.Nombre);
        Assert.AreEqual(_transaccionParaTest2.Id, _transaccionParaTest1.Id);
    }
}