using Dominio;
using Memoria;

namespace Logica;

public class ObjetivoDeGastoLogica
{
    private readonly IRepositorio<ObjetivoDeGasto> _repositorio;

    public ObjetivoDeGastoLogica(
        IRepositorio<ObjetivoDeGasto> objetivoDeGastoRepositorio)
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

    public ObjetivoDeGasto? EliminarObjetivoDeGasto(int idAEliminar)
    {
        return _repositorio.Eliminar(idAEliminar);
    }

    private void ValidarObjetivoDeGastoTituloUnico(ObjetivoDeGasto
        unObjetivoDeGasto)
    {
        if (_repositorio.Encontrar(objetivoDeGasto => objetivoDeGasto.Titulo == unObjetivoDeGasto.Titulo) != null)
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