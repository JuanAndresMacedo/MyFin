using Dominio;
using Dominio.Constantes;
using Logica;
using LogicaTest.Contexto;
using Memoria;
using Memoria.BaseDeDatosRepositorios;

namespace LogicaTest;

[TestClass]
public class ObjetivoDeGastoLogicaTest
{
    private SQLContexto _contexto;
    private ObjetivoDeGastoLogica _objetivoDeGastoLogicaParaTest;
    private ObjetivoDeGastoBDRepositorio _objetivoDeGastoRepositorioParaTest;
    private TransaccionLogica _transaccionLogicaParaTest;
    private TransaccionBDRepositorio _transaccionRepositorioParaTest;
    private TipoDeCambioLogica _tipoDeCambioLogicaParaTest;
    private TipoDeCambioBDRepositorio _tipoDeCambioRepositorioParaTest;
    
    private Transaccion _transaccionParaTest1;
    private Transaccion _transaccionParaTest2;
    private Transaccion _transaccionParaTest3;
    
    private TipoDeCambio _tipoDeCambioParaTest1;
    
    private Monetaria _monetariaParaTest1;
    private Monetaria _monetariaParaTest2;
    
    private ObjetivoDeGasto _objetivoDeGastoParaTest1;
    private ObjetivoDeGasto _objetivoDeGastoParaTest2;
    private ObjetivoDeGasto _objetivoDeGastoParaTest3;
    
    private Usuario _usuarioParaTest;
    
    private Espacio _espacioParaTest1;
    private Espacio _espacioParaTest2;
    
    private Categoria _categoriaParaTest1;
    private Categoria _categoriaParaTest2;
    private Categoria _categoriaParaTest3;
    
    private Moneda _monedaParaTest1;
    private Moneda _monedaParaTest2;
    
    private DateTime _fechaParaTest;

