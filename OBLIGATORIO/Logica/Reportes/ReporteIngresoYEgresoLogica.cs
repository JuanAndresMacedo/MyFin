using Dominio;
using Dominio.Constantes;

namespace Logica;

public class ReporteIngresoYEgresoLogica : ReportesLogica
{
    public float?[] SumaIngresosPorDiaDeUnMes(List<Transaccion> transaccionesDelMes,
        TipoDeCambioLogica tipoDeCambioLogica, int diasDelMes)
    {
        float? [] montosFinalDelDia = new float?[diasDelMes];
        for (int i = 0; i < diasDelMes; i++)
        {
            montosFinalDelDia[i] = 0;
        }

        foreach (var transaccion in transaccionesDelMes)
        {
            if(transaccion.Tipo == ConstantesCategoria.tipoIngreso){
                montosFinalDelDia[transaccion.Fecha.Day - 1 ] += ConvertirAPesosUruguayos(transaccion, tipoDeCambioLogica);
            }
        }
        return montosFinalDelDia;
    }
    
    public float?[] SumaCostosPorDiaDeUnMes(List<Transaccion> transaccionesDelMes,
        TipoDeCambioLogica tipoDeCambioLogica, int diasDelMes)
    {
        float? [] montosFinalDelDia = new float?[diasDelMes];
        for (int i = 0; i < diasDelMes; i++)
        {
            montosFinalDelDia[i] = 0;
        }

        foreach (var transaccion in transaccionesDelMes)
        {
            if(transaccion.Tipo == ConstantesCategoria.tipoCosto){
                montosFinalDelDia[transaccion.Fecha.Day - 1] += ConvertirAPesosUruguayos(transaccion, tipoDeCambioLogica);
            }
        }
        return montosFinalDelDia;
    }
}