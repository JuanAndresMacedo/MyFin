using Dominio;
using Memoria;

namespace MemoriaTest;

[TestClass]
public class TipoDeCambioMemoriaRepositorioTest
{
    private TipoDeCambioMemoriaRepositorio _repositorioDeTipoDeCambioParaTest;
    private TipoDeCambio _tipoDeCambioParaTest1;
    private TipoDeCambio _tipoDeCambioParaTest2;
    private Usuario _usuarioParaTest;
    private Espacio _espacioParaTest;
    private DateTime _fechaParaTest1;

    [TestInitialize]
    public void Inicio()
    {
        _repositorioDeTipoDeCambioParaTest = new TipoDeCambioMemoriaRepositorio();
        _fechaParaTest1 = new DateTime(2023,10,1);
        
        _usuarioParaTest = new Usuario()
        {
            Correo = "hola@gmail.com",
        };

        _espacioParaTest = new Espacio()
        {
            Nombre = "Familia",
            Administrador = _usuarioParaTest,
        };

        _tipoDeCambioParaTest1 = new TipoDeCambio()
        {
            UsuarioCreador = _usuarioParaTest,
            Espacio = _espacioParaTest,
            Fecha = _fechaParaTest1,
            ValorDelDolar = 38.4f
        };

        _tipoDeCambioParaTest2 = new TipoDeCambio()
        {
            UsuarioCreador = _usuarioParaTest,
            Espacio = _espacioParaTest,
            Fecha = _fechaParaTest1,
            ValorDelDolar = 41.2f
        };
    }

    [TestMethod]
    public void IdAgregadaCorrectamente()
    {
        _repositorioDeTipoDeCambioParaTest.Agregar(_tipoDeCambioParaTest1);
        _repositorioDeTipoDeCambioParaTest.Agregar(_tipoDeCambioParaTest2);
        
        Assert.IsTrue(_tipoDeCambioParaTest1.Id == 1);
        Assert.IsTrue(_tipoDeCambioParaTest2.Id == 2);
    }
    
    [TestMethod]
    public void AgregarUnTipoDeCambioAlRepositorioCorrecto()
    {
        _repositorioDeTipoDeCambioParaTest.Agregar(_tipoDeCambioParaTest1);
        Assert.AreEqual(_tipoDeCambioParaTest1, _repositorioDeTipoDeCambioParaTest.Encontrar(
            x => x.Id == _tipoDeCambioParaTest1.Id));
    }
    
    [TestMethod]
    public void AgregarUnTipoDeCambioLoDevuelveCorrecto()
    {
        _repositorioDeTipoDeCambioParaTest.Agregar(_tipoDeCambioParaTest1);
        TipoDeCambio _TipoDeCambioAgregadoAlRepoParaTest = _repositorioDeTipoDeCambioParaTest.Agregar(_tipoDeCambioParaTest1);
        Assert.AreEqual(_TipoDeCambioAgregadoAlRepoParaTest, _tipoDeCambioParaTest1);
    }
    
    [TestMethod]
    public void NoEstaAgregadoElUnTipoDeCambioCorrecto()
    {
        Assert.AreEqual(null, _repositorioDeTipoDeCambioParaTest.Encontrar(
            x => x.Id == _tipoDeCambioParaTest1.Id));
    }
    
    [TestMethod]
    public void ListarTodosCorrecto()
    {
        _repositorioDeTipoDeCambioParaTest.Agregar(_tipoDeCambioParaTest1);
        _repositorioDeTipoDeCambioParaTest.Agregar(_tipoDeCambioParaTest2);
        Assert.IsTrue(_repositorioDeTipoDeCambioParaTest.ListarTodos().Contains(_tipoDeCambioParaTest1) 
                      && 
                      _repositorioDeTipoDeCambioParaTest.ListarTodos().Contains(_tipoDeCambioParaTest2));
    }
    
    [TestMethod]
    public void EliminarUnUnTipoDeCambioQueNoExisteDevuelveNull()
    {
        Assert.AreEqual(null, _repositorioDeTipoDeCambioParaTest.Eliminar(_tipoDeCambioParaTest1.Id));
    }
    
    [TestMethod]
    public void EliminarUnUnTipoDeCambioCorrecto()
    {
        _repositorioDeTipoDeCambioParaTest.Agregar(_tipoDeCambioParaTest1);
        Assert.IsTrue(_tipoDeCambioParaTest1.Equals(_repositorioDeTipoDeCambioParaTest.Eliminar(_tipoDeCambioParaTest1.Id))
                      && _repositorioDeTipoDeCambioParaTest.Encontrar(x => x.Id == _tipoDeCambioParaTest1.Id) == null);
    }
    
    [TestMethod]
    public void ActualizarUnTipoDeCambioCorrecto()
    {
        _repositorioDeTipoDeCambioParaTest.Agregar(_tipoDeCambioParaTest1);
        _repositorioDeTipoDeCambioParaTest.Actualizar(_tipoDeCambioParaTest2);
        Assert.AreEqual(_tipoDeCambioParaTest2.Espacio, _tipoDeCambioParaTest1.Espacio);
        Assert.AreEqual(_tipoDeCambioParaTest2.Fecha, _tipoDeCambioParaTest1.Fecha);
        Assert.AreEqual(_tipoDeCambioParaTest2.ValorDelDolar, _tipoDeCambioParaTest1.ValorDelDolar);
    }
}