using Dominio;
using Logica;
using Memoria;

namespace LogicaTest;

[TestClass]
public class ReporteObjetivosDeGastoTest
{
    private TransaccionLogica _transaccionLogicaParaTest;
    private IRepositorio<Transaccion> _transaccionRepositorioParaTest;

    private ObjetivoDeGastoLogica _objetivoDeGastoLogicaParaTest;
    private IRepositorio<ObjetivoDeGasto> _objetivoDeGastoRepositorioParaTest;

    private TipoDeCambioLogica _tipoDeCambioLogicaParaTest;
    private IRepositorio<TipoDeCambio> _tipoDeCambioRepositorioParaTest;

    private EspacioLogica _espacioLogicaParaTest;
    private IRepositorio<Espacio> _espacioRepositorioParaTest;

    private Transaccion _transaccionParaTest1;
    private Transaccion _transaccionParaTest2;
    private Transaccion _transaccionParaTest3;
    private Transaccion _transaccionParaTest4;
    private Transaccion _transaccionParaTest5;
    private Transaccion _transaccionParaTest6;
    private Transaccion _transaccionParaTest7;

    private Monetaria _monetariaParaTest1;
    private Monetaria _monetariaParaTest2;

    private TarjetaDeCredito _tarjetaDeCreditoParaTest1;

    private Categoria _categoriaParaTest1;
    private Categoria _categoriaParaTest2;
    private Categoria _categoriaParaTest3;
    private Categoria _categoriaParaTest4;

    private Espacio _espacioParaTest1;
    private Espacio _espacioParaTest2;

    private Usuario _usuarioParaTest1;
    private Usuario _usuarioParaTest2;

    private DateTime _fechaParaTest1;
    private DateTime _fechaParaTest2;
    private DateTime _fechaParaTest3;

    private ObjetivoDeGasto _objetivoDeGastoParaTest1;
    private ObjetivoDeGasto _objetivoDeGastoParaTest2;
    private ObjetivoDeGasto _objetivoDeGastoParaTest3;
    
    private TipoDeCambio _tipoDeCambioParaTest1;

