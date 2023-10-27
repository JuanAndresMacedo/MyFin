using Dominio;
using Memoria;

namespace Logica;

public class TransaccionLogica
{
    private readonly IRepositorio<Transaccion> _repositorio;

    public TransaccionLogica(
        IRepositorio<Transaccion> transaccionRepositorio)
    {
        _repositorio = transaccionRepositorio;
    }

    public Transaccion AgregarTransaccion(Transaccion unaTransaccion)
    {
        ValidarMonedaCorrectaParaCuenta(unaTransaccion);
        MovimientoDePlata(unaTransaccion);
        return _repositorio.Agregar(unaTransaccion);
    }

    public Transaccion? EncontrarTransaccion(int idAEncontrar)
    {
        return _repositorio.Encontrar(transaccion => transaccion.Id == idAEncontrar);
    }

    public IList<Transaccion> ListarTransacciones()
    {
        return _repositorio.ListarTodos();
    }

    public Transaccion? ActualizarTransaccion(Transaccion unaTransaccionActualizada)
    {
        return _repositorio.Actualizar(unaTransaccionActualizada);
    }

    public Transaccion? EliminarTransaccion(int idAEliminar)
    {
        return _repositorio.Eliminar(idAEliminar);
    }

    public IList<Transaccion> ListarTransaccionesDeUnEspacio(Espacio unEspacio)
    {
        IList<Transaccion> transaccionesEspacioSeleccionado
            = new List<Transaccion>();
        foreach (Transaccion transaccion in ListarTransacciones())
        {
            if (transaccion.Espacio.Equals(unEspacio))
            {
                transaccionesEspacioSeleccionado.Add(transaccion);
            }
        }

        return transaccionesEspacioSeleccionado;
    }

    public IList<Transaccion> ListarCostosDeUnEspacio(Espacio unEspacio)
    {
        IList<Transaccion> transaccionesEspacioSeleccionado =
            new List<Transaccion>();
        foreach (Transaccion transaccion in ListarTransacciones())
        {
            if (transaccion.Espacio.Equals(unEspacio)
                && transaccion.Tipo == "Costo")
            {
                transaccionesEspacioSeleccionado.Add(transaccion);
            }
        }

        return transaccionesEspacioSeleccionado;
    }


    public Transaccion? DuplicarTransaccion(Transaccion transaccionADuplicar)
    {
        Transaccion? aDuplicar = EncontrarTransaccion(transaccionADuplicar.Id);
        Transaccion transaccionDuplicada = null;
        if (aDuplicar != null)
        {
            transaccionDuplicada = new Transaccion()
            {
                Espacio = aDuplicar.Espacio,
                Fecha = DateTime.Now,
                Tipo = aDuplicar.Tipo,
                Nombre = aDuplicar.Nombre,
                Monto = aDuplicar.Monto,
                Moneda = aDuplicar.Moneda,
                Categoria = aDuplicar.Categoria,
                Cuenta = aDuplicar.Cuenta
            };
        }

        transaccionDuplicada.Nombre = transaccionDuplicada.Nombre;
        AgregarTransaccion(transaccionDuplicada);
        return transaccionDuplicada;
    }

    public void ValidarExistenciaDeTipoDeCambioParaFechaDeTransaccion(
        Transaccion unaTransaccion, Espacio unEspacio,
        TipoDeCambioLogica tipoDeCambioLogica)
    {
        if (unaTransaccion.Moneda == "US$")
        {
            bool hayTipoDeCambio = false;
            List<TipoDeCambio> tiposDeCambio =
                tipoDeCambioLogica.ListarTiposDeCambioDeUnEspacio
                    (unEspacio).ToList();

            for (int i = 0; i < tiposDeCambio.Count && !hayTipoDeCambio; i++)
            {
                if (unaTransaccion.Fecha == tiposDeCambio[i].Fecha)
                {
                    hayTipoDeCambio = true;
                }
            }

            if (!hayTipoDeCambio)
                throw new LogicaExcepcion("No existe un tipo de cambio" +
                                          " para la fecha seleccionada.");
        }
    }

    private void MovimientoDePlata(Transaccion unaTransaccion)
    {
        if (unaTransaccion.Tipo == "Costo")
        {
            unaTransaccion.Cuenta.DescontarCosto(unaTransaccion.Monto);
        }
        else
        {
            unaTransaccion.Cuenta.SumarIngreso(unaTransaccion.Monto);
        }
    }

    private void ValidarMonedaCorrectaParaCuenta(Transaccion unaTransaccion)
    {
        if (unaTransaccion.Cuenta.Moneda != unaTransaccion.Moneda)
            throw new LogicaExcepcion("No se pueden asociar transacciones de" +
                                      " una moneda distinta a la de la cuenta.");
    }
}