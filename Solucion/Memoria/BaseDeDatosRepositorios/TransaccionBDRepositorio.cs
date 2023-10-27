using Dominio;

namespace Memoria.BaseDeDatosRepositorios;

public class TransaccionBDRepositorio : IRepositorio<Transaccion>
{
    private List<Transaccion> _transacciones = new List<Transaccion>();

    public Transaccion Agregar(Transaccion unaTransaccion)
    {
        unaTransaccion.Id = _transacciones.OrderByDescending(x => x.Id)
            .Select(x => x.Id)
            .FirstOrDefault() + 1;
        _transacciones.Add(unaTransaccion);
        return unaTransaccion;
    }

    public Transaccion? Encontrar(Func<Transaccion, bool> filtro)
    {
        return _transacciones.Where(filtro).FirstOrDefault();
    }
    
    public IList<Transaccion> ListarTodos()
    {
        return this._transacciones;
    }
    
    public Transaccion Eliminar(int id)
    {
        bool seElimino = false;
        Transaccion? transaccion = null;
        for (int i=0; i<_transacciones.Count && !seElimino;i++)
        {
            if (_transacciones[i].Id == id)
            {
                transaccion = _transacciones[i];
                _transacciones.Remove(_transacciones[i]);
                seElimino = true;
            }
        }

        return transaccion;
    }

    public Transaccion? Actualizar(Transaccion unaTransaccionEditada)
    {
        Transaccion? unaTransaccionAActualizar = Encontrar(x => x.Id == unaTransaccionEditada.Id);
        if (unaTransaccionAActualizar != null)
        {
            unaTransaccionAActualizar.Nombre = unaTransaccionEditada.Nombre;
        }
        return unaTransaccionAActualizar;
    }
}