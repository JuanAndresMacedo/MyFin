using Dominio;
using Dominio.Constantes;
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
                && transaccion.Tipo == ConstantesCategoria.tipoCosto)
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
                Fecha = aDuplicar.Fecha,
                Tipo = aDuplicar.Tipo,
                Nombre = aDuplicar.Nombre,
                Monto = aDuplicar.Monto,
                Moneda = aDuplicar.Moneda,
                Categoria = aDuplicar.Categoria,
                Cuenta = aDuplicar.Cuenta
            };
        }
        
        AgregarTransaccion(transaccionDuplicada);
        return transaccionDuplicada;
    }

    public void ValidarExistenciaDeTipoDeCambioParaFechaDeTransaccion(
        Transaccion unaTransaccion, Espacio unEspacio,
        TipoDeCambioLogica tipoDeCambioLogica)
    {
        Moneda monedaTransaccion = unaTransaccion.Moneda;
        Moneda monedaCuenta = unaTransaccion.Cuenta.Moneda;
        DateTime fechaTransaccion = unaTransaccion.Fecha;
        bool hayTipoDeCambioParaMonedaCuenta, hayTipoDeCambioParaMonedaTransaccion;

        if (!EsTransaccionEnPesosUruguayosACuentaPesosUruguayos(monedaTransaccion, monedaCuenta))
        {
            if (monedaTransaccion.Nombre == ConstantesMoneda.PesoUruguayo)
            {
                hayTipoDeCambioParaMonedaCuenta = HayTipoDeCambio(fechaTransaccion, monedaCuenta,
                    unEspacio, tipoDeCambioLogica);

                if (!hayTipoDeCambioParaMonedaCuenta)
                    throw new LogicaExcepcion("No existe un tipo de cambio" +
                                              " para la moneda de la cuenta a " +
                                              unaTransaccion.Moneda.Nombre +
                                              " en la fecha seleccionada.");
            }

            if (monedaCuenta.Nombre == ConstantesMoneda.PesoUruguayo)
            {
                hayTipoDeCambioParaMonedaTransaccion = HayTipoDeCambio(fechaTransaccion,
                    monedaTransaccion, unEspacio, tipoDeCambioLogica);

                if (!hayTipoDeCambioParaMonedaTransaccion)
                    throw new LogicaExcepcion("No existe un tipo de cambio" +
                                              " para la moneda de la transacción a " +
                                              unaTransaccion.Cuenta.Moneda.Nombre +
                                              " en la fecha seleccionada.");
            }

            if (monedaCuenta.Nombre != ConstantesMoneda.PesoUruguayo 
                && monedaTransaccion.Nombre != ConstantesMoneda.PesoUruguayo)
            {
                hayTipoDeCambioParaMonedaCuenta = HayTipoDeCambio(fechaTransaccion, monedaCuenta,
                    unEspacio, tipoDeCambioLogica);
                hayTipoDeCambioParaMonedaTransaccion = HayTipoDeCambio(fechaTransaccion,
                    monedaTransaccion, unEspacio, tipoDeCambioLogica);

                if (!hayTipoDeCambioParaMonedaCuenta || !hayTipoDeCambioParaMonedaTransaccion)
                    throw new LogicaExcepcion("No existe un tipo de cambio" +
                                              " para la moneda de la transaccion (" +
                                              unaTransaccion.Moneda.Nombre +
                                              ") o la moneda de la cuenta (" +
                                              unaTransaccion.Cuenta.Moneda.Nombre +
                                              ") en la fecha seleccionada.");
            }
        }
    }

    private bool HayTipoDeCambio(DateTime unaFecha, Moneda unaMoneda, Espacio unEspacio,
        TipoDeCambioLogica tipoDeCambioLogica)
    {
        bool hayTipoDeCambio = false;
        List<TipoDeCambio> tiposDeCambio =
            tipoDeCambioLogica.ListarTiposDeCambioDeUnEspacio
                (unEspacio).ToList();

        for (int i = 0; i < tiposDeCambio.Count && !hayTipoDeCambio; i++)
        {
            if (unaFecha == tiposDeCambio[i].Fecha &&
                unaMoneda.Nombre == tiposDeCambio[i].Moneda.Nombre)
            {
                hayTipoDeCambio = true;
            }
        }

        return hayTipoDeCambio;
    }

    private bool EsTransaccionEnPesosUruguayosACuentaPesosUruguayos(Moneda monedaTransaccion, Moneda monedaCuenta)
    {
        return monedaTransaccion.Nombre == ConstantesMoneda.PesoUruguayo &&
               monedaCuenta.Nombre == ConstantesMoneda.PesoUruguayo;
    }

    public void MovimientoDeDinero(Transaccion unaTransaccion,
        TipoDeCambioLogica tipoDeCambioLogica)
    {
        float? montoTransaccionEnPesoUruguayo = TransformarDineroTransaccionAPesoUruguayo
            (unaTransaccion, tipoDeCambioLogica);
        float? montoCuentaEnPesoUruguayo = TransformarDineroCuentaAPesoUruguayo
            (unaTransaccion, tipoDeCambioLogica);
        float? resultadoTransaccion = 0;

        if (unaTransaccion.Tipo == ConstantesCategoria.tipoCosto)
        {
            resultadoTransaccion = montoCuentaEnPesoUruguayo - montoTransaccionEnPesoUruguayo;
        }
        else
        {
            resultadoTransaccion = montoCuentaEnPesoUruguayo + montoTransaccionEnPesoUruguayo;
        }

        Cuenta cuentaTransaccion = unaTransaccion.Cuenta;
        
        if (cuentaTransaccion.Moneda.Nombre != ConstantesMoneda.PesoUruguayo)
        {
            resultadoTransaccion = TransformarDineroAMonedaCuentaOriginal(resultadoTransaccion,
                unaTransaccion, tipoDeCambioLogica);
        }

        cuentaTransaccion.AsignarDineroCuenta(resultadoTransaccion);
        ActualizarTransaccion(unaTransaccion);
    }

    private float? TransformarDineroTransaccionAPesoUruguayo(Transaccion unaTransaccion,
        TipoDeCambioLogica tipoDeCambioLogica)
    {
        float? montoTransaccionEnPesosUruguayos = unaTransaccion.Monto;

        if (unaTransaccion.Moneda.Nombre != ConstantesMoneda.PesoUruguayo)
        {
            bool seEncontróTipoDeCambio = false;
            IList<TipoDeCambio> tipoDeCambios =
                tipoDeCambioLogica.ListarTiposDeCambioDeUnEspacio(unaTransaccion.Espacio);

            for (int i = 0; i < tipoDeCambios.Count && !seEncontróTipoDeCambio; i++)
            {
                if (tipoDeCambios[i].Fecha == unaTransaccion.Fecha &&
                    tipoDeCambios[i].Moneda.Equals(unaTransaccion.Moneda))
                {
                    seEncontróTipoDeCambio = true;
                    montoTransaccionEnPesosUruguayos = tipoDeCambios[i].ValorDeLaMoneda *
                                                       unaTransaccion.Monto;
                }
            }
        }

        return montoTransaccionEnPesosUruguayos;
    }

    private float? TransformarDineroCuentaAPesoUruguayo(Transaccion unaTransaccion,
        TipoDeCambioLogica tipoDeCambioLogica)
    {
        float? montoCuentaEnPesosUruguayos = unaTransaccion.Cuenta.DevolverDineroCuenta();

        if (unaTransaccion.Cuenta.Moneda.Nombre != ConstantesMoneda.PesoUruguayo)
        {
            bool seEncontróTipoDeCambio = false;
            IList<TipoDeCambio> tipoDeCambios =
                tipoDeCambioLogica.ListarTiposDeCambioDeUnEspacio(unaTransaccion.Espacio);

            for (int i = 0; i < tipoDeCambios.Count && !seEncontróTipoDeCambio; i++)
            {
                if (tipoDeCambios[i].Fecha == unaTransaccion.Fecha &&
                    tipoDeCambios[i].Moneda.Equals(unaTransaccion.Cuenta.Moneda))
                {
                    seEncontróTipoDeCambio = true;
                    montoCuentaEnPesosUruguayos = tipoDeCambios[i].ValorDeLaMoneda *
                                                  unaTransaccion.Cuenta.DevolverDineroCuenta();
                }
            }
        }

        return montoCuentaEnPesosUruguayos;
    }

    private float? TransformarDineroAMonedaCuentaOriginal(float? resultadoTransaccion,
        Transaccion unaTransaccion, TipoDeCambioLogica tipoDeCambioLogica)
    {
        bool seEncontróTipoDeCambio = false;
        IList<TipoDeCambio> tipoDeCambios =
            tipoDeCambioLogica.ListarTiposDeCambioDeUnEspacio(unaTransaccion.Espacio);
        
        for (int i = 0; i < tipoDeCambios.Count && !seEncontróTipoDeCambio; i++)
        {
            if (tipoDeCambios[i].Fecha == unaTransaccion.Fecha &&
                tipoDeCambios[i].Moneda.Equals(unaTransaccion.Cuenta.Moneda))
            {
                seEncontróTipoDeCambio = true;
                resultadoTransaccion = resultadoTransaccion / tipoDeCambios[i].ValorDeLaMoneda;
            }
        }

        return resultadoTransaccion;
    }
}