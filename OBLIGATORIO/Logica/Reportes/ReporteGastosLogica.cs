using Dominio;

namespace Logica;

public class ReporteGastosLogica : ReportesLogica
{
    public List<Transaccion> ReporteListadoDeGastos(
        TransaccionLogica transaccionLogica, Espacio espacioActual,
        Categoria? categoriaAFiltrar, Cuenta cuentaAFiltrar,
        RangoFechas rangoDeFechas)
    {
        List<Transaccion> costos = transaccionLogica.ListarCostosDeUnEspacio(espacioActual).ToList();
        costos = FiltrarCostosPorCategoria(costos, categoriaAFiltrar);
        costos = FiltrarCostosPorCuenta(costos, cuentaAFiltrar);
        costos = FiltrarCostosPorRangoDeFechas(costos, rangoDeFechas);
        return costos;
    }
    
    private List<Transaccion> FiltrarCostosPorCategoria(
        List<Transaccion> costos, Categoria? categoriaAFiltrar)
    {
        List<Transaccion> transaccionesFiltradasPorCategoria = new List<Transaccion>();

        if (categoriaAFiltrar != null)
        {
            foreach (Transaccion transaccion in costos)
            {
                if (categoriaAFiltrar.Equals(transaccion.Categoria))
                    transaccionesFiltradasPorCategoria.Add(transaccion);
            }
        }
        else
        {
            transaccionesFiltradasPorCategoria = costos;
        }

        return transaccionesFiltradasPorCategoria;
    }
}