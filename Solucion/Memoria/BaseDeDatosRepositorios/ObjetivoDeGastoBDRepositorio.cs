using Dominio;

namespace Memoria.BaseDeDatosRepositorios;

public class ObjetivoDeGastoBDRepositorio : IRepositorio<ObjetivoDeGasto>
{
    
    private List<ObjetivoDeGasto> _objetivoDeGastos = new List<ObjetivoDeGasto>();
    public ObjetivoDeGasto Agregar(ObjetivoDeGasto unObjetivoDeGasto)
    {
        unObjetivoDeGasto.Id = _objetivoDeGastos.OrderByDescending(x => x.Id)
            .Select(x => x.Id)
            .FirstOrDefault() + 1;
        _objetivoDeGastos.Add(unObjetivoDeGasto);
        return unObjetivoDeGasto;
    }

    public ObjetivoDeGasto Encontrar(Func<ObjetivoDeGasto, bool> filtro)
    {
        return _objetivoDeGastos.Where(filtro).FirstOrDefault();
    }

    public IList<ObjetivoDeGasto> ListarTodos()
    {
        return this._objetivoDeGastos;
    }
    
    public ObjetivoDeGasto Eliminar(int idAEliminar)
    {
        bool seElimino = false;
        ObjetivoDeGasto? objetivoDeGasto = null;
        for (int i=0; i<_objetivoDeGastos.Count && !seElimino;i++)
        {
            if (_objetivoDeGastos[i].Id == idAEliminar)
            {
                objetivoDeGasto = _objetivoDeGastos[i];
                _objetivoDeGastos.Remove(_objetivoDeGastos[i]);
                seElimino = true;
            }
        }

        return objetivoDeGasto;
    }

    public ObjetivoDeGasto? Actualizar(ObjetivoDeGasto unObjetivoDeGastoEditado)
    {
        throw new NotImplementedException();
    }

    
}