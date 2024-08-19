using Dominio;
using Memoria;

namespace Logica;

public class TipoDeCambioLogica
{
    private readonly IRepositorio<TipoDeCambio> _repositorio;

    public TipoDeCambioLogica(
        IRepositorio<TipoDeCambio> tipoDeCambioRepositorio)
    {
        _repositorio = tipoDeCambioRepositorio;
    }

    public TipoDeCambio? EncontrarTipoDeCambio(int idAEncontrar)
    {
        return _repositorio.Encontrar(tipoDeCambio => tipoDeCambio.Id == idAEncontrar);
    }

    public TipoDeCambio AgregarTipoDeCambio(TipoDeCambio unTipoDeCambio)
    {
        ValidarTipoDeCambioUnico(unTipoDeCambio);
        return _repositorio.Agregar(unTipoDeCambio);
    }

    public IList<TipoDeCambio> ListarTiposDeCambio()
    {
        return _repositorio.ListarTodos();
    }

    public TipoDeCambio? EliminarTipoDeCambio(int idAEliminar)
    {
        return _repositorio.Eliminar(idAEliminar);
    }

    public TipoDeCambio? ActualizarTipoDeCambio(TipoDeCambio
        unTipoDeCambioActualizado)
    {
        return _repositorio.Actualizar(unTipoDeCambioActualizado);
    }

    public IList<TipoDeCambio> ListarTiposDeCambioDeUnEspacio(Espacio unEspacio)
    {
        IList<TipoDeCambio> tiposDeCambioDeEspacio = new List<TipoDeCambio>();
        List<TipoDeCambio> tiposDeCambio = ListarTiposDeCambio().ToList();
        foreach (TipoDeCambio tipoDeCambio in tiposDeCambio)
        {
            if (tipoDeCambio.Espacio.Equals(unEspacio))
            {
                tiposDeCambioDeEspacio.Add(tipoDeCambio);
            }
        }

        return tiposDeCambioDeEspacio;
    }

    public bool ValidarSiTipoDeCambioEsUtilizado(
        TipoDeCambio unTipoDeCambio, Espacio unEspacio,
        TransaccionLogica transaccionLogica)
    {
        bool tieneTransaccionAsociada = false;
        List<Transaccion> transacciones =
            transaccionLogica.ListarTransaccionesDeUnEspacio(unEspacio).ToList();
        for (int i = 0; i < transacciones.Count && !tieneTransaccionAsociada; i++)
        {
            if ((transacciones[i].Fecha.Equals(unTipoDeCambio.Fecha) && 
                transacciones[i].Moneda.Equals(unTipoDeCambio.Moneda)) 
                || 
                transacciones[i].Cuenta.Moneda.Equals(unTipoDeCambio.Moneda) && 
                transacciones[i].Fecha.Equals(unTipoDeCambio.Fecha))
            {
                tieneTransaccionAsociada = true;
            }
        }

        return tieneTransaccionAsociada;
    }

    private void ValidarTipoDeCambioUnico(TipoDeCambio unTipoDeCambio)
    {
        if (_repositorio.Encontrar
            (tipoDeCambio => tipoDeCambio.Fecha == unTipoDeCambio.Fecha &&
                             tipoDeCambio.Espacio.Equals(unTipoDeCambio.Espacio) &&
                             tipoDeCambio.Moneda.Equals(unTipoDeCambio.Moneda)) != null)
            throw new LogicaExcepcion("Ya existe un tipo de cambio" +
                                      " para esta fecha.");
    }
}