using Dominio;

namespace Memoria.BaseDeDatosRepositorios;

public class TipoDeCambioBDRepositorio: IRepositorio<TipoDeCambio>
{
    private List<TipoDeCambio> _tipoDeCambios = new List<TipoDeCambio>();
    public TipoDeCambio Agregar(TipoDeCambio unTipoDeCambio)
    {
        unTipoDeCambio.Id = _tipoDeCambios.OrderByDescending(x => x.Id)
            .Select(x => x.Id)
            .FirstOrDefault() + 1;
        _tipoDeCambios.Add(unTipoDeCambio);
        return unTipoDeCambio;
    }
    

    public TipoDeCambio? Encontrar(Func<TipoDeCambio, bool> filtro)
    {
        return _tipoDeCambios.Where(filtro).FirstOrDefault();
    }

    public IList<TipoDeCambio> ListarTodos()
    {
        return this._tipoDeCambios;
    }

    public TipoDeCambio? Eliminar(int id)
    {
        bool seElimino = false;
        TipoDeCambio? tipoDeCambio = null;
        for (int i=0; i<_tipoDeCambios.Count && !seElimino;i++)
        {
            if (_tipoDeCambios[i].Id == id)
            {
                tipoDeCambio = _tipoDeCambios[i];
                _tipoDeCambios.Remove(_tipoDeCambios[i]);
                seElimino = true;
            }
        }

        return tipoDeCambio;
    }

    public TipoDeCambio? Actualizar(TipoDeCambio unTipoDeCambioEditado)
    {
        TipoDeCambio? unTipoDeCambioAActualizar = Encontrar(x => x.Id == unTipoDeCambioEditado.Id);
        if (unTipoDeCambioAActualizar != null)
        {
            unTipoDeCambioAActualizar.ValorDelDolar = unTipoDeCambioEditado.ValorDelDolar;
        }
        return unTipoDeCambioAActualizar;
    }
}