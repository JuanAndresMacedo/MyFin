using Dominio;
using Dominio.Constantes;
using Memoria;

namespace Logica;

public class ObjetivoDeGastoLogica
{
    private readonly IRepositorioObjDeGasto<ObjetivoDeGasto> _repositorio;

    public ObjetivoDeGastoLogica(
        IRepositorioObjDeGasto<ObjetivoDeGasto> objetivoDeGastoRepositorio)
    {
        _repositorio = objetivoDeGastoRepositorio;
    }

    public ObjetivoDeGasto AgregarObjetivoDeGasto(
        ObjetivoDeGasto unObjetivoDeGasto)
    {
        ValidarObjetivoDeGastoTituloUnico(unObjetivoDeGasto);
        ValidarObjetivoDeGastoConUnaOMasCategorias(unObjetivoDeGasto);
        return _repositorio.Agregar(unObjetivoDeGasto);
    }

    public ObjetivoDeGasto? EncontrarObjetivoDeGasto(int idAEncontrar)
    {
        return _repositorio.Encontrar(objetivoDeGasto => objetivoDeGasto.Id == idAEncontrar);
    }

    public IList<ObjetivoDeGasto> ListarObjetivosDeGasto()
    {
        return _repositorio.ListarTodos();
    }

    public List<ObjetivoDeGasto> ListarObjetivosDeGastosDeUnEspacio(
        Espacio unEspacio)
    {
        List<ObjetivoDeGasto> objetivosDeGasto =
            new List<ObjetivoDeGasto>();
        foreach (var objetivoDeGasto in _repositorio.ListarTodos())
        {
            if (objetivoDeGasto.Espacio.Equals(unEspacio))
            {
                objetivosDeGasto.Add(objetivoDeGasto);
            }
        }

        return objetivosDeGasto;
    }

    public void CompartirUnObjetivo(int idObjetivoACompartir)
    {
        ObjetivoDeGasto objetivoACompartir = EncontrarObjetivoDeGasto(idObjetivoACompartir);
        objetivoACompartir.Token = GenerarToken();
        ActualizarObjetivoDeGasto(objetivoACompartir);
    }

    public void DejarDeCompartirObjetivo(int idObjetivoADejarDeCompartir)
    {
        ObjetivoDeGasto objetivoADejarDeCompartir = EncontrarObjetivoDeGasto(idObjetivoADejarDeCompartir);
        objetivoADejarDeCompartir.Token = null;
        ActualizarObjetivoDeGasto(objetivoADejarDeCompartir);
    }
    
    public float? GastadoActualmenteEnObjetivo(int idObjetivoDeGasto, 
        TransaccionLogica transaccionLogica, TipoDeCambioLogica tipoDeCambioLogica)
    {
        ObjetivoDeGasto objetivoDeGasto = EncontrarObjetivoDeGasto(idObjetivoDeGasto);
        float? gastado = 0;
        
        foreach (var transaccion in transaccionLogica.ListarTransaccionesDeUnEspacio(objetivoDeGasto.Espacio))
        {
            if (objetivoDeGasto.Categorias.Contains(transaccion.Categoria) && 
                transaccion.Tipo == ConstantesCategoria.tipoCosto)
            {
                
                gastado += TransformarDineroTransaccionAPesoUruguayo(transaccion, tipoDeCambioLogica);
            }
        }

        return gastado;
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

    private void ActualizarObjetivoDeGasto(ObjetivoDeGasto unObjetivoDeGastoActualizado)
    {
        _repositorio.Actualizar(unObjetivoDeGastoActualizado);
    }
    
    private string GenerarToken()
    {
        Guid guid = Guid.NewGuid();
        return guid.ToString();
    }

    private void ValidarObjetivoDeGastoTituloUnico(ObjetivoDeGasto
        unObjetivoDeGasto)
    {
        if (_repositorio.Encontrar(objetivoDeGasto =>
                objetivoDeGasto.Titulo == unObjetivoDeGasto.Titulo &&
                objetivoDeGasto.Espacio.Equals(unObjetivoDeGasto.Espacio)) != null)
            throw new LogicaExcepcion("No es posible agregar dos objetivos" +
                                      " de gasto con el mismo título");
    }

    private void ValidarObjetivoDeGastoConUnaOMasCategorias(
        ObjetivoDeGasto unObjetivoDeGasto)
    {
        if (unObjetivoDeGasto.Categorias.Count < 1)
            throw new LogicaExcepcion("No es posible agregar un objetivo " +
                                      "de gasto sin una o varias categorías asociadas");
    }
}