using Dominio;

namespace Logica;

public class ReportesTarjetasDeCreditoLogica : ReportesLogica
{
    public IList<Transaccion> GastosDeUnaTarjetaDelUltimoMes(TransaccionLogica transaccionLogica,
        EspacioLogica espacioLogica,
        TarjetaDeCredito unaTarjetaDeCredito)
    {
        ReportesLogica.RangoFechas rangoTarjeta = RengoFechasAlMesAnterior(unaTarjetaDeCredito.FechaDeCierre);
        List<Transaccion> transaccionesAMostrar =
            transaccionLogica.ListarCostosDeUnEspacio(espacioLogica.EspacioActual()).ToList();

        transaccionesAMostrar = FiltrarCostosPorCuenta(transaccionesAMostrar, unaTarjetaDeCredito);
        transaccionesAMostrar = FiltrarCostosPorRangoDeFechas(transaccionesAMostrar,
            rangoTarjeta);

        return transaccionesAMostrar;
    }
    
    private RangoFechas RengoFechasAlMesAnterior(DateTime fecha)
    {
        DateTime fechaFin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, fecha.Day);
        DateTime fechaInicio = new DateTime(DateTime.Now.Year,
            DateTime.Now.AddMonths(-1).Month, fecha.AddDays(+1).Day);

        RangoFechas rango = new RangoFechas();
        rango.FechaDesde = fechaInicio;
        rango.FechaHasta = fechaFin;

        return rango;
    }
}