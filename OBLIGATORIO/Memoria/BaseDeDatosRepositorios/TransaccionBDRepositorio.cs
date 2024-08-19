using Dominio;
using Microsoft.EntityFrameworkCore;

namespace Memoria.BaseDeDatosRepositorios;

public class TransaccionBDRepositorio : IRepositorio<Transaccion>
{
    private SQLContexto _contexto;

    public TransaccionBDRepositorio(SQLContexto contexto)
    {
        _contexto = contexto;
    }

    public Transaccion Agregar(Transaccion unaTransaccion)
    {
        _contexto.Transacciones.Add(unaTransaccion);
        _contexto.SaveChanges();
        return unaTransaccion;
    }

    public Transaccion? Encontrar(Func<Transaccion, bool> filtro)
    {
        return _contexto.Transacciones
            .Include(transaccion => transaccion.Espacio)
            .Include(transaccion => transaccion.Moneda)
            .Include(transaccion => transaccion.Cuenta)
            .Include(transaccion => transaccion.Categoria)
            .Where(filtro).FirstOrDefault();
    }
    
    public IList<Transaccion> ListarTodos()
    {
        return _contexto.Transacciones
            .Include(transaccion => transaccion.Espacio)
            .Include(transaccion => transaccion.Moneda)
            .Include(transaccion => transaccion.Cuenta)
            .Include(transaccion => transaccion.Categoria)
            .ToList();
    }
    
    public Transaccion Eliminar(int id)
    {
        bool seElimino = false;
        Transaccion? transaccion = null;
        for (int i=0; i<_contexto.Transacciones.Count() && !seElimino;i++)
        {
            if (_contexto.Transacciones.ToList()[i].Id == id)
            {
                transaccion = _contexto.Transacciones.ToList()[i];
                _contexto.Transacciones.Remove(_contexto.Transacciones.ToList()[i]);
                seElimino = true;
            }
        }

        _contexto.SaveChanges();
        return transaccion;
    }

    public Transaccion? Actualizar(Transaccion unaTransaccionEditada)
    {
        Transaccion? unaTransaccionAActualizar = Encontrar(x => x.Id == unaTransaccionEditada.Id);
        if (unaTransaccionAActualizar != null)
        {
            unaTransaccionAActualizar.Nombre = unaTransaccionEditada.Nombre;
        }
        _contexto.SaveChanges();
        return unaTransaccionAActualizar;
    }
}