    [TestInitialize]
    public void Inicio()
    {
        FabricaSQLContexto fabricaSQLContext = new FabricaSQLContexto();
        _contexto = fabricaSQLContext.CrearContextoMemoria();
        
        _objetivoDeGastoRepositorioParaTest = new ObjetivoDeGastoBDRepositorio(_contexto);
        _objetivoDeGastoLogicaParaTest = new ObjetivoDeGastoLogica(_objetivoDeGastoRepositorioParaTest);
        
        _transaccionRepositorioParaTest = new TransaccionBDRepositorio(_contexto);
        _transaccionLogicaParaTest = new TransaccionLogica(_transaccionRepositorioParaTest);
        
        _tipoDeCambioRepositorioParaTest = new TipoDeCambioBDRepositorio(_contexto);
        _tipoDeCambioLogicaParaTest = new TipoDeCambioLogica(_tipoDeCambioRepositorioParaTest);
        
        _fechaParaTest = DateTime.Now;
        
        _usuarioParaTest = new Usuario()
        {
            Id = 1,
            Correo = "juan@adinet.com",
            Contrasena = "juanBall12345",
            Nombre = "Juan",
            Apellido = "Min",
            Direccion = "Av.China"
        };

        _espacioParaTest1 = new Espacio()
        {
            Id = 1,
            Nombre = "Ropa",
            Administrador = _usuarioParaTest
        };
        
        _espacioParaTest2 = new Espacio()
        {
            Id = 2,
            Nombre = "Familia",
            Administrador = _usuarioParaTest
        };

        _categoriaParaTest1 = new Categoria()
        {
            Id = 1,
            Nombre = "Ropa",
            UsuarioCreador = _usuarioParaTest,
            Espacio = _espacioParaTest1,
            Estatus = ConstantesCategoria.estatusActiva,
            Tipo = ConstantesCategoria.tipoCosto,
        };
        
        _categoriaParaTest2 = new Categoria()
        {
            Id = 2,
            Nombre = "Educacion",
            UsuarioCreador = _usuarioParaTest,
            Espacio = _espacioParaTest1,
            Estatus = ConstantesCategoria.estatusActiva,
            Tipo = ConstantesCategoria.tipoCosto,
        };
        
        _categoriaParaTest3 = new Categoria()
        {
            Id = 3,
            Nombre = "Supermercado",
            UsuarioCreador = _usuarioParaTest,
            Espacio = _espacioParaTest1,
            Estatus = ConstantesCategoria.estatusActiva,
            Tipo = ConstantesCategoria.tipoIngreso,
        };
        
        _objetivoDeGastoParaTest1 = new ObjetivoDeGasto()
        {
            Titulo = "Comida",
            MontoMaximo = 30000,
            UsuarioCreador = _usuarioParaTest,
            Espacio = _espacioParaTest1,
        };

        _objetivoDeGastoParaTest2 = new ObjetivoDeGasto()
        {
            Titulo = "Deporte",
            MontoMaximo = 30000,
            UsuarioCreador = _usuarioParaTest,
            Espacio = _espacioParaTest1
        };
        
        _objetivoDeGastoParaTest3 = new ObjetivoDeGasto()
        {
            Titulo = "Deporte",
            MontoMaximo = 30000,
            UsuarioCreador = _usuarioParaTest,
            Espacio = _espacioParaTest2
        };
        
        
        _monedaParaTest1 = new Moneda()
        {
            Id = 1,
            Nombre = ConstantesMoneda.PesoUruguayo,
            SimboloMonetario = "UYU"
        };

        _monedaParaTest2 = new Moneda()
        {
            Id = 2,
            Nombre = ConstantesMoneda.Dolar,
            SimboloMonetario = "U$S"
        };
        
        _tipoDeCambioParaTest1 = new TipoDeCambio()
        {
            Id = 1,
            Moneda = _monedaParaTest2,
            UsuarioCreador = _usuarioParaTest,
            Espacio = _espacioParaTest1,
            Fecha = _fechaParaTest,
            ValorDeLaMoneda = 10f
        };
        
        _monetariaParaTest1 = new Monetaria()
        {
            Id = 1,
            Nombre = "Ahorros",
            FechaDeCreacion = _fechaParaTest,
            Moneda = _monedaParaTest1,
            Monto = 3000f,
            Propietario = _usuarioParaTest
        };
        
        _monetariaParaTest2 = new Monetaria()
        {
            Id = 2,
            Nombre = "Hipoteca",
            FechaDeCreacion = _fechaParaTest,
            Moneda = _monedaParaTest2,
            Monto = 2000f,
            Propietario = _usuarioParaTest
        };
        
        _transaccionParaTest1 = new Transaccion()
        {
            Nombre = "Pago",
            Fecha = _fechaParaTest,
            Monto = 100f,
            Moneda = _monedaParaTest1,
            Categoria = _categoriaParaTest1,
            Cuenta = _monetariaParaTest1,
            Espacio = _espacioParaTest1,
            Tipo = ConstantesCategoria.tipoCosto
        };
        
        _transaccionParaTest2 = new Transaccion()
        {
            Nombre = "Cobro",
            Fecha = _fechaParaTest,
            Monto = 13f,
            Moneda = _monedaParaTest2,
            Categoria = _categoriaParaTest1,
            Cuenta = _monetariaParaTest2,
            Espacio = _espacioParaTest1,
            Tipo = ConstantesCategoria.tipoCosto
        };
        
        _transaccionParaTest3 = new Transaccion()
        {
            Nombre = "Cobro",
            Fecha = _fechaParaTest,
            Monto = 256f,
            Moneda = _monedaParaTest2,
            Categoria = _categoriaParaTest3,
            Cuenta = _monetariaParaTest1,
            Espacio = _espacioParaTest1,
            Tipo = ConstantesCategoria.tipoIngreso
        };
        
        _objetivoDeGastoParaTest1.AgregarCategoria(_categoriaParaTest1);
        _objetivoDeGastoParaTest2.AgregarCategoria(_categoriaParaTest2);
        _objetivoDeGastoParaTest3.AgregarCategoria(_categoriaParaTest3);
    }
    
