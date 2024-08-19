using Dominio;

namespace Logica;

public class ReporteCategoriaLogica : ReportesLogica
{
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

    public List<DatosPorCategoria> ReportePorCategoria(
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
    
    private void LlenarReportesPorCategoriaCostos(
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
    
    private bool ReportesContieneCategoria(
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
    
    private void LlenarTotalDatosCategoria(List<DatosPorCategoria> reportes, float? total)
    {
        for (int iReporte = 0; iReporte < reportes.Count; iReporte++)
        {
            DatosPorCategoria copiaDatos = reportes[iReporte];
            copiaDatos.Total = total;
            reportes[iReporte] = copiaDatos;
        }
    }
    
    private float? CalcularTotalDatosCategoria(List<DatosPorCategoria> reportes)
    {
        float? total = 0;

        foreach (DatosPorCategoria reporteCategoria in reportes)
            total += reporteCategoria.SumaDeGastos;

        return total;
    }
    
    private void CalcularPorcentajeDatosCategoria(List<DatosPorCategoria> reportes)
    {
        for (int iReporte = 0; iReporte < reportes.Count; iReporte++)
        {
            DatosPorCategoria copiaDatos = reportes[iReporte];
            copiaDatos.Porcentaje = (copiaDatos.SumaDeGastos * 100) / copiaDatos.Total;
            reportes[iReporte] = copiaDatos;
        }
    }
}