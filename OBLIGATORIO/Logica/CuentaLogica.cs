using Dominio;
using Memoria;

namespace Logica;

public class CuentaLogica
{
    private readonly IRepositorio<Cuenta> _repositorio;

    public CuentaLogica(IRepositorio<Cuenta> cuentaRepositorio)
    {
        _repositorio = cuentaRepositorio;
    }

    public Cuenta AgregarCuenta(Cuenta unaCuenta)
    {
        ValidarCuentaNombreUnico(unaCuenta);
        ValidarFechaDeCierreMayorAFechaDeCreacion(unaCuenta);
        return _repositorio.Agregar(unaCuenta);
    }

    public Cuenta? EncontrarCuenta(int idAEncontrar)
    {
        return _repositorio.Encontrar(cuenta => cuenta.Id == idAEncontrar);
    }

    public IList<Cuenta> ListarCuentas()
    {
        return _repositorio.ListarTodos();
    }

    public List<Cuenta> ListarCuentasDeUnEspacio(Espacio espacioActual)
    {
        List<Cuenta> cuentasEspacioActual = new List<Cuenta>();
        foreach (Cuenta cuenta in ListarCuentas())
        {
            if (cuenta.Espacio.Equals(espacioActual))
            {
                cuentasEspacioActual.Add(cuenta);
            }
        }

        return cuentasEspacioActual;
    }

    public List<Cuenta> ListarCuentasDeUnEspacioPorMoneda(Espacio espacioActual,
        string unaMoneda)
    {
        List<Cuenta> cuentasEspacioActual = new List<Cuenta>();
        foreach (Cuenta cuenta in ListarCuentas())
        {
            if (cuenta.Espacio.Equals(espacioActual) &&
                cuenta.Moneda == unaMoneda)
            {
                cuentasEspacioActual.Add(cuenta);
            }
        }

        return cuentasEspacioActual;
    }

    public Cuenta? EliminarCuenta(int idAEliminar)
    {
        return _repositorio.Eliminar(idAEliminar);
    }

    public Cuenta? ActualizarCuenta(Cuenta unaCuentaActualizada)
    {
        return _repositorio.Actualizar(unaCuentaActualizada);
    }

    public List<TarjetaDeCredito> DevolverTarjetasDeCreditoDeUnEspacio(
        Espacio unEspacio)
    {
        List<TarjetaDeCredito> tarjetasDeCredito =
            new List<TarjetaDeCredito>();
        foreach (var cuenta in _repositorio.ListarTodos())
        {
            if (cuenta.DevolverTipoDeCuenta() == "tarjetaDeCredito" &&
                cuenta.Espacio.Equals(unEspacio))
            {
                tarjetasDeCredito.Add((TarjetaDeCredito)cuenta);
            }
        }

        return tarjetasDeCredito;
    }

    public List<Monetaria> DevolverCuentasMonetariasDeUnEspacio(Espacio unEspacio)
    {
        List<Monetaria> cuentasMonetarias = new List<Monetaria>();
        foreach (var cuenta in _repositorio.ListarTodos())
        {
            if (cuenta.DevolverTipoDeCuenta() == "monetaria"
                && cuenta.Espacio.Equals(unEspacio))
            {
                cuentasMonetarias.Add((Monetaria)cuenta);
            }
        }

        return cuentasMonetarias;
    }

    public bool ValidarSiCuentaTieneUnaTransacciónAsociada(Cuenta unaCuenta,
        Espacio unEspacio, TransaccionLogica transaccionLogica)
    {
        bool tieneTransaccionAsociada = false;
        List<Transaccion> transacciones =
            transaccionLogica.ListarTransaccionesDeUnEspacio(unEspacio).ToList();
        for (int i = 0; i < transacciones.Count && !tieneTransaccionAsociada; i++)
        {
            if (transacciones[i].Cuenta.Equals(unaCuenta))
            {
                tieneTransaccionAsociada = true;
            }
        }

        return tieneTransaccionAsociada;
    }
    
    private void ValidarCuentaNombreUnico(Cuenta unaCuenta)
    {
        foreach (Cuenta cuenta in ListarCuentas())
        {
            if(cuenta.Nombre == unaCuenta.Nombre && 
               cuenta.Espacio.Equals(unaCuenta.Espacio) && 
               cuenta.Propietario.Equals(unaCuenta.Propietario))
                throw new LogicaExcepcion("No es posible agregar dos" +
                                          " cuentas con el mismo nombre.");
        }
    }
    
    private void ValidarFechaDeCierreMayorAFechaDeCreacion(Cuenta unaCuenta)
    {
        if (unaCuenta.DevolverTipoDeCuenta() == "tarjetaDeCredito")
        {
            TarjetaDeCredito unaTarjetaDeCredito = (TarjetaDeCredito)unaCuenta;

            if (unaTarjetaDeCredito.FechaDeCierre <= unaTarjetaDeCredito.FechaDeCreacion)
                throw new LogicaExcepcion("La fecha de cierre de la cuenta " +
                                          "debe ser mayor a la fecha de creación.");
        }
    }
}