    [TestCleanup]
    public void Fin()
    {
        _contexto.Database.EnsureDeleted();
    }

    [TestMethod]
    public void AgregarObjetivoDeGastoCorrecto()
    {
        ObjetivoDeGasto agregado = _objetivoDeGastoLogicaParaTest.AgregarObjetivoDeGasto
            (_objetivoDeGastoParaTest1);

        Assert.AreEqual(_objetivoDeGastoParaTest1.Titulo, agregado.Titulo);
        Assert.AreEqual(_objetivoDeGastoParaTest1.MontoMaximo, agregado.MontoMaximo);
        Assert.AreEqual(_objetivoDeGastoParaTest1.Categorias, agregado.Categorias);
        Assert.AreEqual(_objetivoDeGastoParaTest1.UsuarioCreador, agregado.UsuarioCreador);
    }

    [TestMethod]
    [ExpectedException(typeof(LogicaExcepcion))]
    public void AgregarDosObjetivosDeGastoIgualesIncorrecto()
    {
        _objetivoDeGastoParaTest2.Titulo = "Comida";
        _objetivoDeGastoLogicaParaTest.AgregarObjetivoDeGasto(_objetivoDeGastoParaTest1);
        _objetivoDeGastoLogicaParaTest.AgregarObjetivoDeGasto(_objetivoDeGastoParaTest2);
    }

    [TestMethod]
    public void AgregarDosObjetivosDeGastoCorrecto()
    {
        _objetivoDeGastoLogicaParaTest.AgregarObjetivoDeGasto(_objetivoDeGastoParaTest1);
        _objetivoDeGastoLogicaParaTest.AgregarObjetivoDeGasto(_objetivoDeGastoParaTest2);
    }

    [TestMethod]
    public void ListarTodosLosObjetivosDeGastoCorrecto()
    {
        ObjetivoDeGasto retornoObjetivoDeGasto1 =
            _objetivoDeGastoLogicaParaTest.AgregarObjetivoDeGasto(_objetivoDeGastoParaTest1);
        ;
        ObjetivoDeGasto retornoObjetivoDeGasto2 =
            _objetivoDeGastoLogicaParaTest.AgregarObjetivoDeGasto(_objetivoDeGastoParaTest2);
        ;

        IList<ObjetivoDeGasto> objetivosDeGastoPrueba = _objetivoDeGastoLogicaParaTest.ListarObjetivosDeGasto();

        Assert.IsTrue(objetivosDeGastoPrueba.Contains(retornoObjetivoDeGasto1));
        Assert.IsTrue(objetivosDeGastoPrueba.Contains(retornoObjetivoDeGasto2));
    }

    [TestMethod]
    public void EncontrarObjetivoDeGastoCorrecto()
    {
        ObjetivoDeGasto agregado = _objetivoDeGastoLogicaParaTest.AgregarObjetivoDeGasto(_objetivoDeGastoParaTest1);
        Assert.AreEqual(agregado, _objetivoDeGastoLogicaParaTest
            .EncontrarObjetivoDeGasto(_objetivoDeGastoParaTest1.Id));
    }

    [TestMethod]
    public void EncontrarObjetivoDeGastoNoEstaDevuelveNuloCorrecto()
    {
        Assert.AreEqual(null,
            _objetivoDeGastoLogicaParaTest.EncontrarObjetivoDeGasto(_objetivoDeGastoParaTest1.Id));
    }
    
