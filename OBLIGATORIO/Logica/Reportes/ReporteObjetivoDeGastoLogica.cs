using Dominio;
using Dominio.Constantes;

namespace Logica;

public class ReporteObjetivoDeGastoLogica : ReportesLogica
{
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

    public List<DatosPorObjetivoDeGasto> ReportePorObjetivoDeGasto(
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

    private void LlenarReportesPorObjetivoDeGasto(List<DatosPorObjetivoDeGasto> reportes,
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

    private void LlenarReportesConCostosDeCadaObjetivoDeGasto(List<DatosPorObjetivoDeGasto> reportes,
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

                    if (transaccion.Moneda.Nombre != ConstantesMoneda.PesoUruguayo)
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

    private void LlenarReportesSiObjetivoDeGastoEstaCumplidoONo(
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
}