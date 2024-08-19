using Dominio;
using Dominio.Constantes;

namespace Logica;

public abstract class ReportesLogica
{
    public struct RangoFechas
    {
        public DateTime? FechaDesde;
        public DateTime? FechaHasta;
    }

    public float? ConvertirAPesosUruguayos(Transaccion transaccion, 
        TipoDeCambioLogica tipoDeCambioLogica)
    {
        float? montoConvertido = transaccion.Monto;

        if (transaccion.Moneda.Nombre != ConstantesMoneda.PesoUruguayo)
        {
            bool buscar = true;
            List<TipoDeCambio> tiposDeCambios =
                tipoDeCambioLogica.ListarTiposDeCambioDeUnEspacio(transaccion.Espacio).ToList();

            for (int i = 0; i < tiposDeCambios.Count && buscar; i++)
            {
                if (transaccion.Fecha.ToShortDateString() == tiposDeCambios[i].Fecha.ToShortDateString() &&
                    transaccion.Moneda.Equals(tiposDeCambios[i].Moneda))
                {
                    montoConvertido = transaccion.Monto * tiposDeCambios[i].ValorDeLaMoneda;
                    buscar = false;
                }
            }
        }

        return montoConvertido;
    }
    
    public List<Transaccion> TransaccionesDeUnMes(List<Transaccion> transacciones, DateTime mes)
    {
        List<Transaccion> transaccionsDelMes = new List<Transaccion>();

        foreach (Transaccion transaccion in transacciones)
        {
            if (transaccion.Fecha.Year == mes.Year &&
                transaccion.Fecha.Month == mes.Month)
            {
                transaccionsDelMes.Add(transaccion);
            }
        }

        return transaccionsDelMes;
    }

    public List<Transaccion> FiltrarCostosPorCuenta(
        List<Transaccion> costos, Cuenta? cuentaAFiltrar)
    {
        List<Transaccion> transaccionesFiltradasPorCuenta = new List<Transaccion>();

        if (cuentaAFiltrar != null)
        {
            foreach (Transaccion transaccion in costos)
            {
                if (cuentaAFiltrar.Equals(transaccion.Cuenta))
                {
                    transaccionesFiltradasPorCuenta.Add(transaccion);
                }
            }
        }
        else
        {
            transaccionesFiltradasPorCuenta = costos;
        }

        return transaccionesFiltradasPorCuenta;
    }

    public List<Transaccion> FiltrarCostosPorRangoDeFechas(List<Transaccion> costos,
        RangoFechas rangoDeFechas)
    {
        ValidarRangoFechas(rangoDeFechas);

        List<Transaccion> transaccionesFiltradasPorCuenta = new List<Transaccion>();

        if (rangoDeFechas.FechaDesde != null && rangoDeFechas.FechaHasta != null)
        {
            foreach (Transaccion transaccion in costos)
            {
                if (transaccion.Fecha >= rangoDeFechas.FechaDesde &&
                    transaccion.Fecha <= rangoDeFechas.FechaHasta)
                {
                    transaccionesFiltradasPorCuenta.Add(transaccion);
                }
            }
        }
        else
        {
            transaccionesFiltradasPorCuenta = costos;
        }

        return transaccionesFiltradasPorCuenta;
    }

    private void ValidarRangoFechas(RangoFechas rangoDeFechas)
    {
        if ((rangoDeFechas.FechaDesde == null && rangoDeFechas.FechaHasta != null)
            || (rangoDeFechas.FechaDesde != null && rangoDeFechas.FechaHasta == null))
            throw new LogicaExcepcion("El rango de fechas debe estar " +
                                      "vacío o ambos campos llenos.");

        if (rangoDeFechas.FechaDesde > rangoDeFechas.FechaHasta)
            throw new LogicaExcepcion("La fecha de inicio debe ser menor " +
                                      "o igual a la fecha de fin.");
    }
}