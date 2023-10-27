using System.Transactions;
using Dominio;

namespace Logica;

public static class ReportesLogica
{
    public struct RangoFechas
    {
        public DateTime? FechaDesde;
        public DateTime? FechaHasta;
    }

    public static List<Transaccion> ReporteListadoDeGastos(
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

    public struct DatosPorCategoria
    {
        public float? Total;
        public float? SumaDeGastos;
        public float? Porcentaje;
        public Categoria Categoria;

        public DatosPorCategoria()
        {
            Total = 0;
            Porcentaje = 0;
            Categoria = null;
            SumaDeGastos = 0;
        }
    }

    public static List<DatosPorCategoria> ReportePorCategoria(
        TransaccionLogica transaccionLogica, Espacio espacioActual,
        DateTime mes, TipoDeCambioLogica tipoDeCambioLogica)
    {
        List<DatosPorCategoria> reportes = new List<DatosPorCategoria>();

        List<Transaccion> costos = transaccionLogica.ListarCostosDeUnEspacio(espacioActual).ToList();
        List<Transaccion> costosDelMes = TransaccionesDeUnMes(costos, mes);

        LlenarReportesPorCategoriaCostos(reportes, costosDelMes);

        for (int iReporte = 0; iReporte < reportes.Count; iReporte++)
        {
            foreach (Transaccion transaccion in costosDelMes)
            {
                if (reportes[iReporte].Categoria.Equals(transaccion.Categoria))
                {
                    float? monto = ConvertirAPesosUruguayos(transaccion, tipoDeCambioLogica);
                    DatosPorCategoria copiaDatos = reportes[iReporte];
                    copiaDatos.SumaDeGastos += monto;
                    reportes[iReporte] = copiaDatos;
                }
            }
        }

        LlenarTotalDatosCategoria(reportes, CalcularTotalDatosCategoria(reportes));
        CalcularPorcentajeDatosCategoria(reportes);

        return reportes;
    }

    public static float? ConvertirAPesosUruguayos(Transaccion transaccion, TipoDeCambioLogica tipoDeCambioLogica)
    {
        float? montoConvertido = transaccion.Monto;

        if (transaccion.Moneda == "US$")
        {
            bool buscar = true;
            List<TipoDeCambio> tiposDeCambios =
                tipoDeCambioLogica.ListarTiposDeCambioDeUnEspacio(transaccion.Espacio).ToList();

            for (int i = 0; i < tiposDeCambios.Count && buscar; i++)
            {
                if (transaccion.Fecha.ToShortDateString() == tiposDeCambios[i].Fecha.ToShortDateString())
                {
                    montoConvertido = transaccion.Monto * tiposDeCambios[i].ValorDelDolar;
                    buscar = false;
                }
            }
        }

        return montoConvertido;
    }

    public struct DatosPorObjetivoDeGasto
    {
        public float? MontoDefinido;
        public float? MontoGastado;
        public ObjetivoDeGasto ObjetivoDeGasto;
        public bool estaObjetivoDeGastoCumplido;

        public DatosPorObjetivoDeGasto()
        {
            MontoDefinido = 0;
            MontoGastado = 0;
            ObjetivoDeGasto = null;
            estaObjetivoDeGastoCumplido = false;
        }
    }

    public static List<DatosPorObjetivoDeGasto> ReportePorObjetivoDeGasto(
        TransaccionLogica transaccionLogica, ObjetivoDeGastoLogica objetivoDeGastoLogica,
        Espacio espacioActual, DateTime mes, TipoDeCambioLogica tipoDeCambioLogica)
    {
        List<DatosPorObjetivoDeGasto> reportes = new List<DatosPorObjetivoDeGasto>();

        IList<ObjetivoDeGasto> objetivosDeGasto =
            objetivoDeGastoLogica.ListarObjetivosDeGastosDeUnEspacio(espacioActual);

        List<Transaccion> costos =
            transaccionLogica.ListarCostosDeUnEspacio(espacioActual).ToList();

        List<Transaccion> TransaccionesDeCostoDeUnMes =
            TransaccionesDeUnMes(costos, mes);

        LlenarReportesPorObjetivoDeGasto(reportes, objetivosDeGasto);
        LlenarReportesConCostosDeCadaObjetivoDeGasto(reportes, TransaccionesDeCostoDeUnMes,
            tipoDeCambioLogica);
        LlenarReportesSiObjetivoDeGastoEstaCumplidoONo(reportes);

        return reportes;
    }

    public static IList<Transaccion> GastosDeUnaTarjetaDelUltimoMes(TransaccionLogica transaccionLogica, EspacioLogica espacioLogica,
        TarjetaDeCredito unaTarjetaDeCredito)
    {
        RangoFechas rangoTarjeta = RengoFechasAlMesAnterior(unaTarjetaDeCredito.FechaDeCierre);
        List<Transaccion> transaccionesAMostrar = transaccionLogica.ListarCostosDeUnEspacio(espacioLogica.EspacioActual).ToList();

        transaccionesAMostrar = FiltrarCostosPorCuenta(transaccionesAMostrar, unaTarjetaDeCredito);
        transaccionesAMostrar = FiltrarCostosPorRangoDeFechas(transaccionesAMostrar,
            rangoTarjeta);

        return transaccionesAMostrar;
    }

    public static IList<Transaccion> MovimientosDeUnaCuentaMonetaria(
        TransaccionLogica transaccionLogica, EspacioLogica espacioLogica,
        Monetaria cuentaMonetaria)
    {
        IList<Transaccion> transaccionesAMostrar = new List<Transaccion>();
        IList<Transaccion> transaccionesDeUnEspacio =
            transaccionLogica.ListarTransaccionesDeUnEspacio(espacioLogica.EspacioActual);

        foreach (Transaccion transaccion in transaccionesDeUnEspacio)
        {
            if (transaccion.Cuenta.Equals(cuentaMonetaria))
            {
                transaccionesAMostrar.Add(transaccion);
            }
        }

        return transaccionesAMostrar;
    }
    
     private static List<Transaccion> FiltrarCostosPorCategoria(
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

    private static List<Transaccion> FiltrarCostosPorCuenta(
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

    private static List<Transaccion> FiltrarCostosPorRangoDeFechas(List<Transaccion> costos,
        RangoFechas rangoDeFechas)
    {
        if ((rangoDeFechas.FechaDesde == null && rangoDeFechas.FechaHasta != null)
            || (rangoDeFechas.FechaDesde != null && rangoDeFechas.FechaHasta == null))
            throw new LogicaExcepcion("El rango de fechas debe estar " +
                                      "vacío o ambos campos llenos.");

        if (rangoDeFechas.FechaDesde > rangoDeFechas.FechaHasta)
            throw new LogicaExcepcion("La fecha de inicio debe ser menor " +
                                      "o igual a la fecha de fin.");

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
    
    private static float? CalcularTotalDatosCategoria(List<DatosPorCategoria> reportes)
    {
        float? total = 0;

        foreach (DatosPorCategoria reporteCategoria in reportes)
            total += reporteCategoria.SumaDeGastos;

        return total;
    }

    private static void LlenarTotalDatosCategoria(List<DatosPorCategoria> reportes, float? total)
    {
        for (int iReporte = 0; iReporte < reportes.Count; iReporte++)
        {
            DatosPorCategoria copiaDatos = reportes[iReporte];
            copiaDatos.Total = total;
            reportes[iReporte] = copiaDatos;
        }
    }

    private static void CalcularPorcentajeDatosCategoria(List<DatosPorCategoria> reportes)
    {
        for (int iReporte = 0; iReporte < reportes.Count; iReporte++)
        {
            DatosPorCategoria copiaDatos = reportes[iReporte];
            copiaDatos.Porcentaje = (copiaDatos.SumaDeGastos * 100) / copiaDatos.Total;
            reportes[iReporte] = copiaDatos;
        }
    }

    private static void LlenarReportesPorCategoriaCostos(
        List<DatosPorCategoria> reportes, List<Transaccion> costos)
    {
        foreach (Transaccion transaccion in costos)
        {
            DatosPorCategoria datos = new DatosPorCategoria();
            if (!ReportesContieneCategoria(reportes, transaccion.Categoria))
            {
                datos.Categoria = transaccion.Categoria;
                reportes.Add(datos);
            }
        }
    }

    private static bool ReportesContieneCategoria(
        List<DatosPorCategoria> reportes, Categoria categoriaABuscar)
    {
        bool contiene = false;

        for (int i = 0; i < reportes.Count && !contiene; i++)
        {
            if (reportes[i].Categoria != null &&
                reportes[i].Categoria.Equals(categoriaABuscar))
            {
                contiene = true;
            }
        }

        return contiene;
    }

    private static List<Transaccion> TransaccionesDeUnMes(List<Transaccion> transacciones, DateTime mes)
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
    
    private static void LlenarReportesPorObjetivoDeGasto(List<DatosPorObjetivoDeGasto> reportes,
        IList<ObjetivoDeGasto> objetivosDeGasto)
    {
        foreach (ObjetivoDeGasto objetivoDeGasto in objetivosDeGasto)
        {
            DatosPorObjetivoDeGasto datos = new DatosPorObjetivoDeGasto();
            datos.ObjetivoDeGasto = objetivoDeGasto;
            datos.MontoDefinido = objetivoDeGasto.MontoMaximo;
            reportes.Add(datos);
        }
    }

    private static void LlenarReportesConCostosDeCadaObjetivoDeGasto(List<DatosPorObjetivoDeGasto> reportes,
        List<Transaccion> TransaccionesDeCostoDeUnMes, TipoDeCambioLogica tipoDeCambioLogica)
    {
        foreach (Transaccion transaccion in TransaccionesDeCostoDeUnMes)
        {
            for (int iReporte = 0; iReporte < reportes.Count; iReporte++)
            {
                if (reportes[iReporte].ObjetivoDeGasto.Categorias.Contains
                        (transaccion.Categoria))
                {
                    float? Monto = transaccion.Monto;

                    if (transaccion.Moneda == "US$")
                    {
                        Monto = ConvertirAPesosUruguayos(transaccion,
                            tipoDeCambioLogica);
                    }

                    DatosPorObjetivoDeGasto copiaDatos = reportes[iReporte];
                    copiaDatos.MontoGastado += Monto;
                    reportes[iReporte] = copiaDatos;
                }
            }
        }
    }

    private static void LlenarReportesSiObjetivoDeGastoEstaCumplidoONo(
        List<DatosPorObjetivoDeGasto> reportes)
    {
        for (int iReporte = 0; iReporte < reportes.Count; iReporte++)
        {
            if (reportes[iReporte].MontoDefinido >= reportes[iReporte].MontoGastado)
            {
                DatosPorObjetivoDeGasto copiaDatos = reportes[iReporte];
                copiaDatos.estaObjetivoDeGastoCumplido = true;
                reportes[iReporte] = copiaDatos;
            }
        }
    }

    private static RangoFechas RengoFechasAlMesAnterior(DateTime fecha)
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