    [TestInitialize]
    public void Inicio()
    {
        _transaccionRepositorioParaTest = new TransaccionMemoriaRepositorio();
        _transaccionLogicaParaTest = new TransaccionLogica(_transaccionRepositorioParaTest);

        _objetivoDeGastoRepositorioParaTest = new ObjetivoDeGastoMemoriaRepositorio();
        _objetivoDeGastoLogicaParaTest = new ObjetivoDeGastoLogica(_objetivoDeGastoRepositorioParaTest);

        _tipoDeCambioRepositorioParaTest = new TipoDeCambioMemoriaRepositorio();
        _tipoDeCambioLogicaParaTest = new TipoDeCambioLogica(_tipoDeCambioRepositorioParaTest);

        _espacioRepositorioParaTest = new EspacioMemoriaRepositorio();
        _espacioLogicaParaTest = new EspacioLogica(_espacioRepositorioParaTest);

        _fechaParaTest1 = new DateTime(2023, 10, 8);
        _fechaParaTest2 = new DateTime(2023, 10, 30);
        _fechaParaTest3 = new DateTime(2023, 11, 1);

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

        _espacioParaTest1 = new Espacio()
        {
            Nombre = "Amigos",
            Administrador = _usuarioParaTest1,
            Id = 1
        };

        _espacioParaTest2 = new Espacio()
        {
            Nombre = "Familia",
            Administrador = _usuarioParaTest1,
            Id = 2
        };

        _monetariaParaTest1 = new Monetaria()
        {
            Nombre = "Ahorros",
            FechaDeCreacion = _fechaParaTest1,
            Moneda = "UYU",
            Monto = 2344445f,
            Propietario = _usuarioParaTest1,
            Espacio = _espacioParaTest1
        };

        _monetariaParaTest2 = new Monetaria()
        {
            Nombre = "Ahorros2",
            FechaDeCreacion = _fechaParaTest1,
            Moneda = "UYU",
            Monto = 2344445f,
            Propietario = _usuarioParaTest2,
            Espacio = _espacioParaTest1
        };

        _tarjetaDeCreditoParaTest1 = new TarjetaDeCredito()
        {
            Nombre = "Santander Credito",
            Id = 350,
            Propietario = _usuarioParaTest1,
            Espacio = _espacioParaTest1,
            Moneda = "US$",
            FechaDeCreacion = _fechaParaTest1,
            FechaDeCierre = _fechaParaTest3.AddDays(2),
            BancoEmisor = "Santander",
            UltimosCuatroDigitos = "3455",
            CreditoDisponible = 2344445f
        };

        _categoriaParaTest1 = new Categoria()
        {
            Espacio = _espacioParaTest1,
            UsuarioCreador = _usuarioParaTest1,
            Nombre = "Ropa",
            FechaDeCreacion = _fechaParaTest1,
            Tipo = "Costo",
            Estatus = "Activa",
        };

        _categoriaParaTest2 = new Categoria()
        {
            Espacio = _espacioParaTest1,
            UsuarioCreador = _usuarioParaTest1,
            Nombre = "Comida",
            FechaDeCreacion = _fechaParaTest1,
            Tipo = "Costo",
            Estatus = "Activa",
        };

        _categoriaParaTest3 = new Categoria()
        {
            Espacio = _espacioParaTest1,
            UsuarioCreador = _usuarioParaTest1,
            Nombre = "Combustible",
            FechaDeCreacion = _fechaParaTest2,
            Tipo = "Costo",
            Estatus = "Activa",
        };
        
        _categoriaParaTest4 = new Categoria()
        {
            Espacio = _espacioParaTest1,
            UsuarioCreador = _usuarioParaTest1,
            Nombre = "Ropa2",
            FechaDeCreacion = _fechaParaTest3,
            Tipo = "Costo",
            Estatus = "Inactiva",
        };

        _objetivoDeGastoParaTest1 = new ObjetivoDeGasto()
        {
            Titulo = "Menos Ropa",
            Espacio = _espacioParaTest1,
            MontoMaximo = 500f,
        };

        _objetivoDeGastoParaTest2 = new ObjetivoDeGasto()
        {
            Titulo = "Menos Salidas",
            Espacio = _espacioParaTest1,
            MontoMaximo = 3000f,
        };

        _transaccionParaTest1 = new Transaccion()
        {
            Nombre = "Pago GAP",
            Fecha = _fechaParaTest2,
            Monto = 10f,
            Moneda = "UYU",
            Categoria = _categoriaParaTest1,
            Cuenta = _monetariaParaTest2,
            Espacio = _espacioParaTest1,
            Tipo = "Costo"
        };

        _transaccionParaTest2 = new Transaccion()
        {
            Nombre = "Championes",
            Fecha = _fechaParaTest1,
            Monto = 150f,
            Moneda = "UYU",
            Categoria = _categoriaParaTest1,
            Cuenta = _monetariaParaTest1,
            Espacio = _espacioParaTest1,
            Tipo = "Costo"
        };

        _transaccionParaTest3 = new Transaccion()
        {
            Nombre = "Fanta",
            Fecha = _fechaParaTest2,
            Monto = 80f,
            Moneda = "UYU",
            Categoria = _categoriaParaTest2,
            Cuenta = _monetariaParaTest1,
            Espacio = _espacioParaTest1,
            Tipo = "Costo"
        };

        _transaccionParaTest4 = new Transaccion()
        {
            Nombre = "Tanque lleno",
            Fecha = _fechaParaTest1,
            Monto = 1000f,
            Moneda = "UYU",
            Categoria = _categoriaParaTest3,
            Cuenta = _monetariaParaTest1,
            Espacio = _espacioParaTest1,
            Tipo = "Costo"
        };
        
        _transaccionParaTest5 = new Transaccion()
        {
            Nombre = "Championes Nike",
            Fecha = _fechaParaTest1,
            Monto = 15f,
            Moneda = "US$",
            Categoria = _categoriaParaTest1,
            Cuenta = _tarjetaDeCreditoParaTest1,
            Espacio = _espacioParaTest1,
            Tipo = "Costo"
        };
        
        _transaccionParaTest6 = new Transaccion()
        {
            Nombre = "Bufanda",
            Fecha = _fechaParaTest1,
            Monto = 20f,
            Moneda = "UYU",
            Categoria = _categoriaParaTest1,
            Cuenta = _monetariaParaTest1,
            Espacio = _espacioParaTest2,
            Tipo = "Costo"
        };
        
                
        _transaccionParaTest7 = new Transaccion()
        {
            Nombre = "Gas oil",
            Fecha = _fechaParaTest3,
            Monto = 20f,
            Moneda = "UYU",
            Categoria = _categoriaParaTest1,
            Cuenta = _monetariaParaTest1,
            Espacio = _espacioParaTest2,
            Tipo = "Costo"
        };

        _tipoDeCambioParaTest1 = new TipoDeCambio()
        {
            UsuarioCreador = _usuarioParaTest1,
            Espacio = _espacioParaTest1,
            Fecha = _fechaParaTest1,
            ValorDelDolar = 40f
        };

        _tipoDeCambioLogicaParaTest.AgregarTipoDeCambio(_tipoDeCambioParaTest1);

        _espacioLogicaParaTest.AgregarEspacio(_espacioParaTest1);

        _objetivoDeGastoParaTest1.AgregarCategoria(_categoriaParaTest1);
        _objetivoDeGastoParaTest1.AgregarCategoria(_categoriaParaTest4);
        
        _objetivoDeGastoParaTest2.AgregarCategoria(_categoriaParaTest2);
        _objetivoDeGastoParaTest2.AgregarCategoria(_categoriaParaTest3);
        
        _objetivoDeGastoLogicaParaTest.AgregarObjetivoDeGasto(_objetivoDeGastoParaTest1);
        _objetivoDeGastoLogicaParaTest.AgregarObjetivoDeGasto(_objetivoDeGastoParaTest2);

        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest1);
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest2);
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest3);
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest4);
    }

    [TestMethod]
    public void ReportePorObjetivoDeGastoUnaCategoriaCorrecto()
    {
        List<ReportesLogica.DatosPorObjetivoDeGasto> _reportePorObjetivoDeGasto =
            ReportesLogica.ReportePorObjetivoDeGasto(_transaccionLogicaParaTest,
                _objetivoDeGastoLogicaParaTest, _espacioParaTest1, _fechaParaTest1,
                _tipoDeCambioLogicaParaTest);

        Assert.AreEqual(_reportePorObjetivoDeGasto[0].ObjetivoDeGasto, _objetivoDeGastoParaTest1);
        Assert.AreEqual(_reportePorObjetivoDeGasto[0].MontoDefinido, 500);
        Assert.AreEqual(_reportePorObjetivoDeGasto[0].MontoGastado, 160);
        Assert.IsTrue(_reportePorObjetivoDeGasto[0].estaObjetivoDeGastoCumplido);
    }

    [TestMethod]
    public void ReportePorObjetivoDeGastoDosCategoriasCorrecto()
    {
        List<ReportesLogica.DatosPorObjetivoDeGasto> _reportePorObjetivoDeGasto =
            ReportesLogica.ReportePorObjetivoDeGasto(_transaccionLogicaParaTest,
                _objetivoDeGastoLogicaParaTest, _espacioParaTest1,
                _fechaParaTest2, _tipoDeCambioLogicaParaTest);

        Assert.AreEqual(_reportePorObjetivoDeGasto[1].ObjetivoDeGasto, _objetivoDeGastoParaTest2);
        Assert.AreEqual(_reportePorObjetivoDeGasto[1].MontoDefinido, 3000);
        Assert.AreEqual(_reportePorObjetivoDeGasto[1].MontoGastado, 1080);
        Assert.IsTrue(_reportePorObjetivoDeGasto[1].estaObjetivoDeGastoCumplido);
    }

    [TestMethod]
    public void ReportePorObjetivoDeGastoConTransaccionesEnDolaresCorrecto()
    {
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest5);
        
        List<ReportesLogica.DatosPorObjetivoDeGasto> _reportePorObjetivoDeGasto =
            ReportesLogica.ReportePorObjetivoDeGasto(_transaccionLogicaParaTest,
                _objetivoDeGastoLogicaParaTest, _espacioParaTest1, _fechaParaTest1,
                _tipoDeCambioLogicaParaTest);

        Assert.AreEqual(_reportePorObjetivoDeGasto[0].ObjetivoDeGasto, _objetivoDeGastoParaTest1);
        Assert.AreEqual(_reportePorObjetivoDeGasto[0].MontoDefinido, 500);
        Assert.AreEqual(_reportePorObjetivoDeGasto[0].MontoGastado, 760);
        Assert.IsFalse(_reportePorObjetivoDeGasto[0].estaObjetivoDeGastoCumplido);
    }
    
    [TestMethod]
    public void ReportePorObjetivoDeGastoConTransaccionEnOtroEspaciosCorrecto()
    {
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest6);
        
        List<ReportesLogica.DatosPorObjetivoDeGasto> _reportePorObjetivoDeGasto =
            ReportesLogica.ReportePorObjetivoDeGasto(_transaccionLogicaParaTest,
                _objetivoDeGastoLogicaParaTest, _espacioParaTest1, _fechaParaTest1,
                _tipoDeCambioLogicaParaTest);

        Assert.AreEqual(_reportePorObjetivoDeGasto[0].ObjetivoDeGasto, _objetivoDeGastoParaTest1);
        Assert.AreEqual(_reportePorObjetivoDeGasto[0].MontoDefinido, 500);
        Assert.AreEqual(_reportePorObjetivoDeGasto[0].MontoGastado, 160);
        Assert.IsTrue(_reportePorObjetivoDeGasto[0].estaObjetivoDeGastoCumplido);
    }
    
    [TestMethod]
    public void ReportePorObjetivoDeGastoConTransaccionDeOtroMesCorrecto()
    {
        _transaccionLogicaParaTest.AgregarTransaccion(_transaccionParaTest7);
        
        List<ReportesLogica.DatosPorObjetivoDeGasto> _reportePorObjetivoDeGasto =
            ReportesLogica.ReportePorObjetivoDeGasto(_transaccionLogicaParaTest,
                _objetivoDeGastoLogicaParaTest, _espacioParaTest1, _fechaParaTest1,
                _tipoDeCambioLogicaParaTest);

        Assert.AreEqual(_reportePorObjetivoDeGasto[1].ObjetivoDeGasto, _objetivoDeGastoParaTest2);
        Assert.AreEqual(_reportePorObjetivoDeGasto[1].MontoDefinido, 3000);
        Assert.AreEqual(_reportePorObjetivoDeGasto[1].MontoGastado, 1080);
        Assert.IsTrue(_reportePorObjetivoDeGasto[1].estaObjetivoDeGastoCumplido);
    }
}

