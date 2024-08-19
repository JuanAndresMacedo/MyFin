using Dominio;
using Memoria;

namespace Logica;

public class TarjetaDeCreditoLogica
{
    private readonly IRepositorio<TarjetaDeCredito> _repositorio;

    public TarjetaDeCreditoLogica(IRepositorio<TarjetaDeCredito> tarjetaDeCreditoRepositorio)
    {
        _repositorio = tarjetaDeCreditoRepositorio;
    }

    public TarjetaDeCredito AgregarTarjetaDeCredito(TarjetaDeCredito unaTarjetaDeCredito)
    {
        ValidarTarjetaDeCreditoNombreUnico(unaTarjetaDeCredito);
        ValidarFechaDeCierreMayorAFechaDeCreacion(unaTarjetaDeCredito);
        return _repositorio.Agregar(unaTarjetaDeCredito);
    }

    public TarjetaDeCredito? EncontrarTarjetaDeCredito(int idAEncontrar)
    {
        return _repositorio.Encontrar(tarjetaDeCredito => tarjetaDeCredito.Id == idAEncontrar);
    }

    public IList<TarjetaDeCredito> ListarTarjetasDeCredito()
    {
        return _repositorio.ListarTodos();
    }

    public List<TarjetaDeCredito> ListarTarjetasDeCreditoDeUnEspacio(Espacio espacioActual)
    {
        List<TarjetaDeCredito> tarjetasDeCreditoEspacioActual = new List<TarjetaDeCredito>();
        foreach (TarjetaDeCredito tarjetaDeCredito in ListarTarjetasDeCredito())
        {
            if (tarjetaDeCredito.Espacio.Equals(espacioActual))
            {
                tarjetasDeCreditoEspacioActual.Add(tarjetaDeCredito);
            }
        }

        return tarjetasDeCreditoEspacioActual;
    }

    public List<TarjetaDeCredito> ListarTarjetasDeCreditoDeUnEspacioPorMoneda(Espacio espacioActual,
        Moneda unaMoneda)
    {
        List<TarjetaDeCredito> tarjetasDeCreditoEspacioActual = new List<TarjetaDeCredito>();
        foreach (TarjetaDeCredito tarjetaDeCredito in ListarTarjetasDeCredito())
        {
            if (tarjetaDeCredito.Espacio.Equals(espacioActual) &&
                tarjetaDeCredito.Moneda.Equals(unaMoneda))
            {
                tarjetasDeCreditoEspacioActual.Add(tarjetaDeCredito);
            }
        }

        return tarjetasDeCreditoEspacioActual;
    }

    public TarjetaDeCredito? EliminarTarjetaDeCredito(int idAEliminar)
    {
        return _repositorio.Eliminar(idAEliminar);
    }

    public TarjetaDeCredito? ActualizarTarjetaDeCredito(TarjetaDeCredito unaTarjetaDeCreditoActualizada)
    {
        ValidarTarjetaDeCreditoNombreUnicoAlEditar(unaTarjetaDeCreditoActualizada);
        return _repositorio.Actualizar(unaTarjetaDeCreditoActualizada);
    }

    public bool ValidarSiTarjetaDeCreditoTieneUnaTransacciónAsociada(TarjetaDeCredito unaTarjetaDeCredito,
        Espacio unEspacio, TransaccionLogica transaccionLogica)
    {
        bool tieneTransaccionAsociada = false;
        List<Transaccion> transacciones =
            transaccionLogica.ListarTransaccionesDeUnEspacio(unEspacio).ToList();
        for (int i = 0; i < transacciones.Count && !tieneTransaccionAsociada; i++)
        {
            if (transacciones[i].Cuenta.Equals(unaTarjetaDeCredito))
            {
                tieneTransaccionAsociada = true;
            }
        }

        return tieneTransaccionAsociada;
    }

    private void ValidarTarjetaDeCreditoNombreUnico(TarjetaDeCredito unaTarjetaDeCredito)
    {
        foreach (TarjetaDeCredito tarjetaDeCredito in ListarTarjetasDeCredito())
        {
            if (tarjetaDeCredito.Nombre == unaTarjetaDeCredito.Nombre &&
                tarjetaDeCredito.Espacio.Equals(unaTarjetaDeCredito.Espacio) &&
                tarjetaDeCredito.Propietario.Equals(unaTarjetaDeCredito.Propietario))
                throw new LogicaExcepcion("No es posible agregar dos" +
                                          " cuentas con el mismo nombre.");
        }
    }
    
    private void ValidarTarjetaDeCreditoNombreUnicoAlEditar(TarjetaDeCredito unaTarjetaDeCredito)
    {
        foreach (TarjetaDeCredito tarjetaDeCredito in ListarTarjetasDeCredito())
        {
            if (tarjetaDeCredito.Nombre == unaTarjetaDeCredito.Nombre &&
                tarjetaDeCredito.Espacio.Equals(unaTarjetaDeCredito.Espacio) &&
                tarjetaDeCredito.Propietario.Equals(unaTarjetaDeCredito.Propietario) &&
                tarjetaDeCredito.Id != unaTarjetaDeCredito.Id)
                throw new LogicaExcepcion("No es posible agregar dos" +
                                          " cuentas con el mismo nombre.");
        }
    }

    private void ValidarFechaDeCierreMayorAFechaDeCreacion(TarjetaDeCredito unaTarjetaDeCredito)
    {
        if (unaTarjetaDeCredito.FechaDeCierre <= unaTarjetaDeCredito.FechaDeCreacion)
            throw new LogicaExcepcion("La fecha de cierre de la cuenta " +
                                      "debe ser mayor a la fecha de creación.");
    }
}