    [TestMethod]
    public void ListarCategoriasDeUnEspacioCorrecto()
    {
        ObjetivoDeGasto retornoObjetivoDeGasto1 = 
            _objetivoDeGastoLogicaParaTest.AgregarObjetivoDeGasto(_objetivoDeGastoParaTest1);
        ObjetivoDeGasto retornoObjetivoDeGasto2 = 
            _objetivoDeGastoLogicaParaTest.AgregarObjetivoDeGasto(_objetivoDeGastoParaTest2);
        ObjetivoDeGasto retornoObjetivoDeGasto3 = 
            _objetivoDeGastoLogicaParaTest.AgregarObjetivoDeGasto(_objetivoDeGastoParaTest3);

        List<ObjetivoDeGasto> objetivosDeGastoPrueba = 
            _objetivoDeGastoLogicaParaTest.ListarObjetivosDeGastosDeUnEspacio(_espacioParaTest1);

        Assert.IsTrue(objetivosDeGastoPrueba.Contains(retornoObjetivoDeGasto1));
        Assert.IsTrue(objetivosDeGastoPrueba.Contains(retornoObjetivoDeGasto2));
        Assert.IsFalse(objetivosDeGastoPrueba.Contains(retornoObjetivoDeGasto3));     
    }
    
    [TestMethod]
    [ExpectedException(typeof(LogicaExcepcion))]
    public void ObjetivoDeGastoSinCategoriasIncorrecto()
    {
        _objetivoDeGastoParaTest1.Categorias = new List<Categoria>();
        ObjetivoDeGasto retornoCategoria1 = 
            _objetivoDeGastoLogicaParaTest.AgregarObjetivoDeGasto(_objetivoDeGastoParaTest1);
    }
    
    [TestMethod]
    public void CompartirObjetivoDeGastoCorrecto()
    {
        _objetivoDeGastoLogicaParaTest.AgregarObjetivoDeGasto(_objetivoDeGastoParaTest1);
        Assert.AreEqual(null,_objetivoDeGastoParaTest1.Token);
        _objetivoDeGastoLogicaParaTest.CompartirUnObjetivo(1);
        Assert.IsTrue(_objetivoDeGastoParaTest1.Token != null);
    }
    
    [TestMethod]
    public void DejarDeCompartirObjetivoCorrecto()
    {
        _objetivoDeGastoLogicaParaTest.AgregarObjetivoDeGasto(_objetivoDeGastoParaTest1);
        _objetivoDeGastoLogicaParaTest.CompartirUnObjetivo(1);
        _objetivoDeGastoLogicaParaTest.DejarDeCompartirObjetivo(1);
        Assert.AreEqual(null,_objetivoDeGastoParaTest1.Token);
    }

    [TestMethod]
    public void GastadoActualmenteEnObjetivoSolo1TransacionCorrecto()
    {
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest1);
        _tipoDeCambioLogicaParaTest.AgregarTipoDeCambio(_tipoDeCambioParaTest1);
        _objetivoDeGastoLogicaParaTest.AgregarObjetivoDeGasto(_objetivoDeGastoParaTest1);
        float? gastado = _objetivoDeGastoLogicaParaTest.GastadoActualmenteEnObjetivo(
            1, _transaccionLogicaParaTest, _tipoDeCambioLogicaParaTest);
        Assert.AreEqual(100,gastado);
    }
    
    [TestMethod]
    public void GastadoActualmenteEnObjetivoSolo1EnDolaresTransacionCorrecto()
    {
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest2);
        _tipoDeCambioLogicaParaTest.AgregarTipoDeCambio(_tipoDeCambioParaTest1);
        _objetivoDeGastoLogicaParaTest.AgregarObjetivoDeGasto(_objetivoDeGastoParaTest1);
        float? gastado = _objetivoDeGastoLogicaParaTest.GastadoActualmenteEnObjetivo(
            1, _transaccionLogicaParaTest, _tipoDeCambioLogicaParaTest);
        Assert.AreEqual(130,gastado);
    }
    
    [TestMethod]
    public void GastadoActualmenteEnObjetivoSoloIngresoCorrecto()
    {
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest3);
        _tipoDeCambioLogicaParaTest.AgregarTipoDeCambio(_tipoDeCambioParaTest1);
        _objetivoDeGastoLogicaParaTest.AgregarObjetivoDeGasto(_objetivoDeGastoParaTest1);
        float? gastado = _objetivoDeGastoLogicaParaTest.GastadoActualmenteEnObjetivo(
            1, _transaccionLogicaParaTest, _tipoDeCambioLogicaParaTest);
        Assert.AreEqual(0,gastado);
    }
}