using Dominio;
using Memoria;

namespace Logica;

public class MonetariaLogica
{
    private readonly IRepositorio<Monetaria> _repositorio;

    public MonetariaLogica(IRepositorio<Monetaria> monetariaRepositorio)
    {
        _repositorio = monetariaRepositorio;
    }

    public Monetaria AgregarMonetaria(Monetaria unaMonetaria)
    {
        ValidarMonetariaNombreUnicoAlCrear(unaMonetaria);
        return _repositorio.Agregar(unaMonetaria);
    }

    public Monetaria? EncontrarMonetaria(int idAEncontrar)
    {
        return _repositorio.Encontrar(monetaria => monetaria.Id == idAEncontrar);
    }

    public IList<Monetaria> ListarMonetarias()
    {
        return _repositorio.ListarTodos();
    }

    public List<Monetaria> ListarMonetariasDeUnEspacio(Espacio espacioActual)
    {
        List<Monetaria> monetariasEspacioActual = new List<Monetaria>();
        foreach (Monetaria monetaria in ListarMonetarias())
        {
            if (monetaria.Espacio.Equals(espacioActual))
            {
                monetariasEspacioActual.Add(monetaria);
            }
        }

        return monetariasEspacioActual;
    }

    public List<Monetaria> ListarMonetariasDeUnEspacioPorMoneda(Espacio espacioActual,
        Moneda unaMoneda)
    {
        List<Monetaria> monetariasEspacioActual = new List<Monetaria>();
        foreach (Monetaria monetaria in ListarMonetarias())
        {
            if (monetaria.Espacio.Equals(espacioActual) &&
                monetaria.Moneda.Equals(unaMoneda))
            {
                monetariasEspacioActual.Add(monetaria);
            }
        }

        return monetariasEspacioActual;
    }

    public Monetaria? EliminarMonetaria(int idAEliminar)
    {
        return _repositorio.Eliminar(idAEliminar);
    }

    public Monetaria? ActualizarMonetaria(Monetaria unaMonetariaActualizada)
    {
        ValidarMonetariaNombreUnicoAlEditar(unaMonetariaActualizada);
        return _repositorio.Actualizar(unaMonetariaActualizada);
    }

    public bool ValidarSiMonetariaTieneUnaTransacciónAsociada(Monetaria unaMonetaria,
        Espacio unEspacio, TransaccionLogica transaccionLogica)
    {
        bool tieneTransaccionAsociada = false;
        List<Transaccion> transacciones =
            transaccionLogica.ListarTransaccionesDeUnEspacio(unEspacio).ToList();
        for (int i = 0; i < transacciones.Count && !tieneTransaccionAsociada; i++)
        {
            if (transacciones[i].Cuenta.Equals(unaMonetaria))
            {
                tieneTransaccionAsociada = true;
            }
        }

        return tieneTransaccionAsociada;
    }
    
    private void ValidarMonetariaNombreUnicoAlCrear(Monetaria unaMonetaria)
    {
        foreach (Monetaria monetaria in ListarMonetarias())
        {
            if(monetaria.Nombre == unaMonetaria.Nombre && 
               monetaria.Espacio.Equals(unaMonetaria.Espacio) && 
               monetaria.Propietario.Equals(unaMonetaria.Propietario))
                throw new LogicaExcepcion("No es posible agregar dos" +
                                          " cuentas con el mismo nombre.");
        }
    }
    
    private void ValidarMonetariaNombreUnicoAlEditar(Monetaria unaMonetaria)
    {
        foreach (Monetaria monetaria in ListarMonetarias())
        {
            if (monetaria.Nombre == unaMonetaria.Nombre &&
                monetaria.Espacio.Equals(unaMonetaria.Espacio) &&
                monetaria.Propietario.Equals(unaMonetaria.Propietario) &&
                monetaria.Id != unaMonetaria.Id)
                throw new LogicaExcepcion("No es posible agregar dos" +
                                  " cuentas con el mismo nombre.");
        }
    }
}