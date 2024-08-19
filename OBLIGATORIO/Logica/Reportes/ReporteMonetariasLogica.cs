using Dominio;

namespace Logica;

public class ReporteMonetariasLogica : ReportesLogica
{
    public IList<Transaccion> TransaccionesDeUnaCuentaMonetaria(
        TransaccionLogica transaccionLogica, EspacioLogica espacioLogica,
        Monetaria cuentaMonetaria)
    {
        IList<Transaccion> transaccionesAMostrar = new List<Transaccion>();
        IList<Transaccion> transaccionesDeUnEspacio =
            transaccionLogica.ListarTransaccionesDeUnEspacio(espacioLogica.EspacioActual());

        foreach (Transaccion transaccion in transaccionesDeUnEspacio)
        {
            if (transaccion.Cuenta.Equals(cuentaMonetaria))
            {
                transaccionesAMostrar.Add(transaccion);
            }
        }

        return transaccionesAMostrar;
    }
}